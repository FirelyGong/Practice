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
using Practice.Algorithm.Tree.BinaryTree;

namespace Demo
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeViewWindow : Window
    {
        private BalancedBinaryTree<int> _simpleBinaryTree;
        private int[] _result;
        private int _index;

        public TreeViewWindow()
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
                _simpleBinaryTree = new BalancedBinaryTree<int>();
                _result = InitArray(11, 1, 100); //new [] { 28, 8, 97, 14, 71, 53, 15, 74, 72, 5, 60 };// 
                textBoxBinaryTree.Text = string.Join(",", _result);
                Array.ForEach(_result, a => _simpleBinaryTree.AddNode(a, a));
            }

            if (btn.Content.ToString() == "All")
            {
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
                canvas.Children.Clear();
                DisplayNodes(_simpleBinaryTree.RootNode, 0, Width/2.0);
            }

            if (btn.Content.ToString() == "Delete")
            {
                bool bln = _simpleBinaryTree.DeleteNode(_result[0]);
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
                textBlockBinaryTreeDesc.Text = bln.ToString();
            }

            if (btn.Content.ToString() == "Add")
            {
                bool bln = _simpleBinaryTree.AddNode(88, 88);
                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.GetAll());
                textBlockBinaryTreeDesc.Text = bln.ToString();
            }

            if (btn.Content.ToString() == "Step")
            {
                if (_result == null || _result.Length == 0 || _index>=_result.Length)
                {
                    _result = InitArray(11, 1, 100);
                    textBoxBinaryTree.Text = string.Join(",", _result);
                    _index = 0;
                    _simpleBinaryTree = new BalancedBinaryTree<int>();
                }

                canvas.Children.Clear();
                _simpleBinaryTree.AddNode(_result[_index], _result[_index]);
                DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
                _index++;
            }

            if (btn.Content.ToString() == "Element")
            {
                if (_simpleBinaryTree == null)
                {
                    _simpleBinaryTree = new BalancedBinaryTree<int>();
                }

                int value;
                if (int.TryParse(addTextBox.Text, out value))
                {
                    canvas.Children.Clear();
                    _simpleBinaryTree.AddNode(value, value);
                    DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
                }
            }
        }

        private void DisplayNodes<T>(BinaryTreeNode<T> node, double top, double left)
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
                Width = 30,
                Height = 30,
                Content = node.NodeValue.ToString(),
                ToolTip =
                    "value: " + node.NodeValue + " - size: " + node.Size + " - parent: "
                    + (node.Parent == null ? "null" : node.Parent.NodeKey.ToString(CultureInfo.InvariantCulture)),
            };

            display.Click += DeleteNode;

            Canvas.SetLeft(display, left);
            Canvas.SetTop(display, top);
            canvas.Children.Add(display);

            if (node.LeftChild != null)
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.Y1 = top + 15;
                line.X1 = left + 15;
                line.Y2 = top + 35 + 15;
                line.X2 = left - node.Size * 35 / 2.0 + 15;
                canvas.Children.Add(line);
                Canvas.SetZIndex(line, -1000);
                DisplayNodes(node.LeftChild, top + 35, left - node.Size * 35 / 2.0);


            }
            if (node.RightChild != null)
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.Y1 = top + 15;
                line.X1 = left + 15;
                line.Y2 = top + 35 + 15;
                line.X2 = left + node.Size * 35 / 2.0 + 15;
                canvas.Children.Add(line);
                Canvas.SetZIndex(line, -1000);
                DisplayNodes(node.RightChild, top + 35, left + node.Size * 35 / 2.0);
            }
        }

        private void DeleteNode(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            int value;
            if (int.TryParse(button.Content.ToString(), out value))
            {
                _simpleBinaryTree.DeleteNode(value);

                canvas.Children.Clear();
                DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
            }
        }
    }
}
