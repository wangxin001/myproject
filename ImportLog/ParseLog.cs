using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ImportLog
{
    public static class LogUtil
    {
        public static String ParseLog(String strLogFile)
        {
            String strNewLogFile = strLogFile + ".txt";

            if (File.Exists(strNewLogFile))
            {
                File.Delete(strNewLogFile);
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(strLogFile, Encoding.ASCII);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(strNewLogFile, true, Encoding.ASCII, 1024 * 200);

            String[] strLogLevel = System.Configuration.ConfigurationManager.AppSettings["LogLevel"].Split(',');

            String strFormat = System.Configuration.ConfigurationManager.AppSettings["LogFormat"];

            while (!reader.EndOfStream)
            {
                String strLog = reader.ReadLine();

                // 当前是空行，跳过不处理
                if (String.IsNullOrEmpty(strLog))
                {
                    continue;
                }

                // 当前行是注释行，不处理
                if (strLog.StartsWith("#"))
                {
                    continue;
                }

                String[] log = strLog.Split(' ');

                if (log.Length != 16)
                {
                    continue;
                }

                // 只导入aspx的页面，其他的跳过不处理
                String strUrl = log[5].Split('?')[0];

                for (int i = 0; i < strLogLevel.Length; i++)
                {
                    if (strUrl.EndsWith(strLogLevel[i]))
                    {
                        writer.WriteLine(String.Format(strFormat, log));
                        break;
                    }
                }
            }

            writer.Close();

            return strNewLogFile;
        }

        public static void BulkImportLog(String strLogFile)
        {
            String strFormatFile = System.Windows.Forms.Application.StartupPath;
            strFormatFile = strFormatFile + (strFormatFile.EndsWith("\\") ? "" : "\\") + "LogFormat.xml";

            String strSQLFormat = " BULK INSERT PvLog FROM '{0}' WITH (FORMATFILE = '{1}'); ";

            String strSQL = String.Format(strSQLFormat, strLogFile, strFormatFile);

            // Console.Out.WriteLine(strSQL);

            OR.DB.SQLHelper.ExecuteSql(strSQL);

            strSQL = "Update PvLog Set  ";
        }
    }
}
