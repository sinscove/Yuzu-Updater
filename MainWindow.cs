using SevenZipNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using static Yuzu_Updater.DownloadManager;

namespace Yuzu_Updater
{
    public partial class MainForm : Form
    {
        private Settings settings = Settings.GetInstance();
        private Logger logger = new Logger();
        private HttpClient httpClient = new HttpClient();
        private Dictionary<string, string> archivedVersions = new Dictionary<string, string>();
        private Stopwatch stopwatch = new Stopwatch();
        private SettingsView settingsView = new SettingsView();
        public MainForm()
        {
            httpClient.Timeout = TimeSpan.FromSeconds(20);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.31.1");
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private async Task CheckForUpdates()
        {
            try
            {
                SetControlsEnabled(false);
                var response = await httpClient.GetAsync("https://pastebin.com/raw/tc6Pk7rz");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();
                        string[] lines = stringResponse.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        if (lines.Length > 1)
                        {
                            SetStatusAndProgress("Retrieved Database Info", 25);
                            string version = lines[0];
                            string nextURL = lines[2];

                            int latestArchivedVersion = 0;
                            int versionAsInt = 0;
                            int currentVersion = 0;
                            try
                            {
                                latestArchivedVersion = int.Parse(VersionDropdown.Items[1].ToString());
                                versionAsInt = int.Parse(version);
                                currentVersion = int.Parse(settings.Version);
                            }catch(Exception)
                            {
                                // Ignore
                            }
                            if (latestArchivedVersion > versionAsInt)
                            {
                                if (latestArchivedVersion > currentVersion)
                                {
                                    if (settings.BackupSaveFiles)
                                    {
                                        BackupSaveFiles();
                                    }
                                    await DownloadVersion(VersionDropdown.Items[1].ToString(), true);
                                }
                                else
                                {
                                    SetStatusAndProgress("You have the latest version!", 100);
                                    FinishUp();
                                }
                            }
                            else
                            {
                                if (settings.Version.Equals(version))
                                {
                                    SetStatusAndProgress("You have the latest version!", 100);
                                    FinishUp();
                                }
                                else
                                {
                                    if (settings.BackupSaveFiles)
                                    {
                                        BackupSaveFiles();
                                    }
                                    if (nextURL.Length > 0)
                                    {
                                        response = await httpClient.GetAsync(nextURL);

                                        if (response.IsSuccessStatusCode)
                                        {
                                            string content = await response.Content.ReadAsStringAsync();
                                            content = content.Substring(content.IndexOf("https://download"));
                                            content = content.Substring(0, content.IndexOf("\""));
                                            nextURL = content;
                                            String fileName = "YuzuEA-" + version + ".7z";

                                            SetStatusAndProgress("Downloading Update: " + version, 35);
                                            String address = nextURL;

                                            if (settings.AcceleratedDownloads)
                                            {
                                                DownloadResult downloadResult = await DownloadManager.Download(address, Directory.GetCurrentDirectory(), Math.Min(settings.MaxConnections, 16), DownloadProgressChanged);
                                                SetStatus($"Download Took: {downloadResult.TimeTaken}");
                                                DownloadCompleted(new FileDownloadInfo(downloadResult.FilePath, version, false));
                                            }
                                            else
                                            {
                                                using (WebClient webClient = new WebClient())
                                                {
                                                    webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                                                    webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                                                    stopwatch.Start();
                                                    webClient.DownloadFileAsync(new Uri(address), Directory.GetCurrentDirectory() + "\\" + fileName, new FileDownloadInfo(fileName, version, false));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SetStatusAndProgress($"Error {response.StatusCode.ToString()} returned.", 0);
                                            logger.Log(LogLevel.ERROR, $"Download URL returned an error code {response.StatusCode.ToString()}");
                                            SetControlsEnabled(true);
                                        }
                                    }
                                    else
                                    {
                                        SetStatusAndProgress("Initial attempt failed", 0);
                                        logger.Log(LogLevel.WARNING, "Couldn't parse URL for download");
                                        SetControlsEnabled(true);
                                    }

                                }
                            }
                        }
                        else
                        {
                            SetStatusAndProgress("Initial attempt failed", 0);
                            logger.Log(LogLevel.WARNING, "Couldn't parse database for URL fetch.");
                            if (settings.SinMode)
                            {
                                SetStatusAndProgress("You have the latest version (Sin hasn't updated yet)!", 0);
                            }
                            else
                            {
                                await AttemptFallbackDownload();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (!e.GetType().IsAssignableFrom(typeof(ObjectDisposedException)))
                        {
                            logger.Log(LogLevel.WARNING, e.Message);

                            if (settings.SinMode)
                            {
                                SetStatusAndProgress("You have the latest version (Sin hasn't updated yet)!", 0);
                            }
                            else
                            {
                                await AttemptFallbackDownload();
                            }
                        }

                    }
                }
                else
                {
                    if (settings.SinMode)
                    {
                        SetStatusAndProgress("You have the latest version (Sin hasn't updated yet)!", 0);
                    }
                    else
                    {
                        await AttemptFallbackDownload();
                    }
                }
            }
            catch (Exception e)
            {
                if (e.GetType().IsAssignableFrom(typeof(HttpRequestException)) || e.GetType().IsAssignableFrom(typeof(TaskCanceledException)))
                {
                    if (settings.SinMode)
                    {
                        SetStatusAndProgress("You have the latest version (Sin hasn't updated yet)!", 0);
                    }
                    else
                    {
                        await AttemptFallbackDownload();
                    }
                }
                else
                {
                    SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                    logger.Log(LogLevel.ERROR, e.Message);
                    logger.Log(LogLevel.ERROR, e.StackTrace);
                    SetControlsEnabled(true);
                }
            }
        }

        private async Task AttemptFallbackDownload()
        {
            if (VersionDropdown.Items.Count > 1)
            {
                int latestArchivedVersion = int.Parse(VersionDropdown.Items[1].ToString());
                int settingsVersion = 0;
                try
                {
                    settingsVersion = int.Parse(settings.Version);
                }
                catch (Exception)
                {
                    //Do nothing
                }
                if (latestArchivedVersion > settingsVersion)
                {
                    await DownloadVersion(VersionDropdown.Items[1].ToString(), true);
                }
                else
                {
                    LaunchEmulator();
                }
            }
        }

        private async Task DownloadAvailableArchivedVersions()
        {
            SetStatusAndProgress("Fetching list of older versions", 0);
            try
            {
                var response = await httpClient.GetAsync("https://pineappleea.github.io/");

                if (response.IsSuccessStatusCode)
                {
                    var patternAnon = new Regex("https://anonfiles.com/.*/YuzuEA-(.*)_7z", RegexOptions.Multiline);
                    var contentAnon = await response.Content.ReadAsStringAsync();
                    var matchesAnon = patternAnon.Matches(contentAnon);

                    var patternGit = new Regex("https://github.com/pineappleEA/pineapple-src/releases/tag/EA-(\\d*)", RegexOptions.Multiline);
                    var contentGit = await response.Content.ReadAsStringAsync();
                    var matchesGit = patternGit.Matches(contentGit);

                    if (matchesGit.Count > 0)
                    {
                        foreach (Match match in matchesGit)
                        {
                            archivedVersions.Add(match.Groups[1].Value, match.Groups[0].Value);
                            Invoke(new MethodInvoker(() =>
                            {
                                VersionDropdown.Items.Add(match.Groups[1].Value);
                            }));
                        }
                    }

                    if (matchesAnon.Count > 0)
                    {
                        foreach (Match match in matchesAnon)
                        {
                            archivedVersions.Add(match.Groups[1].Value, match.Groups[0].Value);
                            Invoke(new MethodInvoker(() =>
                            {
                                VersionDropdown.Items.Add(match.Groups[1].Value);
                            }));
                        }
                    }

                    SetStatusAndProgress("Fetched list of older versions!", 100);
                }
                else
                {
                    SetStatusAndProgress("Couldn't fetch older versions, code: " + response.StatusCode, 100);
                }
            }
            catch (Exception e)
            {
                SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
            }
        }

        private void BackupSaveFiles()
        {
            try
            {
                string appDataPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\yuzu\\nand\\user";
                string version = settings.Version;
                if (version.Trim().Equals("")) version = "0";
                if(Directory.Exists(appDataPath))
                {
                    if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\backup-{version}"))
                    {
                        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\backup-{version}");
                    }
                    if (Directory.Exists($"{appDataPath}\\save"))
                    {
                        Copy($"{appDataPath}\\save", $"{Directory.GetCurrentDirectory()}\\backup-{version}\\save", "Backing up save data...");
                    }
                    if (Directory.Exists($"{appDataPath}\\saveMeta"))
                    {
                        Copy($"{appDataPath}\\saveMeta", $"{Directory.GetCurrentDirectory()}\\backup-{version}\\saveMeta", "Backing up save metadata...");
                    }
                }
            }
            catch(Exception e)
            {
                SetStatus("Couldn't create backup.");
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
            }
        }
        class GithubReleaseAsset
        {
            public string browser_download_url { get; set; }
        }
        class GithubRealeaseJSON
        {
            

            public IEnumerable<GithubReleaseAsset> assets { get; set; }
        }

        private async Task DownloadVersion(string version, bool replaceAsLatest)
        {
            try
            {
                if (archivedVersions.ContainsKey(version))
                {
                    SetStatusAndProgress("Downloading version " + version + "\r\nThis may take a while..", 0);
                    SetControlsEnabled(false);

                    var gitUrl = "https://github.com/pineappleEA/pineapple-src/releases/tag/EA-";
                    var gitLink = "https://api.github.com/repos/pineappleEA/pineapple-src/releases/tags/EA-" + version;

                    var anonResponse = await httpClient.GetAsync(archivedVersions[version]);
                    var gitResponse = await httpClient.GetAsync(gitLink);

                    if (gitResponse.IsSuccessStatusCode)
                    {

                        var str = await gitResponse.Content.ReadAsStringAsync();
                        var gitContent = JsonConvert.DeserializeObject<GithubRealeaseJSON>(await gitResponse.Content.ReadAsStringAsync());

                        var archivePath = gitContent.assets.Select(asset => asset.browser_download_url).FirstOrDefault(fileUrl =>
                            fileUrl.Contains("Windows-Yuzu-EA") && (fileUrl.EndsWith(".7z") || fileUrl.EndsWith(".zip")));

                        if (string.IsNullOrEmpty(archivePath))
                        {
                            throw new Exception($"Could not find archived file for release ${version} at ${gitLink}");
                        }

                        var fileName = Path.GetFileName(archivePath);
                        if (settings.AcceleratedDownloads)
                        {

                            DownloadResult downloadResult = await DownloadManager.Download(archivePath, Directory.GetCurrentDirectory(), settings.MaxConnections, ArchiveOctaneClient_DownloadProgressChanged);
                            SetStatus($"Download Took: {downloadResult.TimeTaken}");
                            ArchiveDownloadCompleted(new FileDownloadInfo(fileName, version, replaceAsLatest));
                        }
                        else
                        {
                            using (WebClient archiveWebClient = new WebClient())
                            {

                                archiveWebClient.DownloadProgressChanged += ArchiveWebClient_DownloadProgressChanged;
                                archiveWebClient.DownloadFileCompleted += ArchiveWebClient_DownloadFileCompleted;
                                stopwatch.Start();
                                archiveWebClient.DownloadFileAsync(new Uri(archivePath), Directory.GetCurrentDirectory() + "\\" + fileName, new FileDownloadInfo(Directory.GetCurrentDirectory() + "\\" + fileName, version, replaceAsLatest));
                            }
                        }
                    }

                    if (anonResponse.IsSuccessStatusCode && !gitResponse.IsSuccessStatusCode)
                    {
                        var content = await anonResponse.Content.ReadAsStringAsync();
                        var pattern = new Regex("https://cdn.*.anonfiles.com/.*/(YuzuEA-.*.7z)");

                        var match = pattern.Match(content);

                        if (match.Success)
                        {
                            String address = match.Groups[0].Value;
                            String fileName = match.Groups[1].Value;
                            if (settings.AcceleratedDownloads)
                            {

                                DownloadResult downloadResult = await DownloadManager.Download(address, Directory.GetCurrentDirectory(), settings.MaxConnections, ArchiveOctaneClient_DownloadProgressChanged);
                                SetStatus($"Download Took: {downloadResult.TimeTaken}");
                                ArchiveDownloadCompleted(new FileDownloadInfo(fileName, version, replaceAsLatest));
                            }
                            else
                            {
                                using (WebClient archiveWebClient = new WebClient())
                                {
                                    
                                    archiveWebClient.DownloadProgressChanged += ArchiveWebClient_DownloadProgressChanged;
                                    archiveWebClient.DownloadFileCompleted += ArchiveWebClient_DownloadFileCompleted;
                                    stopwatch.Start();
                                    archiveWebClient.DownloadFileAsync(new Uri(address), Directory.GetCurrentDirectory() + "\\" + fileName, new FileDownloadInfo(Directory.GetCurrentDirectory() + "\\" + fileName, version, replaceAsLatest));
                                }
                            }
                        }
                    }
                }
                else
                {
                    SetStatusAndProgress("Please select a valid version to download.", 0);
                    SetControlsEnabled(true);
                }
            }
            catch (Exception e)
            {
                SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
                SetControlsEnabled(true);
            }
        }

        private void ArchiveOctaneClient_DownloadProgressChanged(int progress)
        {
            SetStatus($"Accelerated Downloading from Pineapple ({progress})%");
            SetProgress(progress);
        }

        private void DownloadProgressChanged(int progress)
        {
            SetStatus($"Accelerated Downloading from Pastebin ({progress})%");
            SetProgress(progress);
        }

        private void ArchiveDownloadCompleted(FileDownloadInfo info)
        {
            stopwatch.Reset();
            ZipArchive archive = null;
            try
            {
                SetStatus("Extracting files..\r\nThis may take a while..");
                SevenZipExtractor extractor = new SevenZipExtractor(Directory.GetCurrentDirectory() + "\\" + info.fileName);
                if (extractor.TestArchive())
                {
                    extractor.ProgressUpdated += Extractor_ProgressUpdated;
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + info.version))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + info.version);
                    }
                    extractor.ExtractAll(Directory.GetCurrentDirectory() + "\\" + info.version, true);

                    if (info.replaceAsLatest)
                    {
                        string path = GetInstalledVersionPath(info.version);
                        if (path.Length > 0)
                        {
                            // Remove the yuzu.exe from the path
                            path = path.Substring(0, path.Length - 8);
                        }
                        SetStatusAndProgress("Transferring Files", 75);
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Yuzu Early Access\\");
                        Copy(path, Directory.GetCurrentDirectory() + "\\Yuzu Early Access\\");
                        settings.Version = info.version;
                        settings.Save();
                    }
                    SetStatusAndProgress("Cleaning Up", 75);
                    File.Delete(Directory.GetCurrentDirectory() + "\\" + info.fileName);
                    SetStatusAndProgress("Done!", 100);
                    Invoke(new MethodInvoker(() =>
                    {
                        InstallLaunchButton.Text = "Launch";
                    }));
                    FinishUp();
                }
                else
                {
                    SetStatusAndProgress("The archive file was invalid, please try again later.", 0);
                    SetControlsEnabled(true);
                }
            }
            catch (Exception ex)
            {
                SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                if (null != archive)
                {
                    archive.Dispose();
                }
                logger.Log(LogLevel.ERROR, ex.Message);
                logger.Log(LogLevel.ERROR, ex.StackTrace);
                SetControlsEnabled(true);
            }
        }
        private void ArchiveWebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            string speedInKB = string.Format("{0} kb/s", (e.BytesReceived / 1024d / stopwatch.Elapsed.TotalSeconds).ToString("0.00"));

