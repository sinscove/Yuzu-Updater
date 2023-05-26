using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuzu_Updater
{
    internal struct FileDownloadInfo
    {
        public string fileName { get; set; }
        public string version { get; set; }
        public bool replaceAsLatest { get; set; }

        public FileDownloadInfo(string fileName, string version, bool replaceAsLatest)
        {
            this.fileName = fileName;
            this.version = version;
            this.replaceAsLatest = replaceAsLatest;
        }
    }
}