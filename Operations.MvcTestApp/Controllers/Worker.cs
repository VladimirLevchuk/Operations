using System;
using System.Threading;
using System.Threading.Tasks;

namespace Operations.MvcTestApp.Controllers
{
    public class Worker
    {
        public static void DoWork()
        {
            using (Op.Start("async work"))
            {
                Thread.Sleep(1000);
            }
        }

        public static Task DoWorkAsync()
        {
            return Task.Run(() => DoWork());
        }

        public static void DoWorkWithError()
        {
            throw new Exception("Worker error");
        }

        public static Task DoWorkWithErrorAsync()
        {
            return Task.Run(() => DoWorkWithError());
        }
    }
}