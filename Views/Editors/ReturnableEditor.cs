using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ren_Py_Designer.Views.Editors
{
    public class ReturnableEditor : Editor
    {
        private TextBox _labelBox = new TextBox();


        public delegate void EventHandler (object sender, ValueChangedEventArgs e);

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private void PortValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
            _labelBox.Text = e.Value;
        }

        public ReturnableEditor() : base()
        {
            Out.ValueChanged += PortValueChanged;

            _labelBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            _labelBox.SetValue(Grid.ColumnProperty, 0);
            _labelBox.IsEnabled = false;
            MainGrid.Children.Add(_labelBox);
        }
    }
}
