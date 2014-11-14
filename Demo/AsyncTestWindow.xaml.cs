using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Demo
{
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);

    /// <summary>
    /// Interaction logic for AsyncTestWindow.xaml
    /// </summary>
    public partial class AsyncTestWindow : Window
    {
        public AsyncTestWindow()
        {
            InitializeComponent();

            listBox.Items.Add("Current UI Thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add("current thread:" + Thread.CurrentThread.ManagedThreadId + ", start at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            AsyncMethodCaller caller = new AsyncMethodCaller(AsyncTestMethod);
            int threadId;

            IAsyncResult ia = caller.BeginInvoke(4000, out threadId, new AsyncCallback(
                obj =>
                {
                    string str = caller.EndInvoke(out threadId, obj);
                    int callbackThread = Thread.CurrentThread.ManagedThreadId;
                    this.Dispatcher.Invoke(
                        () =>
                        {
                            listBox.Items.Add(
                                "call back thread: " + callbackThread + ", at "
                                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));

                            listBox.Items.Add(str + ", thread: "+ threadId + ", at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                        });
                }), null);

            listBox.Items.Add("current thread:" + Thread.CurrentThread.ManagedThreadId + ", end at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //Thread.Sleep(3000);

            //string str = caller.EndInvoke(out threadId, ia);
            //listBox.Items.Add(str + ", end at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add("current thread:" + Thread.CurrentThread.ManagedThreadId + ", start at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            
            int threadId;


            Thread.Sleep(3000);

            string str = AsyncTestMethod(4000, out threadId);

            listBox.Items.Add(str + ", end at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        }

        private string AsyncTestMethod(int callDuration, out int threadId)
        {
            //Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }

    public class AsyncResult : IAsyncResult
    {/// <summary>
        /// Event to block execution till the asynchronous DTM operation is finished.
        /// </summary>
        private volatile ManualResetEvent _WaitEvent;/// <summary>
        /// 
        /// The user-defined object that qualifies or contains information about an asynchronous DTM operation.
        /// </summary>
        private object _AsyncState;

        public AsyncResult(object state)
        {
            _AsyncState = state;
        }

        public object AsyncState
        {
            get
            {
                return _AsyncState;
            }
        }

        public void SetAsyncState(object obj)
        {
            _AsyncState = obj;
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (this._WaitEvent == null)
                {
                    bool isCompleted = this.IsCompleted;
                    ManualResetEvent manualResetEvent = new ManualResetEvent(isCompleted);
                    if (Interlocked.CompareExchange<ManualResetEvent>(ref this._WaitEvent, manualResetEvent, null) == null)
                    {
                        if (!isCompleted && this.IsCompleted)
                        {
                            manualResetEvent.Set();
                        }
                    }
                    else
                    {
                        manualResetEvent.Close();
                    }
                }
                return this._WaitEvent;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        public bool IsCompleted
        {
            get;
            private set;
        }

        public void SetCompleted()
        {
            IsCompleted = true;
        }
    }
}
