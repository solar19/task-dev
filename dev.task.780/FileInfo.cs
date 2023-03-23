using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.task._780
{
    public class FileInfo
    {
        public string URL { get; set; } = "";
        public string LocalName { get; set; } = "";
        public string FileExtension { get; set; } = "";
        public long? FileSize { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
