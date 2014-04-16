using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace JJWBackService
{
    public partial class mainService : ServiceBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public mainService()
        {
            logger.Info("开始初始化服务...");
            InitializeComponent();
            logger.Info("初始化服务完成");
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("服务启动 OnStart");
            QuartzManage.start();
        }

        protected override void OnStop()
        {
            logger.Info("服务停止 OnStop");
            QuartzManage.clear();
        }
    }
}
