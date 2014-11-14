using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Practice.Algorithm.Tree.BTree;

namespace Demo
{
    /// <summary>
    /// Interaction logic for BTreeWindow.xaml
    /// </summary>
    public partial class BTreeWindow : Window
    {
        private SimpleBTree<int> _simpleBinaryTree;
        private int[] _result;
        private int _index;

        public BTreeWindow()
        {
            InitializeComponent();
        }
        private int[] InitArray(int length, int minValue, int maxValue)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(minValue, maxValue);
            }

            return array;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            if (btn.Content.ToString() == "Init")
            {
                _simpleBinaryTree = new SimpleBTree<int>(2);
                _result = InitArray(41, 1, 100); //new [] { 28, 8, 97, 14, 71, 53, 15, 74, 72, 5, 60 };// 
                textBoxBinaryTree.Text = string.Join(",", _result);
                Array.ForEach(_result, a => _simpleBinaryTree.Add(a, a));
            }

            if (btn.Content.ToString() == "All")
            {
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.Find(0,100));
                canvas.Children.Clear();
                DisplayNodes(_simpleBinaryTree.RootNode, 0, Width/2.0);
            }

            if (btn.Content.ToString() == "Delete")
            {
                int value;
                if (int.TryParse(addTextBox.Text, out value))
                {
                    bool bln = _simpleBinaryTree.Delete(value);
                    textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.Find(0, 100));
                    textBlockBinaryTreeDesc.Text = bln.ToString();
                    canvas.Children.Clear();
                    DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
                }
            }

            if (btn.Content.ToString() == "Step")
            {
                if (_result == null || _result.Length == 0 || _index>=_result.Length)
                {
                    _result = InitArray(11, 1, 100);
                    textBoxBinaryTree.Text = string.Join(",", _result);
                    _index = 0;
                    _simpleBinaryTree = new SimpleBTree<int>(2);
                }

                canvas.Children.Clear();
                _simpleBinaryTree.Add(_result[_index], _result[_index]);
                DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
                _index++;
            }

            if (btn.Content.ToString() == "Add")
            {
                if (_simpleBinaryTree == null)
                {
                    _simpleBinaryTree = new SimpleBTree<int>(2);
                }

                int value;
                if (int.TryParse(addTextBox.Text, out value))
                {
                    canvas.Children.Clear();
                    _simpleBinaryTree.Add(value, value);
                    DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
                }
            }

            if (btn.Content.ToString() == "Find")
            {
                int value;
                if (int.TryParse(addTextBox.Text, out value))
                {
                    int find = _simpleBinaryTree.Find(value);
                    textBlockBinaryTreeDesc.Text = find.ToString();
                }
            }
        }
        
        private void DisplayNodes<T>(BTreeNode<T> node, double top, double left)
        {
            if (left < 0)
            {
                left = 0;
            }

            if (node == null)
            {
                return;
            }

            Button display = new Button
            {
                Width = 50,
                Height = 30,
                DataContext = node.Elements,
                Style= (Style)this.FindResource("buttonStyle")
            };

            //display.Click += DeleteNode;

            Canvas.SetLeft(display, left);
            Canvas.SetTop(display, top);
            canvas.Children.Add(display);

            int deepth = GetNodeDeepth(node);

            for (int i = 0; i < node.Size + 1; i++)
            {
                if (node.Children(i) != null)
                {
                    double childTop = top + 35;
                    double childLeft = left + (i - (node.Size + 1) / 2) * 50 * Math.Pow(4, deepth-1);

                    Line line = new Line();
                    line.Stroke = new SolidColorBrush(Colors.Black);
                    line.StrokeThickness = 1;
                    line.Y1 = top + 15;
                    line.X1 = left + i * (50.0 / (node.Size+1));
                    line.Y2 = childTop;
                    line.X2 = childLeft+25;
                    canvas.Children.Add(line);
                    Canvas.SetZIndex(line, -1000);

                    DisplayNodes<T>(node.Children(i), childTop, childLeft);
                }
            }
        }

        private int GetNodeDeepth<T>(BTreeNode<T> node)
        {
            int deepth = 0;
            var loop = node.Children(0);
            while (loop != null)
            {
                deepth++;
                loop = loop.Children(0);
            }

            return deepth;
        }
    }
}
