using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TotalBBS.Common.Dac
{
    public class Global
    {
        public static string DataBaseConnection
        {
            get { return ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString(); }
        }

        public static string ErrorLoggerDataBaseConnection
        {
            get { return ConfigurationManager.ConnectionStrings["ErrorLoggerDataBaseConnection"].ToString(); }
        }

        public static string ERPDataBaseConnection
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDataBaseConnection"].ToString(); }
        }
    }
}