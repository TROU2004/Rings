using Quartz;
using Quartz.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;

namespace Rings
{
    class Program
    {
        static readonly int[,] RING_TIME_LIST = new int[,] {
            {7, 10}, {7, 50}, {8, 0}, {8, 40}, {8, 50}, {9, 30}, {10, 0}, {10, 40}, {10, 50}, {11, 30}, {13, 20}, {14, 0}, {14, 10}, {14, 50}, {15,10}, {15, 50}, {16, 0}, {16, 40}
        };

        private static async Task Main(string[] args)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();
            Console.WriteLine("任务调度器已启用(六班荣誉提供>_<), 加载了{0}个任务", RING_TIME_LIST.GetLength(0));
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "ring.wav")))
            {
                Console.WriteLine("错误: 铃声文件 ring.wav 不存在, 程序即将退出");
                return;
            }
            for (int i = 0; i < RING_TIME_LIST.GetLength(0); i++)
            {
                var jobDetail = JobBuilder.Create<RingJob>().Build();
                var hour = RING_TIME_LIST[i, 0];
                var minute = RING_TIME_LIST[i, 1];
                ITrigger trigger = TriggerBuilder.Create()
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour, minute))
                    .Build();
                await scheduler.ScheduleJob(jobDetail, trigger);
            }
            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
