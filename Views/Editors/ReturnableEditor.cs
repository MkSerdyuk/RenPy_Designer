using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Ren_Py_Designer.Views.Editors
{
    public class ReturnableEditor : Editor
    {
        public TextBox LabelBox = new TextBox();


        public delegate void EventHandler (object sender, ValueChangedEventArgs e);

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private void PortValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
            Value = e.Value;
        }

        public ReturnableEditor() : base()
        {
            Out.ValueChanged += PortValueChanged;
            Out.ParentType = GetType();
            Out.Grid.Children.RemoveAt(0);
            Out.Grid.Children.Add(
                new Rectangle { Height = 20, Width = 20, Stroke = new SolidColorBrush(Colors.Black), Fill = new SolidColorBrush(Colors.White) }
                );

            LabelBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            LabelBox.SetValue(Grid.ColumnProperty, 0);
            LabelBox.Visibility = System.Windows.Visibility.Hidden;
            MainGrid.Children.Add(LabelBox);
        }

        public string FullCommand
        {
            get
            {
                if (LabelBox.Text != "")
                {
                    string command = Command;
                    int i = command.IndexOf("{val}");
                    string result = command.Remove(i, 5).Insert(i, LabelBox.Text);
                    i = command.IndexOf("{val}");
                    command.Remove(i, 5).Insert(i, Value);
                    return _value + command + Endl;
                }
                return _value + Command.Replace("{val}", Value) + Endl;
            }
        }
    }
}
