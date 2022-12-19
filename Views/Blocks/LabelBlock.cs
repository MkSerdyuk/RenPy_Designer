using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Flowchart_Framework.View.Blocks
{
    public class LabelBlock: Block
    {
        public delegate void EventHandler(object sender, ValueChangedEventArgs e);

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public InPort In = new InPort();
        public TextEditor Editor = new TextEditor();

        public LabelBlock()
        {
            InitializeComponent();

            Editor.SetValue(Grid.RowProperty, 3);
            In.SetValue(Grid.RowProperty, 1);

            Editor.SetValue(Grid.ColumnProperty, 0);
            In.SetValue(Grid.ColumnProperty, 0);

            Editor.SetValue(Grid.ColumnSpanProperty, 3);

            In.Parent = this;

            Label label = new Label();

            label.Content = "Label";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(Editor);
            MainGrid.Children.Add(label);

            Editor.Command = "label {val}";
            Editor.Endl = ":\n";

            Editor.ValueChanged += LabelNameChanged;
        }

        private void LabelNameChanged(object sender, ValueChangedEventArgs e)
        {
            if (Manager.Labels.ContainsKey(Value))
            {
                Manager.Labels.Remove(Value);
            }
            if (Manager.Labels.ContainsKey(e.Value))
            {
                MessageBox.Show("Обратите внимание, уже есть label с таким именем");
            }
            Manager.Labels.Add(e.Value, "");
            Value = e.Value;
            ValueChanged?.Invoke(this, e);
        }

        public override void InputChanged()
        {            
            
        }
    }
}
