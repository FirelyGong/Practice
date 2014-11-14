using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Practice.Algorithm.Tree.BPlusTree;

namespace Demo
{
    /// <summary>
    /// Interaction logic for BTreeWindow.xaml
    /// </summary>
    public partial class BPlusTreeWindow : Window
    {
        private BPTree _simpleBinaryTree;

        private string[] _result;
        private int _index;

        public BPlusTreeWindow()
        {
            InitializeComponent();
        }
        private string[] InitArray(int length, int minValue, int maxValue)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string[] array = new string[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(minValue, maxValue).ToString("00");
            }

            return array;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            if (btn.Content.ToString() == "Init")
            {
                _simpleBinaryTree = new BPTree(2, 0, BPTreeNodeType.Leaf);
                _result = InitArray(30, 1, 100); //new [] { 28, 8, 97, 14, 71, 53, 15, 74, 72, 5, 60 };// 
                textBoxBinaryTree.Text = string.Join(",", _result);
                Array.ForEach(_result,
                    a =>
                    {
                        _simpleBinaryTree.Add(a, a);
                        textBlockBinaryTreeDesc.Text = _simpleBinaryTree.Root.NodeId.ToString();
                    });
            }

            if (btn.Content.ToString() == "All")
            {
                //if (_simpleBinaryTree == null)
                //{
                //    _simpleBinaryTree = new BPTree<int>(2, new NodePersistence(@"d:\test.dat", (sizeof(int) + 2) * 4), 19);
                //}

                textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.Find("01", "99"));
                canvas.Children.Clear();
                DisplayNodes(_simpleBinaryTree.Root, 0, Width / 2.0);
            }

            if (btn.Content.ToString() == "Feh")
            {
                //if (_simpleBinaryTree == null)
                //{
                //    _simpleBinaryTree = new BPTree<int>(2, new NodePersistence(@"d:\test.dat", (sizeof(int) + 2) * 4), 19);
                //}
                IList<string> result = new List<string>();
                foreach (BPTreeNodeData data in _simpleBinaryTree.DataSet)
                {
                    result.Add(data.Value);
                }

                textBlockBinaryTree.Text = string.Join(",", result.ToArray());
            }

