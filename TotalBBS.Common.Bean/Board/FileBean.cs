using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TotalBBS.Common.Bean.Board
{
    public class FileBean
    {
        public int intIdx { get; set; }

        public int intBBSIdx { get; set; }

        public string strRealFileName { get; set; }

        public string strFileName { get; set; }

        public string strFilePrefix { get; set; }

        public int intFileSize { get; set; }

        public int intFileSort { get; set; }

        public string strFilePath { get; set; }
    }
}