            double bytesPerSecond = e.BytesReceived / stopwatch.Elapsed.TotalSeconds;
            long bytesLeft = e.TotalBytesToReceive - e.BytesReceived;
            long secondsRemaining = (long)(bytesLeft / bytesPerSecond);
            long minutesRemaining = secondsRemaining / 60;
            secondsRemaining -= (minutesRemaining * 60);
            string timeRemaining = string.Format("{0}m {1}s", minutesRemaining, secondsRemaining);
            string percentage = e.ProgressPercentage.ToString() + "%";
            SetStatus($"Downloading from Pineapple\r\n{speedInKB} ( {percentage} )\r\nTime Remaining: {timeRemaining}");
            SetProgress(e.ProgressPercentage);
        }

        private void ArchiveWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            FileDownloadInfo info = e.UserState as FileDownloadInfo;
            ArchiveDownloadCompleted(info);
        }

        private void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            string speedInKB = string.Format("{0} kb/s", (e.BytesReceived / 1024d / stopwatch.Elapsed.TotalSeconds).ToString("0.00"));

            double bytesPerSecond = e.BytesReceived / stopwatch.Elapsed.TotalSeconds;
            long bytesLeft = e.TotalBytesToReceive - e.BytesReceived;
            long secondsRemaining = (long)(bytesLeft / bytesPerSecond);
            long minutesRemaining = secondsRemaining / 60;
            secondsRemaining -= (minutesRemaining * 60);
            string timeRemaining = string.Format("{0}m {1}s", minutesRemaining, secondsRemaining);
            string percentage = e.ProgressPercentage.ToString() + "%";
            SetStatus($"Downloading from Pastebin...\r\n{speedInKB} ( {percentage} )\r\nTime Remaining: {timeRemaining}");
            SetProgress(e.ProgressPercentage);
        }

        
        private void SetStatusAndProgress(String status, int value)
        {
            try
            {
                if (StatusLabel.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        StatusLabel.Text = status;
                        DownloadInstallProgressBar.Value = value;
                        logger.Log(status);
                    }));
                }
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
            }
        }

        private void SetStatus(String status)
        {
            if (!StatusLabel.IsDisposed && StatusLabel.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    StatusLabel.Text = status;
                }));
            }
        }
        private void SetProgress(int value)
        {
            if (!DownloadInstallProgressBar.IsDisposed && DownloadInstallProgressBar.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    DownloadInstallProgressBar.Value = value;
                }));
            }
        }

        private void Extractor_ProgressUpdated(int progress)
        {
            SetProgress(progress);
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
           try
            {
                FileDownloadInfo info = e.UserState as FileDownloadInfo;
                DownloadCompleted(info);
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.ERROR, ex.Message);
                logger.Log(LogLevel.ERROR, ex.StackTrace);
            }
        }

        private void DownloadCompleted(FileDownloadInfo info)
        {
            ZipArchive archive = null;
            try
            {
                SetStatusAndProgress("Extracting files..\r\nThis may take a while..", 0);
                SevenZipNET.SevenZipExtractor extractor = new SevenZipNET.SevenZipExtractor(info.fileName);
                if (extractor.TestArchive())
                {
                    extractor.ProgressUpdated += Extractor_ProgressUpdated;
                    extractor.ExtractAll(Directory.GetCurrentDirectory(), true);
                    SetStatusAndProgress("Cleaning Up", 75);
                    File.Delete(info.fileName);
                    settings.Version = info.version;
                    settings.Save();
                    SetStatusAndProgress("Done!", 100);
                    Invoke(new MethodInvoker(() =>
                    {
                        InstallLaunchButton.Text = "Launch";
                    }));
                    FinishUp();
                }
                else
                {
                    SetStatusAndProgress("The archive file was invalid, please try again later.", 0);
                    SetControlsEnabled(true);
                    logger.Log("The archive file was invalid, please try again later.");
                }
            }
            catch (Exception ex)
            {
                SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                if (null != archive)
                {
                    archive.Dispose();
                }
                logger.Log(LogLevel.ERROR, ex.Message);
                logger.Log(LogLevel.ERROR, ex.StackTrace);
                SetControlsEnabled(true);
            }
        }

        private void FinishUp()
        {
            try
            {
                if (settings.AutoLaunch)
                {
                    LaunchEmulator();
                }
            }
            catch (Exception e)
            {
                SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                logger.Log(LogLevel.ERROR, "Couldn't start the emulator!");
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
            }
            if (settings.AutoClose)
            {
                try
                {
                    Application.Exit();
                }
                catch (Exception e)
                {
                    SetStatusAndProgress("An error occurred, please check the logs for the issue", 0);
                    logger.Log(LogLevel.ERROR, "Couldn't auto-close!");
                    logger.Log(LogLevel.ERROR, e.Message);
                    logger.Log(LogLevel.ERROR, e.StackTrace);
                }
            }

            SetControlsEnabled(true);

        }

        private void SetControlsEnabled(bool enabled)
        {
            Invoke(new MethodInvoker(() =>
                {
                    VersionDropdown.Enabled = enabled;
                    InstallLaunchButton.Enabled = enabled;
                }));
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await DownloadAvailableArchivedVersions();

                Invoke(new MethodInvoker(() =>
                {
                    if (VersionDropdown.Items.Contains(settings.DesiredVersion))
                    {
                        VersionDropdown.SelectedItem = settings.DesiredVersion;
                    }
                    else
                    {
                        VersionDropdown.SelectedIndex = 0;
                    }
                }));

                if (settings.DesiredVersion.Equals("Latest"))
                {
                    await CheckForUpdates();
                }
                else if (!settings.DesiredVersion.Equals("Latest") && settings.AutoLaunch)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        var version = VersionDropdown.SelectedItem.ToString();
                        if (IsVersionInstalled(version))
                        {
                            LaunchEmulator();
                        }
                        else
                        {
                            Task.Run(async () =>
                            {
                                await DownloadVersion(version, false);
                            });
                        }
                    }));
                }
            });
        }

        private void InstallLaunchButton_Click(object sender, EventArgs e)
        {
            string version = VersionDropdown.SelectedItem.ToString();
            if (version.Equals("Latest") || IsVersionInstalled(version))
            {
                LaunchEmulator();
            }
            else
            {
                Task.Run(async () =>
                {
                    await DownloadVersion(version, false);
                });
            }
        }

        private void VersionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VersionDropdown.SelectedItem.ToString().Equals("Latest"))
            {
                settings.DesiredVersion = "Latest";
                InstallLaunchButton.Text = "Launch";
            }
            else
            {
                if (IsVersionInstalled(VersionDropdown.SelectedItem.ToString()))
                {
                    InstallLaunchButton.Text = "Launch";
                }
                else
                {
                    InstallLaunchButton.Text = "Install";
                }

                settings.DesiredVersion = VersionDropdown.SelectedItem.ToString();
            }

            settings.Save();
        }

        private bool IsVersionInstalled(string version)
        {
            return File.Exists(Directory.GetCurrentDirectory() + "\\" + VersionDropdown.SelectedItem.ToString() + "\\" + "yuzu-windows-msvc-early-access" + "\\" + "yuzu.exe") || File.Exists(Directory.GetCurrentDirectory() + "\\" + version + "\\" + "Yuzu Early Access " + version + "\\" + "Yuzu Early Access\\yuzu.exe") || File.Exists(Directory.GetCurrentDirectory() + "\\" + version + "\\" + "YuzuEA-" + version + "\\" + "yuzu.exe");
        }

        private string GetInstalledVersionPath(string version)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + version + "\\" + "yuzu-windows-msvc-early-access" + "\\yuzu.exe"))
            {
                return Directory.GetCurrentDirectory() + "\\" + version + "\\" + "yuzu-windows-msvc-early-access" + "\\yuzu.exe";
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\" + version + "\\" + "Yuzu Early Access " + version + "\\" + "Yuzu Early Access\\yuzu.exe"))
            {
                return Directory.GetCurrentDirectory() + "\\" + version + "\\" + "Yuzu Early Access " + version + "\\" + "Yuzu Early Access\\yuzu.exe";
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\" + version + "\\" + "YuzuEA-" + version + "\\yuzu.exe"))
            {
                return Directory.GetCurrentDirectory() + "\\" + version + "\\" + "YuzuEA-" + version + "\\yuzu.exe";
            }
            else
            {
                return "";
            }
        }

        private void LaunchEmulator()
        {
            string emulatorPath;
            if (settings.DesiredVersion.Equals("Latest"))
            {
                emulatorPath = Directory.GetCurrentDirectory() + "\\Yuzu Early Access\\yuzu.exe";
            }
            else
            {
                emulatorPath = GetInstalledVersionPath(settings.DesiredVersion);
            }
            Process yuzuEmulator = new Process();
            yuzuEmulator.StartInfo.FileName = emulatorPath;
            if (settings.LaunchAsAdmin)
            {
                yuzuEmulator.StartInfo.UseShellExecute = true;
                yuzuEmulator.StartInfo.Verb = "runas";
            }
            try
            {
                yuzuEmulator.Start();
            }
            catch(Exception e)
            {
                logger.Log(LogLevel.ERROR, e.Message);
                logger.Log(LogLevel.ERROR, e.StackTrace);
            }
        }

        private void Copy(string sourceDirectory, string targetDirectory, string message = "Copying Files...", string endMessage = "Done")
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            SetStatus(message);
            CopyAll(diSource, diTarget);
            SetStatus(endMessage);
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            settingsView.ShowDialog();
        }
    }
}
