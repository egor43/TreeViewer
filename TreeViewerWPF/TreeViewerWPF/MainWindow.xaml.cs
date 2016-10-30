using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace TreeViewerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public class Node
    {
        public Node Left, Right;
        public int Number;
        public int LengthTree;
        public int DepthTree;
        public int MaxWidthTree;
    }

    public partial class MainWindow : Window
    {
        public Node Tree;

        public MainWindow()
        {

            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Документ XML(*.xml)| *.xml";
            if (OpenFile.ShowDialog() == DialogResult.HasValue)
                return;
            XmlSerializer formatter = new XmlSerializer(typeof(Node));
            FileStream fs = new FileStream(OpenFile.FileName, FileMode.Open);
            Tree = (Node)formatter.Deserialize(fs);
            fs.Close();
            Tree.MaxWidthTree = Convert.ToInt32(Math.Pow(2, Tree.DepthTree = HeightTree(Tree)));
            Tree.LengthTree = CountTree(Tree);
            PrintTree(Tree,60*Tree.MaxWidthTree/2, 0, 60 * Tree.MaxWidthTree / 2);
        }

        public void PrintTree(Node tree, double StartX, double StartY, double center)
        {
            double minus = center/2;
            RectangleGeometry rect = new RectangleGeometry();
            rect.Rect = new Rect(StartX, StartY, 50, 40);
            TextBlock tb = new TextBlock();
            tb.Text = Convert.ToString(tree.Number);
            tb.Margin = new Thickness(StartX, StartY, 0, 0);
            System.Windows.Shapes.Path pt = new System.Windows.Shapes.Path();
            pt.Fill = Brushes.LightBlue;
            pt.Data = rect;
            grid.Children.Add(pt);
            grid.Children.Add(tb);
            if (tree.Left != null)
            {
                LineGeometry line = new LineGeometry();
                line.StartPoint = new Point(StartX+25, StartY+40);
                line.EndPoint = new Point(StartX - minus+25, StartY + 50+40);
                System.Windows.Shapes.Path ptline = new System.Windows.Shapes.Path();
                ptline.Stroke=Brushes.Black;
                ptline.Data = line;
                grid.Children.Add(ptline);
                PrintTree(tree.Left, StartX - minus, StartY + 50, minus);
            }
            if (tree.Right != null)
            {
                LineGeometry line = new LineGeometry();
                line.StartPoint = new Point(StartX + 25, StartY + 40);
                line.EndPoint = new Point(StartX + minus + 25, StartY + 50 + 40);
                System.Windows.Shapes.Path ptline = new System.Windows.Shapes.Path();
                ptline.Stroke = Brushes.Black;
                ptline.Data = line;
                grid.Children.Add(ptline);
                PrintTree(tree.Right, StartX + minus, StartY + 50, minus);
            }
         
        }

        public int HeightTree(Node tree)
        {
            if (tree == null) return -1;
            return Math.Max(HeightTree(tree.Left), HeightTree(tree.Right)) + 1;
        }

        public int CountTree(Node tree)
        {
            if (tree == null) return 0;
            return (CountTree(tree.Left) + CountTree(tree.Right)) + 1;
        }
    }
}
