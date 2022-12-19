using Ren_Py_Designer.Views;
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

namespace Flowchart_Framework.View
{
    /// <summary>
    /// Interaction logic for InPort.xaml
    /// </summary>
    public partial class InPort : UserControl
    {

        

        private string _value;

        public List<OutPort> Linked = new List<OutPort>();

        private List<Connector> _connectors = new List<Connector>();

        public Block Parent;

        public string ParentValue
        {
            get { return Parent.Value; }
        }

        public string Value
        {
            get { return _value; }
            set 
            {
                if (Linked.Count > 0)
                {
                    _value = value;
                    Parent.InputChanged();
                }
            }
        }


        private OutPort _source;


        public InPort()
        {
            InitializeComponent();
        }

        private void Port_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PortManager.From != null)
                if (PortManager.From.GetType() != typeof(MultyOutPort) && PortManager.From.Linked.Count >= 1)
            {
                return;
            }
            if (PortManager.From != null)
            {
                ((Ellipse)PortManager.From.Grid.Children[0]).Fill = Brushes.White;
                PortManager.To = this;
                Connector connector = new Connector(PortManager.From, PortManager.To);
                //Connector crunch = new Connector(PortManager.From, PortManager.To); //англ костыль
                PortManager.To._source = PortManager.From;
                //PortManager.From.Grid.Children.Add(crunch);
                List<InPort> newList = PortManager.From.Linked.ToList();
                newList.Add(this);
                PortManager.From.Linked = newList;

                Linked.Add(PortManager.From);

                Value = PortManager.From.Value;
                PortManager.From = null;
                PortManager.To = null;
                PortManager.Canvas.Children.Add(connector);
                _connectors.Add(connector);
                connector.RedrawLine(null, null);

                Parent.InputChanged();
                (Linked[Linked.Count - 1]).UpdateOut();
            }

        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Value = "";

            foreach (OutPort linked in Linked.ToList<OutPort>())
            {
                List<InPort> newList = linked.Linked.ToList();
                newList.Remove(this);
                linked.Linked = newList;

                Linked.Remove(linked);
            }

            foreach (Connector connector in _connectors)
            {
                PortManager.Canvas.Children.Remove(connector);
            }

            _connectors = new List<Connector>();    
        }
    }
}
