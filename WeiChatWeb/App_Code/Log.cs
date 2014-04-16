using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///Log 的摘要说明
/// </summary>
public class Log
{
    public Log()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private static LogUtil.Level _logLevel = LogUtil.Level.Info;

    private static bool hasLoad = false;

    public static void debug(String logContent)
    {
        if (GetLogLevel() >= LogUtil.Level.Debug)
        {
            SaveLog(LogUtil.Level.Debug, logContent);
        }
    }
    public static void info(String logContent)
    {
        if (GetLogLevel() >= LogUtil.Level.Info)
        {
            SaveLog(LogUtil.Level.Info, logContent);
        }
    }

    public static void warn(String logContent)
    {
        if (GetLogLevel() >= LogUtil.Level.Warn)
        {
            SaveLog(LogUtil.Level.Warn, logContent);
        }
    }

    public static void error(String logContent)
    {
        if (GetLogLevel() >= LogUtil.Level.Error)
        {
            SaveLog(LogUtil.Level.Error, logContent);
        }
    }

    public static void fatal(String logContent)
    {
        if (GetLogLevel() >= LogUtil.Level.Fatal)
        {
            SaveLog(LogUtil.Level.Fatal, logContent);
        }
    }


    /// <summary>
    /// 获取当期配置的日志级别
    /// </summary>
    /// <returns></returns>
    internal static LogUtil.Level GetLogLevel()
    {
        if (!hasLoad)
        {
            String strLevel = System.Configuration.ConfigurationManager.AppSettings["LogLevel"];

            if (!String.IsNullOrEmpty(strLevel))
            {
                int i = 0;
                if (Int32.TryParse(strLevel, out i))
                {
                    if (i >= 1 && i <= 5)
                    {
                        _logLevel = (LogUtil.Level)i;
                    }
                }
            }
            else
            {
                _logLevel = LogUtil.Level.Info;
            }

            hasLoad = true;
        }
        return _logLevel;
    }


    internal static void SaveLog(LogUtil.Level level, String logContent)
    {
        model.SysLog log = new model.SysLog();
        log.Created = DateTime.Now;
        log.LogContent = logContent;
        log.LogLevel = (int)level;
        log.UserGUID = Util.GetLoginUserInfo().UserGUID;
        log.UserName = Util.GetLoginUserInfo().UserName;
        log.RemoteIP = HttpContext.Current.Request.ServerVariables["Remote_Addr"];

        OR.Control.DAL.Add<model.SysLog>(log, false);
    }
}