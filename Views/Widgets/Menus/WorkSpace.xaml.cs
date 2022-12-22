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

        private void ReSacle(float val)
        {
            if (val > 1)
            {
                Canvas.Width *= val;
                Canvas.Height *= val;
            }
            ScaleTransform scale = new ScaleTransform(Canvas.LayoutTransform.Value.M11 * val, Canvas.LayoutTransform.Value.M22 * val);
            Canvas.LayoutTransform = scale;
            Canvas.UpdateLayout();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPlus)
            {
                ReSacle(1.1f);
            }
            else if (e.Key == Key.OemMinus)
            {
                ReSacle(0.9f);
            }

        }
    }
}
