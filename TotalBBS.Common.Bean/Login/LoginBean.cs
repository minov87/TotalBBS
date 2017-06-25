using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalBBS.Common.Bean.Login
{
    public class LoginBean
    {
        public int intIdx { get; set; }

        public string strAdminId { get; set; }

        public string strAdminPw { get; set; }

        public string strAdminName { get; set; }

        public string chrAdminAuth { get; set; }

        public string chrCheckLogin { get; set; }

        public string dateRegDate { get; set; }
    }
}