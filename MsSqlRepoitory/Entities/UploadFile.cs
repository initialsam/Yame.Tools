using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class UploadFile
    {
        public int FileId { get; set; }
        public string OriginalName { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
        public bool IsCover { get; set; }
        public int ProjectInfoId { get; set; }
        public virtual ProjectInfo ProjectInfo { get; set; }

    }
}
