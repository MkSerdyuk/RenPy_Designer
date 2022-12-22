using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Flowchart_Framework.View;
using Flowchart_Framework.View.Blocks;

namespace Ren_Py_Designer.Views.Blocks
{
    public class CharacterBlock : SingleBlock
    {
        public CharacterBlock()
        {
            InitializeComponent();

            Editor = new TextEditor();

            Editor.SetValue(Grid.RowProperty, 3);
            In.SetValue(Grid.RowProperty, 1);

            Editor.SetValue(Grid.ColumnProperty, 0);
            In.SetValue(Grid.ColumnProperty, 0);

            Editor.SetValue(Grid.ColumnSpanProperty, 3);

            In.Parent = this;

            Label label = new Label();

            label.Content = "New Character";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            Editor.Command = "\tdefine {val} = Character(\"{val}\") ";
            Editor.Endl = "\n";

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(Editor);
            MainGrid.Children.Add(label);
        }

        public override void InputChanged()
        {
            Editor.Value = In.Value;
        }

        public override void Parse(Block bl, string str)
        {
            if (bl.GetType() == typeof(LabelBlock))
            {
                ((LabelBlock)bl).Editor.Out.Link(In);
            }
            else
            {
                ((SingleBlock)bl).Editor.Out.Link(In);
            }
            var words = str.Split(' ');

            ((TextEditor)Editor).SetText(words[1].Trim());
        }
    }
}
