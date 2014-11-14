using System;
using System.Collections.Generic;
using System.Globalization;
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
using TnaWant.DataBase;

namespace TnaWant.VisualManager
{
    /// <summary>
    /// Interaction logic for TnaWantTest.xaml
    /// </summary>
    public partial class TnaWantTest : Window
    {
        public TnaWantTest()
        {
            InitializeComponent();
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            //using (TnaWantDB tnaWant= new TnaWantDB(@"D:\test.dat"))
            //{
            //    DateTime now = DateTime.Now;

            //    RecordSet<string> set = tnaWant.OpenRecordSet<string>();

            //    int cnt = int.Parse(textBoxLoop.Text);
            //    int id = 0;
            //    for (int i = 0; i < cnt; i++)
            //    {
            //        id = int.Parse(textBoxNo.Text);
            //        if (id == 0)
            //        {
            //            id = new Random().Next(100, 100000);
            //        }

            //        set.Save(id, textBoxString.Text);
            //    }

            //    textBlockString.Text = id.ToString();
            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }
        
        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            //using (TnaWantDB tnaWant = new TnaWantDB(@"D:\test.dat"))
            //{
            //    DateTime now = DateTime.Now;

            //    RecordSet<string> set = tnaWant.OpenRecordSet<string>();

            //    int cnt = int.Parse(textBoxNo.Text);

            //    textBlockString.Text = set.Get(cnt);

            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }
        
        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            //using (TnaWantDB tnaWant = new TnaWantDB(@"D:\test.dat"))
            //{
            //    DateTime now = DateTime.Now;

            //    RecordSet<string> set = tnaWant.OpenRecordSet<string>();

            //    int cnt = int.Parse(textBoxNo.Text);

            //    string str = set.Delete(cnt);
            //    textBlockString.Text = str;

            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }

        private void ButtonBase4_OnClick(object sender, RoutedEventArgs e)
        {
            //using (TnaWantDB tnaWant = new TnaWantDB(@"D:\test.dat"))
            //{
            //    DateTime now = DateTime.Now;

            //    RecordSet<string> set = tnaWant.OpenRecordSet<string>();

            //    int cnt = int.Parse(textBoxNo.Text);

            //    bool bln = set.Update(cnt, textBoxString.Text);
            //    textBlockString.Text = bln.ToString(CultureInfo.InvariantCulture);

            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            textBlockLen.Text = (sender as TextBox).Text.Length.ToString();
        }
    }
}
