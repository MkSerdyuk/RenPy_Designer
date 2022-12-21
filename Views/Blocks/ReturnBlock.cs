using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ren_Py_Designer.Views.Blocks
{
    public class ReturnBlock : Block
    {
        public InPort In = new InPort();

        public ReturnBlock()
        {
            InitializeComponent();
            Width = 60;
            In.SetValue(Grid.RowProperty, 1);
            In.SetValue(Grid.ColumnProperty, 0);
            In.Parent = this;
            Label label = new Label();
            label.Content = "Return";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(label);
        }

        public override void InputChanged()
        {
            try
            {
                int indexL = In.Value.IndexOf(" ") + 1;
                int indexR = In.Value.IndexOf(":");
                string labelName = In.Value.Substring(indexL, indexR - indexL);
                Manager.Labels[labelName] = In.Value + "\treturn\n";
                Manager.ReloadCode();
            }
            catch { }
        }
    }
}
