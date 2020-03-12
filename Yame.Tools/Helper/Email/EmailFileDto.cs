using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yame.Tools.Helper
{
    public class EmailFileDto
    {
        public string FileName { get; set; }
        public MemoryStream MemoryStream { get; set; }
    }
}
