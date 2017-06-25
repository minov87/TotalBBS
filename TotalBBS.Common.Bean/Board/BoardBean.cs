using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalBBS.Common.Bean.Board
{
    public class BoardBean
    {
        public int intIdx { get; set; }

        public int intBoardCategory { get; set; }

        public int intWriteCategory { get; set; }

        public string strUserId { get; set; }

        public string strWriter { get; set; }

        public string strSubject { get; set; }

        public string strContent { get; set; }

        public string dateRegDate { get; set; }

        public string dateModDate { get; set; }

        public string chrDeleteFlag { get; set; }

        public int intViewCount { get; set; }
    }
}