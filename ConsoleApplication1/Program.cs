using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> test = (a, b) => a + b;

            var langs = new[] { "C", "C++", "C#" };
            var query = langs.AsQueryable().Where(l => l.EndsWith("#"));

            // The asynchronous method puts the thread id here.
            int threadId;

            // Create an instance of the test class.
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(3000,
                out threadId, null, null);

            DoWork(10000000);
            Console.WriteLine("Main thread {0} does some work. at [{1}]",
                Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            
            DoWork(100000000);

            //Thread.Sleep(4000);
            // Wait for the WaitHandle to become signaled.
            //result.AsyncWaitHandle.WaitOne();

            // Perform additional processing here.
            // Call EndInvoke to retrieve the results.
            string returnValue = caller.EndInvoke(out threadId, result);
            
            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            Console.WriteLine("The call executed on thread {0}, with return value \"{1}\". time [{2}]",
                threadId, returnValue, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));

            Console.ReadLine();
        }

        private static void DoWork(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double d = Math.Sqrt(i);
            }
        }
    }
}
