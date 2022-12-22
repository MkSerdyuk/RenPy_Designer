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
    /// Interaction logic for Block.xaml
    /// </summary>
    ///
    public partial class Block : UserControl
    {
        public delegate void EventHandler (object sender, ValueChangedEventArgs e);

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public Block()
        {
            InitializeComponent();
        }        

        public Canvas Parent;

        private string _value = "";

        public string Value 
        { 
            get
            {
                return _value;
            }
            set
            {
                ValueChanged?.Invoke (this, new ValueChangedEventArgs (value));
                _value = value;
            }
        }

        private string _state = "static";

        public Grid MainGrid 
        { 
            get { return _mainGrid; } 
        }

        public virtual void InputChanged()
        {

        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _state = "active";
        }

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_state == "active")
            {
                var position = e.GetPosition(PortManager.Canvas);
                Canvas.SetLeft(this, position.X - 50);
                Canvas.SetTop(this, position.Y - 20);

                if (position.X > PortManager.Canvas.Width - 100)
                {
                    PortManager.Canvas.Width = position.X + 100;
                } 
                if (position.Y > PortManager.Canvas.Height - 100)
                {
                    PortManager.Canvas.Height = position.Y + 100;
                }

            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _state = "static";
        }

        public virtual void Parse(Block bl, string str)
        {
            
        }
    }
}
