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
    /// 

    public class LinkedChangedEventArgs
    {
        public LinkedChangedEventArgs(List<InPort> linked) { Linked = linked; }
        public List<InPort> Linked { get; }
    }

    public partial class OutPort : UserControl
    {
        public delegate void EventHandler(object sender, LinkedChangedEventArgs e);
        public delegate void ValueEventHandler(object sender, ValueChangedEventArgs e);

        public event EventHandler LinkedChanged;
        public event ValueEventHandler ValueChanged;

        public Type ParentType = typeof(Block);

        private string _value;

        public virtual string Value
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

        public List<InPort> _linked = new List<InPort>();

        public void LinkedValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }

        public virtual List<InPort> Linked
        {

            get { return _linked; }
            set 
            {
               var type = this.GetType();
                if (value.Count <= 1)
                {
                    if (Linked.Count > 0)
                    {
                        Linked[0].Parent.ValueChanged -= LinkedValueChanged;
                    }
                    if (value.Count > 0)
                    {
                        value[0].Parent.ValueChanged += LinkedValueChanged;
                        ValueChanged?.Invoke(null, new ValueChangedEventArgs(value[0].Parent.Value));
                    }
                    LinkedChanged?.Invoke(this, new LinkedChangedEventArgs(value));
                    _linked = value;
                }
                else
                {
                    throw new InvalidOperationException("SinglePort takes to linked ports");
                }
            }
        }

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
                ((Shape)PortManager.From.Grid.Children[0]).Fill = Brushes.Aqua;
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

        public void Link(InPort port)
        {
            port.Link(this);
        }
    }
}
