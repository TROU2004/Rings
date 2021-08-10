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
        readonly static string PATH = Path.Combine(Directory.GetCurrentDirectory(), "ring.wav");
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("打铃: {0}", DateTime.Now.ToString());
                new SoundPlayer(PATH).Play();
        });
        }
    }
}
