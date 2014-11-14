using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Practice.DataBase;
using Practice.DataBase.UserInterface;

namespace Demo
{
    /// <summary>
    /// Interaction logic for DemoView.xaml
    /// </summary>
    public partial class DemoView : Window
    {
        public DemoView()
        {
            InitializeComponent();
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {

            long key = 0;
            using (DBEngine dataEngine = new DBEngine(@"D:\test.dat", @"D:\test.sys"))
            {
                //if (!dataEngine.Initialized)
                //{
                //    dataEngine.Configure(50, comboBox.SelectedIndex == 0 ? true : false);
                //}

                DateTime now = DateTime.Now;

                //Console.Out.WriteLine("1."+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));

                //byte[] buffer = new byte[textBoxString.Text.Length+1];
                //using (MemoryStream ms = new MemoryStream(buffer))
                //{
                //    using (BinaryWriter writer=new BinaryWriter(ms))
                //    {
                //        writer.Write(textBoxString.Text);
                //    }
                //}
                DateTime dtToday = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                int cnt = 0;
                RecordSet recordSet = dataEngine.OpenRecordSet("Student");
                for (int i = 0; i < cnt; i++)
                {
                    key = new Random(GetRandomSeed(dtToday, i)).Next(0, int.MaxValue);
                    recordSet.Save(key, txtName.Text + key);
                }

                label.Content = TimeSpan.FromMilliseconds((DateTime.Now - now).TotalMilliseconds).ToString();
                //textBlockString.Text = key.ToString();
            }

            IList<Student> lst = new List<Student>();
            lst.Add(
                new Student
                {
                    Name=txtName.Text,
                    Age=txtAge.Text,
                    Class=txtClass.Text,
                    Teacher=txtTeacher.Text
                });

            //dataGrid.DataContext = lst;
            
            dataGrid.ItemsSource = lst;
        }

        private int GetRandomSeed(DateTime fromTime, int seq)
        {
            return (int)(DateTime.Now.Ticks - fromTime.Ticks) + seq;
        }
    }
}
