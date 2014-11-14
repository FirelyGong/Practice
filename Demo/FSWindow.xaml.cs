using System;
using System.Windows;
using System.Windows.Controls;
using Practice.DataBase;
using Practice.DataBase.UserInterface;

namespace Demo
{
    /// <summary>
    /// Interaction logic for FSWindow.xaml
    /// </summary>
    public partial class FSWindow : Window
    {
        public FSWindow()
        {
            InitializeComponent();
        }

        //private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        //{
        //    using (DataEngine dataEngine = new DataEngine(@"D:\test.dat"))
        //    {
        //        if (!dataEngine.Initialized)
        //        {
        //            dataEngine.CreateDataSystem(50, comboBox.SelectedIndex == 0 ? true : false);
        //        }

        //        DateTime now = DateTime.Now;

        //        //Console.Out.WriteLine("1."+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //        int cnt = int.Parse(textBoxNo.Text);
        //        for (int i = 0; i < cnt; i++)
        //        {
        //            int ll = dataEngine.Save(textBoxString.Text);
        //        }

        //        textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
        //    }
        //}

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
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

                int cnt = int.Parse(textBoxNo.Text);
                RecordSet recordSet = dataEngine.OpenRecordSet(textBoxTable.Text);
                for (int i = 0; i < cnt; i++)
                {
                    key = new Random(GetRandomSeed(dtToday, i)).Next(0, int.MaxValue);
                    recordSet.Save(key, textBoxString.Text + key);
                }

                textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
                textBlockString.Text = key.ToString();
            }
        }

        private int GetRandomSeed(DateTime fromTime, int seq)
        {
            return (int)(DateTime.Now.Ticks - fromTime.Ticks) + seq;
        }

        //private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        //{
        //    using (DataEngine dataEngine = new DataEngine(@"D:\test.dat"))
        //    {
        //        if (!dataEngine.Initialized)
        //        {
        //            dataEngine.CreateDataSystem(50, comboBox.SelectedIndex == 0 ? true : false);
        //        }

        //        DateTime now = DateTime.Now;

        //        int cnt = int.Parse(textBoxNo.Text);
        //        textBlockString.Text = dataEngine.Read(cnt);

        //        textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
        //    }
        //}

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            using (DBEngine dataEngine = new DBEngine(@"D:\test.dat", @"D:\test.sys"))
            {
                //if (!dataEngine.Initialized)
                //{
                //    dataEngine.Configure(50, comboBox.SelectedIndex == 0 ? true : false);
                //}

                DateTime now = DateTime.Now;

                long cnt = long.Parse(textBoxNo.Text);
                ////int offset = int.Parse(textBoxOffset.Text);
                //byte[] buffer=dataEngine.Read(cnt);
                //if (buffer.Length == 0)
                //{
                //    textBlockString.Text = "null";
                //    return;
                //}

                //using (MemoryStream ms = new MemoryStream(buffer))
                //{
                //    using (BinaryReader reader = new BinaryReader(ms))
                //    {
                //        textBlockString.Text = reader.ReadString();
                //    }
                //}
                RecordSet recordSet = dataEngine.OpenRecordSet(textBoxTable.Text);
                //textBlockString.Text = recordSet.Read(cnt);
                textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            }
        }

        //private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        //{
        //    using (DataEngine dataEngine = new DataEngine(@"D:\test.dat"))
        //    {
        //        if (!dataEngine.Initialized)
        //        {
        //            dataEngine.CreateDataSystem(50, comboBox.SelectedIndex == 0 ? true : false);
        //        }

        //        DateTime now = DateTime.Now;

        //        int cnt = int.Parse(textBoxNo.Text);
        //        dataEngine.Delete(cnt);

        //        textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
        //    }
        //}

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            //using (StoreEngine dataEngine = new StoreEngine(@"D:\test.dat"))
            //{
            //    //if (!dataEngine.Initialized)
            //    //{
            //    //    dataEngine.Configure(50, comboBox.SelectedIndex == 0 ? true : false);
            //    //}

            //    DateTime now = DateTime.Now;

            //    int cnt = int.Parse(textBoxNo.Text);
            //    //int offset = int.Parse(textBoxOffset.Text);
            //    dataEngine.Delete(cnt);

            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }

        private void ButtonBase4_OnClick(object sender, RoutedEventArgs e)
        {
            //using (StoreEngine dataEngine = new StoreEngine(@"D:\test.dat"))
            //{
            //    //if (!dataEngine.Initialized)
            //    //{
            //    //    dataEngine.Configure(50, comboBox.SelectedIndex == 0 ? true : false);
            //    //}

            //    DateTime now = DateTime.Now;

            //    int cnt = int.Parse(textBoxNo.Text);
            //    // byte[] buffer = new byte[textBoxString.Text.Length+1];
            //    //using (MemoryStream ms = new MemoryStream(buffer))
            //    //{
            //    //    using (BinaryWriter writer=new BinaryWriter(ms))
            //    //    {
            //    //        writer.Write(textBoxString.Text);
            //    //    }
            //    //}
            //    //int offset = int.Parse(textBoxOffset.Text);
            //    dataEngine.Update(cnt, textBoxString.Text);

            //    textBlockTime.Text = (DateTime.Now - now).TotalMilliseconds.ToString();
            //}
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            textBlockLen.Text = (sender as TextBox).Text.Length.ToString();
        }
    }
}
