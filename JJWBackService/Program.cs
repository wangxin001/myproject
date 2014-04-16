using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace JJWBackService
{
    static class Program
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new mainService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
