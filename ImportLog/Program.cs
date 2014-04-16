using System;
using System.Collections.Generic;
using System.Text;

namespace ImportLog
{
    class Program
    {
        static void Main(string[] args)
        {
            String strPath = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"];

            String logFileName = String.Format("ex{0:yyMMdd}.log", DateTime.Now.AddDays(-1));

            if (args.Length > 0)
            {
                logFileName = String.Format("ex{0}.log", args[0]);
            }

            logFileName = strPath + (strPath.EndsWith(@"\") ? "" : "\\") + logFileName;

            String strNewLog = LogUtil.ParseLog(logFileName);

            LogUtil.BulkImportLog(strNewLog);

            System.IO.File.Delete(strNewLog);
        }
    }
}