            if (btn.Content.ToString() == "Delete")
            {
                string value = addTextBox.Text;
                //if (int.TryParse(addTextBox.Text, out value))
                //{
                    bool bln = _simpleBinaryTree.Delete(value);
                    textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.Find("01", "99"));
                    textBlockBinaryTreeDesc.Text = bln.ToString();
                    canvas.Children.Clear();
                    DisplayNodes(_simpleBinaryTree.Root, 0, Width / 2.0);
                //}
            }

            if (btn.Content.ToString() == "Step")
            {
                if (_result == null || _result.Length == 0 || _index >= _result.Length)
                {
                    _result = InitArray(11, 1, 100);
                    textBoxBinaryTree.Text = string.Join(",", _result);
                    _index = 0;
                    _simpleBinaryTree = new BPTree(2, 0, BPTreeNodeType.Leaf);
                }

                canvas.Children.Clear();
                _simpleBinaryTree.Add(_result[_index].ToString(), _result[_index].ToString());
                DisplayNodes(_simpleBinaryTree.Root, 0, Width / 2.0);
                _index++;
            }

            if (btn.Content.ToString() == "Add")
            {
                if (_simpleBinaryTree == null)
                {
                    _simpleBinaryTree = new BPTree(2, 0, BPTreeNodeType.Leaf);
                }

                string value=addTextBox.Text;
                //if (int.TryParse(addTextBox.Text, out value))
                //{
                    canvas.Children.Clear();
                    _simpleBinaryTree.Add(value, value);
                    DisplayNodes(_simpleBinaryTree.Root, 0, Width / 2.0);
                //}
            }

            if (btn.Content.ToString() == "Find")
            {
                if (_simpleBinaryTree == null)
                {
                    _simpleBinaryTree = new BPTree(2, 0, BPTreeNodeType.Leaf);
                }

                string value = addTextBox.Text;
                //if (int.TryParse(addTextBox.Text, out value))
                //{
                    string find = _simpleBinaryTree.Find(value);
                    textBlockBinaryTreeDesc.Text = find;
                //}
            }
        }
        
        private void DisplayNodes(BPTreeNode node, double top, double left)
        {
            if (left < 0)
            {
                left = 0;
            }

            IList<string> keys = new List<string>();
            if (node.NodeType == BPTreeNodeType.Internal)
            {
                int cnt = (node as BPTreeInternalNode).Size;
                for (int i = 0; i < cnt; i++)
                {
                    keys.Add((node as BPTreeInternalNode).Children[i].Key);
                }
            }
            else
            {
                int cnt = (node as BPTreeLeafNode).Size;
                for (int i = 0; i < cnt; i++)
                {
                    keys.Add((node as BPTreeLeafNode).Elements[i].Key);
                }
            }

            Button display = new Button
            {
                Width = 50,
                Height = 30,
                DataContext = keys,
                Style= (Style)this.FindResource("buttonStyle")
            };

            display.Click += DeleteNode;

            Canvas.SetLeft(display, left);
            Canvas.SetTop(display, top);
            canvas.Children.Add(display);

            int deepth = GetNodeDeepth(node);
            if (deepth == 0)
            {
                deepth = 1;
            }

            double width = 50 * Math.Pow(node.Size + 1, deepth - 1) * node.Size;
            double nodeWidth = width / (node.Size > 1 ? node.Size - 1 : 1);

            for (int i = 0; i < node.Size; i++)
            {
                if (node.NodeType == BPTreeNodeType.Internal)
                {
                    if (GetChild(node, i) != null)
                    {
                        double childTop = top + 35;
                        double childLeft = left - width / 2 + i * nodeWidth;

                        Line line = new Line();
                        line.Stroke = new SolidColorBrush(Colors.Black);
                        line.StrokeThickness = 1;
                        line.Y1 = top + 15;
                        line.X1 = left + i * (50.0 / (node.Size + 1));
                        line.Y2 = childTop;
                        line.X2 = childLeft + 25;
                        canvas.Children.Add(line);
                        Canvas.SetZIndex(line, -1000);

                        DisplayNodes(GetChild(node, i), childTop, childLeft);
                    }
                }
            }
        }

        private BPTreeNode GetChild(BPTreeNode node, int index)
        {
            if (node is BPTreeLeafNode)
            {
                return null;
            }

            return (node as BPTreeInternalNode).Children[index];
        }

        private int GetNodeDeepth(BPTreeNode node)
        {
            if (node is BPTreeLeafNode)
            {
                return 0;
            }

            int deepth = 0;
            var child = (node as BPTreeInternalNode).Children[0];
            if (child == null)
            {
                return 0;
            }
            else
            {
                deepth = GetNodeDeepth(child) + 1;
            }

            return deepth;
        }

        private void DeleteNode(object sender, EventArgs e)
        {
            //Button button = sender as Button;
            //if (button == null)
            //{
            //    return;
            //}

            //int value;
            //if (int.TryParse(button.Content.ToString(), out value))
            //{
            //    bool bln = _simpleBinaryTree.Delete(value);
            //    textBlockBinaryTree.Text = string.Join(",", _simpleBinaryTree.Find(0, 100));
            //    textBlockBinaryTreeDesc.Text = bln.ToString();
            //    canvas.Children.Clear();
            //    DisplayNodes(_simpleBinaryTree.RootNode, 0, Width / 2.0);
            //}
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            string value = label.Content.ToString();
            var bln =_simpleBinaryTree.Delete(value);

            canvas.Children.Clear();
            DisplayNodes(_simpleBinaryTree.Root, 0, Width / 2.0);
            textBlockBinaryTreeDesc.Text = bln.ToString();
        }

        //private BPTreeNodeKey MakeLocator(long key)
        //{
        //    return new BPTreeNodeKey(key.ToString());
        //}
    }
}
