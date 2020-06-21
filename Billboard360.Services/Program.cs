using System;
using System.Threading;

namespace Billboard360.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Console.ReadLine();
        }

        public static void TimerCallback(object o)
        {
            // Display the date/time when this method got called.
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
            // Force a garbage collection to occur for this demo.
            tes();

            GC.Collect();
        }

        public static void tes()
        {
            Console.WriteLine("tes");
        }

        
    }
}
