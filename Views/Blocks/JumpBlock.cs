using Flowchart_Framework.View;
using Ren_Py_Designer.Views.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Flowchart_Framework.View.Blocks
{
    public class JumpBlock : Block
    {
        public InPort In = new InPort();
        public ReturnableEditor Editor = new ReturnableEditor();

        private void TargetLabelChanged(object sender, ValueChangedEventArgs e)
        {

        }

        public JumpBlock()
        {
            InitializeComponent();

            Editor.SetValue(Grid.RowProperty, 3);
            In.SetValue(Grid.RowProperty, 1);

            Editor.SetValue(Grid.ColumnProperty, 0);
            In.SetValue(Grid.ColumnProperty, 0);

            Editor.SetValue(Grid.ColumnSpanProperty, 3);

            In.Parent = this;

            Label label = new Label();

            label.Content = "Jump";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(Editor);
            MainGrid.Children.Add(label);

            Editor.Command = "  jump {val}";
            Editor.Endl = "\n";

            Editor.ValueChanged += TargetLabelChanged;
            
        }
    }
}
