using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Flowchart_Framework.View
{
    public class BlockWithTextBox : Block
    {
        public InPort In = new InPort();
        public TextEditor Editor = new TextEditor();
        

        public BlockWithTextBox()
        {
            InitializeComponent();

            Editor.SetValue(Grid.RowProperty, 3);
            In.SetValue(Grid.RowProperty, 1);            
            
            Editor.SetValue(Grid.ColumnProperty, 0);
            In.SetValue(Grid.ColumnProperty, 0);

            Editor.SetValue(Grid.ColumnSpanProperty, 3);

            In.Parent = this;

            Label label = new Label();

            label.Content = "Example Block";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(Editor);
            MainGrid.Children.Add(label);


        }

        public override void InputChanged() 
        {
            Editor.Value = In.Value;
        }
        
    }
}
