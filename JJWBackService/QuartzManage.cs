using Quartz;
using Quartz.Impl;

public class QuartzManage
{
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static IScheduler sched = null;

    /// <summary>
    /// start quartz service
    /// </summary>
    public static void start()
    {
        logger.Info("进入start");

        if (sched == null)
        {
            logger.Info("sf==null");
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();

            logger.Info("初始化Quartz组件...");
        }

        sched.Start();

        logger.Info("Quartz启动完成.");

        JobDetail job = new JobDetail("jobService", "groupService", typeof(SyncPriceJob));

        CronTrigger trigger = new CronTrigger("triggerService", "groupService", "jobService", "groupService", System.Configuration.ConfigurationManager.AppSettings["RunService"]);

        sched.ScheduleJob(job, trigger);
    }

    /// <summary>
    /// clear all job listener
    /// </summary>
    public static void clear()
    {
        Quartz.Collection.ISet jobs = sched.JobListenerNames;

        for (int i = 0; i < jobs.Count; i++)
        {
            string jobName = ((IJobListener)jobs[i]).Name;

            sched.RemoveJobListener(jobName);

            logger.InfoFormat("清除Quartz作业：{0}", jobName);
        }

        sched = null;
    }

    /// <summary>
    /// execute job dispatcher. 
    /// </summary>
    public class SyncPriceJob : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            logger.InfoFormat("开始执行Quartz作业...");
            SyncService service = new SyncService();
            service.StartSync(System.DateTime.Now.Date);
            logger.InfoFormat("Quartz作业执行完成。");
        }
    }
}
