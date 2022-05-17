using System;
using System.Threading;
using System.Threading.Tasks;
namespace SUTD_UROP
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {

            Console.Title = "UROP Game Server";
            Console.WriteLine("Hello World!");
            Server.Start(2, 25000);
            isRunning = true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;
            InputHandle.ServerInput();
            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
       
    }
}
