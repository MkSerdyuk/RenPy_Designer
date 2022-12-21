using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ren_Py_Designer.Widgets.Menus
{
    /// <summary>
    /// Interaction logic for WorkSpace.xaml
    /// </summary>
    public partial class WorkSpace : Page
    {
        public WorkSpace()
        {
            InitializeComponent();
            PortManager.Canvas = Canvas;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PortManager.SelectedType != null)
            {
                Block newBlock = ((Block)Activator.CreateInstance(PortManager.SelectedType));
                Canvas.Children.Add(newBlock);
                Canvas.SetLeft(newBlock, e.GetPosition(Canvas).X);
                Canvas.SetTop(newBlock, e.GetPosition(Canvas).Y);
                PortManager.SelectedType = null;
                PortManager.NodeList.SelectedItem = null;
            }
        }
    }
}
