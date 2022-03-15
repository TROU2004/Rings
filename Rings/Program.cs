using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace Rings
{
    class Program
    {
        static ISchedulerFactory SchedulerFactory;
        static IScheduler Scheduler;

        private static async Task Main(string[] args)
        {
            SchedulerFactory = new StdSchedulerFactory();
            Scheduler = await SchedulerFactory.GetScheduler();
            await Scheduler.Start();
            Console.WriteLine("通化一中打铃系统 20220315 版本");
            CreateRing(7, 10, "ring");
            CreateRing(7, 50, "over");
            CreateRing(8, 0, "ring");
            CreateRing(8, 40, "over");
            CreateRing(8, 50, "ring");
            CreateRing(9, 30, "eye");
            CreateRing(10, 0, "ring");
            CreateRing(10, 40, "over");
            CreateRing(10, 50, "ring");
            CreateRing(11, 30, "over");
            CreateRing(13, 20, "ring");
            CreateRing(14, 0, "eye");
            CreateRing(14, 15, "ring");
            CreateRing(14, 55, "exercise");
            CreateRing(15, 25, "ring");
            CreateRing(16, 30, "over");
            CreateRing(17, 30, "ring");
            CreateRing(18, 50, "over");
            CreateRing(19, 0, "ring");
            CreateRing(20, 0, "over");
            CreateRing(20, 10, "ring");
            CreateRing(21, 0, "over");
            while (true) { Console.ReadKey(); }
        }

        private static async void CreateRing(int hour, int minute, string file)
        {
            Console.WriteLine($"创建打铃任务, 于 {(hour < 10 ? "0" + hour : hour)} 点 {(minute == 0 ? "00" : minute)} 分, 预定打铃任务 {file}");
            var jobDetail = JobBuilder.Create<RingJob>().UsingJobData("file", file).Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour, minute))
                .Build();
            await Scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
