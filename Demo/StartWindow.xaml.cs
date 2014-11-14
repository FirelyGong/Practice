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
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            textBlockThread.Text = "Current UI Thread: " + Thread.CurrentThread.ManagedThreadId;
        }

        private void Async_Click(object sender, RoutedEventArgs e)
        {
            AsyncTestWindow window = new AsyncTestWindow();
            window.Show();
        }

        private void Alg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window=new MainWindow();
            window.Show();
        }

        private void Ui_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Loaded += (s, ex) =>
            {
                VisualHost vh = new VisualHost();
                HostVisual hostVisual = new HostVisual();
                vh.Child = hostVisual;
                win.Content = vh;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    VisualTarget visualTarget = new VisualTarget(hostVisual);
                    DrawingVisual dv = new DrawingVisual();
                    using (var dc = dv.RenderOpen())
                    {
                        dc.DrawText(new FormattedText("UI from another UI thread : "+ Thread.CurrentThread.ManagedThreadId,
                            System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface("Verdana"),
                            32,
                            Brushes.Black), new Point(10, 0));
                    }

                    visualTarget.RootVisual = dv;

                    System.Windows.Threading.Dispatcher.Run();  //启动Dispatcher
                }));

                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
            };

            win.Show();
        }
    }
}
