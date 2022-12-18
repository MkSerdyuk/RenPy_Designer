using Flowchart_Framework.ViewModels;
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
    /// Interaction logic for Connector.xaml
    /// </summary>
    public partial class Connector : UserControl
    {
        private PositionWatcher _selfWatcher = new PositionWatcher();
        private PositionWatcher _fromWatcher = new PositionWatcher();
        private PositionWatcher _toWatcher = new PositionWatcher();
     

        public FrameworkElement From
        {
            get { return (FrameworkElement)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
        DependencyProperty.Register("From", typeof(FrameworkElement), typeof(FrameworkElement),
        new FrameworkPropertyMetadata((o, args) =>
        { var self = (Connector)o; self._fromWatcher.ChangeTarget(self.From); }));

        public FrameworkElement To
        {
            get { return (FrameworkElement)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(FrameworkElement), typeof(FrameworkElement),
                new FrameworkPropertyMetadata((o, args) =>
                { var self = (Connector)o; self._toWatcher.ChangeTarget(self.To); }));

        public Connector(OutPort from)
        {
            InitializeComponent();

            _selfWatcher.ChangeTarget(this);
            _selfWatcher.Changed += RedrawLine;

            From = from;
            _fromWatcher.Changed += RedrawLine;
            FollowMouse();
            //RedrawLine(null, (PositionChangeEventArgs) null);
        }

        public Connector(OutPort from,InPort to)
        {
            InitializeComponent();

            _selfWatcher.ChangeTarget(this);
            _selfWatcher.Changed += RedrawLine;

            From = from;
            _fromWatcher.Changed += RedrawLine;

            To = to;
            _toWatcher.Changed += RedrawLine;

            RedrawLine(null, (PositionChangeEventArgs) null);
        }

        public void FollowPort(InPort to)
        {
            To = to;
            _toWatcher.Changed += RedrawLine;
        }

        public void FollowMouse()
        {
            To = null;

            Mouse.AddMouseMoveHandler(this, RedrawLine);
        }

        private void RedrawLine(object sender, MouseEventArgs e)
        {
            if (From == null || To == null)
            {
                ConnectingLine.Visibility = Visibility.Collapsed;
                return;
            }

            ConnectingLine.Visibility = Visibility.Visible;

            var fromPoint = PositionWatcher.ComputeRendererPoint(From, this);
            var toPoint = e.GetPosition(this);

            ConnectingLine.X1 = fromPoint.X + 20;
            ConnectingLine.Y1 = fromPoint.Y + 10;

            ConnectingLine.X2 = toPoint.X + 20;
            ConnectingLine.Y2 = toPoint.Y + 10;
        }

        public void RedrawLine(object sender, PositionChangeEventArgs e)
        {    
            if (From == null || To == null)
            {
                ConnectingLine.Visibility = Visibility.Collapsed;
                return;
            }

            ConnectingLine.Visibility = Visibility.Visible;

            var fromPoint = PositionWatcher.ComputeRendererPoint(From, this);
            var toPoint = PositionWatcher.ComputeRendererPoint(To, this);

            ConnectingLine.X1 = fromPoint.X + 10;
            ConnectingLine.Y1 = fromPoint.Y + 10;

            ConnectingLine.X2 = toPoint.X + 10;
            ConnectingLine.Y2 = toPoint.Y + 10;
        }


    }
}
