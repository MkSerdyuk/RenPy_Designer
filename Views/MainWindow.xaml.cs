using Microsoft.Win32;
using Ren_Py_Designer.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ren_Py_Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.Path != "")
            {
                Manager.Save(Manager.Path);
            }
        }        
        
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            Manager.Path = openFileDialog.FileName;
            Manager.OpenBlocks(Manager.Path);
        }

        private void SaveProj_Click(object sender, RoutedEventArgs e)
        {
            Manager.Save_As();
        }
    }
}
