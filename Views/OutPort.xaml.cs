using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Flowchart_Framework.View
{
    /// <summary>
    /// Interaction logic for Port.xaml
    /// </summary>
    public partial class OutPort : UserControl
    {

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (Linked != null)
                {
                    foreach(InPort linked in Linked)
                    {
                        linked.Value = value;//_value + Command.Replace("{val}", _editorValue) + Endl ;
                    }
                }
            }
        }        

        public List<InPort> Linked = new List<InPort>();

        public void UpdateOut()
        {
            foreach (InPort linked in Linked)
            {
                linked.Value = _value;//_value + Command.Replace("{val}", _editorValue) + Endl;
            }
        }

        public OutPort()
        {
            InitializeComponent();
        }

        private void Port_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PortManager.From == null )
            {
                PortManager.From = this;
                ((Ellipse)PortManager.From.Grid.Children[0]).Fill = Brushes.Aqua;
            }

        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach(InPort linked in Linked.ToList<InPort>())
            {
                Linked.Remove(linked);
            }
            
            Grid.Children.RemoveRange(1, Grid.Children.Count-1);
        }
    }
}
