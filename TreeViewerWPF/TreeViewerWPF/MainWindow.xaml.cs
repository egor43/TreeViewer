using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        }
    }
}
