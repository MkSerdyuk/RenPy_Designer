using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
using Ren_Py_Designer.Views.Blocks;
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
            try
            {
                Value = e.Value;
                int indexL = In.Value.IndexOf(" ") + 1;
                int indexR = In.Value.IndexOf(":");
                string labelName = In.Value.Substring(indexL, indexR - indexL);
                Manager.Labels[labelName] = In.Value + Editor.FullCommand;
                Manager.ReloadCode();
            }
            catch { }
        }

        public JumpBlock()
        {
            InitializeComponent();
            Width = 120;
            Editor.SetValue(Grid.RowProperty, 2);
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

            Editor.Command = "\tjump {val}";
            Editor.Endl = "\n";

            Editor.ValueChanged += TargetLabelChanged;
            
        }        
        
        public JumpBlock(Block bl, string str)
        {
            InitializeComponent();
            Width = 120;
            Editor.SetValue(Grid.RowProperty, 2);
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

            Editor.Command = "\tjump {val}";
            Editor.Endl = "\n";

            Editor.ValueChanged += TargetLabelChanged;

            Parse(bl, str);
        }

        public override void InputChanged()
        {
            try
            {
                int indexL = In.Value.IndexOf(" ") + 1;
                int indexR = In.Value.IndexOf(":");
                string labelName = In.Value.Substring(indexL, indexR - indexL);
                Manager.Labels[labelName] = In.Value + Editor.FullCommand;
                Manager.ReloadCode();
            }
            catch { }
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
            int indexL = str.IndexOf(" ") + 1;
            int indexR = str.IndexOf("\n");
            string labelName = str.Substring(indexL);
            if (!Manager.Labels.ContainsKey(labelName))
            {
                LabelBlock labelBlock = new LabelBlock(null, "label " + labelName + ":\n");
            }
            Editor.Out.Link(Manager.LabelBlocks[labelName].In);
        }
    }
}
