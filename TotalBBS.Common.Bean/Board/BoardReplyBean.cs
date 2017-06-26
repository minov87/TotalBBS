using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBBS.Common.Bean.Board
{
    public class BoardReplyBean
    {
        public int intIdx { get; set; }

        public int intParentIdx { get; set; }

        public int intBBSIdx { get; set; }

        public int intDepth { get; set; }

        public string strUserId { get; set; }

        public string strWriter { get; set; }

        public string strContent { get; set; }

        public string dateRegDate { get; set; }
    }
}
