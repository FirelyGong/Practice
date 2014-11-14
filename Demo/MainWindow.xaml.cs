using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Practice.Algorithm.Finding;
using Practice.Algorithm.Sorting;
using Practice.Algorithm.Tree.BinaryTree;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] _array;
        private int[] _result;
        private BalancedBinaryTree<int> _simpleBinaryTree;
 
        public MainWindow()
        {
            InitializeComponent();
            //string ss = "1".PadLeft(sizeof(long),'0');
            _array = InitArray(3000, 0, 50000);
            textBoxSort.Text = string.Join(",", _array);
            _simpleBinaryTree = new BalancedBinaryTree<int>();

            //int i = 34234;
            //byte x = checked((byte)i);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "Bubble")
            {
                DateTime now = DateTime.Now;
                _result = new BubbleSort<int>().Sort(_array, OrderByAsc);
                double ms = (DateTime.Now - now).TotalMilliseconds;
                textBlockSort.Text = string.Join(",", _result);
                textBlockSortDesc.Text = ms.ToString();
            }

            if (btn.Content.ToString() == "Quick")
            {
                DateTime now = DateTime.Now;
                _result = new QuickSort<int>().Sort(_array, OrderByAsc);
                double ms = (DateTime.Now - now).TotalMilliseconds;
                textBlockSort.Text = string.Join(",", _result);
                textBlockSortDesc.Text = ms.ToString();
            }

            if (btn.Content.ToString() == "Simple" && !string.IsNullOrEmpty(textBoxSearch.Text))
            {
                DateTime now = DateTime.Now;
                int result;
                bool bln = new SimpleSearch<int,string>(CompareInt).Search(_result, textBoxSearch.Text, out result);
                double ms = (DateTime.Now - now).TotalMilliseconds;
                textBlockSearch.Text = bln.ToString();
                textBlockSearchDesc.Text = ms.ToString() + " - " + result;
            }

            if (btn.Content.ToString() == "Binary" && !string.IsNullOrEmpty(textBoxSearch.Text))
            {
                DateTime now = DateTime.Now;
                int result;
                bool bln = new BinarySearch<int, string>(CompareInt).Search(_result, textBoxSearch.Text, out result);
                double ms = (DateTime.Now - now).TotalMilliseconds;
                textBlockSearch.Text = bln.ToString();
                textBlockSearchDesc.Text = ms.ToString() + " - " + result;
            }

            if (btn.Content.ToString() == "Init")
            {
                _simpleBinaryTree = new BalancedBinaryTree<int>();
                _result = InitArray(11, 1, 100);
                textBoxBinaryTree.Text = string.Join(",", _result);
                Array.ForEach(_result, a => _simpleBinaryTree.AddNode(a, a));
            }

            if (btn.Content.ToString() == "All")
            {
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
            }

            if (btn.Content.ToString() == "Delete")
            {
                bool bln=_simpleBinaryTree.DeleteNode(_result[0]);
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
                textBlockBinaryTreeDesc.Text = bln.ToString();
            }

            if (btn.Content.ToString() == "Add")
            {
                bool bln = _simpleBinaryTree.AddNode(88,88);
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
                textBlockBinaryTreeDesc.Text = bln.ToString();
            }
        }

        private int[] InitArray(int length, int minValue, int maxValue)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int[] array=new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] =random.Next(minValue, maxValue);
            }

            return array;
        }

        private bool OrderByAsc(int i, int j)
        {
            if (i <= j)
            {
                return true;
            }

            return false;
        }

        private bool OrderByDesc(int i, int j)
        {
            if (i <= j)
            {
                return false;
            }

            return true;
        }

        private int CompareInt(int i, string k)
        {
            if (i == int.Parse(k))
            {
                return 0;
            }
            else
            {
                if (i > int.Parse(k))
                {
                    return 1;
                }

                return -1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TreeViewWindow window = new TreeViewWindow();
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BTreeWindow window = new BTreeWindow();
            window.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BPlusTreeWindow window = new BPlusTreeWindow();
            window.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FSWindow window = new FSWindow();
            window.Show();
        }
    }
}
