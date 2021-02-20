using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yuzu_Updater
{

    internal class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    class DownloadManager
    {
        public class DownloadResult
        {
            public long Size { get; set; }
            public String FilePath { get; set; }
            public TimeSpan TimeTaken { get; set; }
            public int ParallelDownloads { get; set; }
        }

        static DownloadManager()
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 1000;
        }

        public static async Task<DownloadResult> Download(String fileUrl, String destinationFolderPath, int numberOfParallelDownloads = 1, Action<int> progressCallback = null, bool validateSSL = false)
        {
            return await Task.Run(() =>
            {

                if (!validateSSL)
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                }

                Uri uri = new Uri(fileUrl);

                //Calculate destination path  
                String destinationFilePath = Path.Combine(destinationFolderPath, uri.Segments.Last());

                DownloadResult result = new DownloadResult() { FilePath = destinationFilePath };

                //Handle number of parallel downloads  
                if (numberOfParallelDownloads <= 0)
                {
                    numberOfParallelDownloads = Environment.ProcessorCount;
                }

                #region Get file size  
                WebRequest webRequest = HttpWebRequest.Create(fileUrl);
                webRequest.Method = "HEAD";
                long responseLength;
                float finishedRequests = 0;
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    responseLength = long.Parse(webResponse.Headers.Get("Content-Length"));
                    result.Size = responseLength;
                }
                #endregion

                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
                }

                using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Append))
                {
                    ConcurrentDictionary<Range, String> tempFilesDictionary = new ConcurrentDictionary<Range, String>();

                    #region Calculate ranges  
                    List<Range> readRanges = new List<Range>();
                    for (int chunk = 0; chunk < numberOfParallelDownloads - 1; chunk++)
                    {
                        var range = new Range()
                        {
                            Start = chunk * (responseLength / numberOfParallelDownloads),
                            End = ((chunk + 1) * (responseLength / numberOfParallelDownloads)) - 1
                        };
                        readRanges.Add(range);
                    }

                    readRanges.Add(new Range()
                    {
                        Start = readRanges.Any() ? readRanges.Last().End + 1 : 0,
                        End = responseLength - 1
                    });

                    #endregion

                    DateTime startTime = DateTime.Now;

                    #region Parallel download  

                    int index = 0;
                    Parallel.ForEach(readRanges, new ParallelOptions() { MaxDegreeOfParallelism = numberOfParallelDownloads }, readRange =>
                    {
                        HttpWebRequest httpWebRequest = HttpWebRequest.Create(fileUrl) as HttpWebRequest;
                        httpWebRequest.Method = "GET";
                        httpWebRequest.AddRange(readRange.Start, readRange.End);
                        using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            String tempFilePath = Path.GetTempFileName();
                            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                httpWebResponse.GetResponseStream().CopyTo(fileStream);
                                int progress = (int)((++finishedRequests / numberOfParallelDownloads) * 100);
                                progressCallback(progress);
                                tempFilesDictionary.TryAdd(readRange, tempFilePath);
                            }
                        }
                        index++;
                    });

                    result.ParallelDownloads = index;

                    #endregion

                    result.TimeTaken = DateTime.Now.Subtract(startTime);

                    #region Merge to single file  
                    foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.Key.Start))
                    {
                        byte[] tempFileBytes = File.ReadAllBytes(tempFile.Value);
                        destinationStream.Write(tempFileBytes, 0, tempFileBytes.Length);
                        File.Delete(tempFile.Value);
                    }
                    #endregion


                    return result;
                }
            });


        }
    }

}
