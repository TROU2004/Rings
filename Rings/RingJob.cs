using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Rings
{
    public class RingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var file = context.JobDetail.JobDataMap.GetString("file");
                Console.WriteLine(DateTime.Now.ToString() + $" 执行打铃 {file}");
                new SoundPlayer(Path.Combine(Directory.GetCurrentDirectory(), $"{file}.wav")).Play();
        });
        }
    }
}
