using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
using Ren_Py_Designer.Views.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ren_Py_Designer.Views.Blocks
{
    public class IfBlock : Block
    {
        public InPort In = new InPort();
        public List<ReturnableEditor> Editors = new List<ReturnableEditor>();
        public List<string> Commands = new List<string>();

        public Button NewCondition = new Button();

        private void TargetLabelChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                Value = e.Value;
                Commands[Editors.IndexOf((ReturnableEditor)sender)] = ((ReturnableEditor)sender).FullCommand;
                ReloadCode();
            }
            catch { }
        }

        private void ReloadCode()
        {
            try
            {
                int indexL = In.Value.IndexOf(" ") + 1;
                int indexR = In.Value.IndexOf(":");
                string labelName = In.Value.Substring(indexL, indexR - indexL);

                string result = In.Value;

                foreach (string command in Commands)
                {
                    result += command;
                }

                Manager.Labels[labelName] = result;
                Manager.ReloadCode();
            }
            catch { }
        }

        public void AddCondition(object sender, RoutedEventArgs e)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition());

            Editors.Add(new ReturnableEditor());
            Commands.Add("");

            int index = Editors.Count - 1;

            Editors[index].SetValue(Grid.RowProperty, index + 3);
            Editors[index].SetValue(Grid.ColumnProperty, 0);
            Editors[index].SetValue(Grid.ColumnSpanProperty, 3);
            Editors[index].LabelBox.Visibility = Visibility.Visible;

            if (index == 0)
            {
                Editors[index].Command = "\tif {val}:\n\t\tjump {val}";
            }
            else
            {
                Editors[index].Command = "\telif {val}:\n\t\tjump {val}";
            }
            Editors[index].Endl = "\n";

            Editors[index].ValueChanged += TargetLabelChanged;

            MainGrid.Children.Add(Editors[index]);
        }

        public IfBlock()
        {
            InitializeComponent();
            Width = 120;
            NewCondition.SetValue(Grid.RowProperty, 2);
            NewCondition.SetValue(Grid.ColumnProperty, 0);
            NewCondition.SetValue(Grid.ColumnSpanProperty, 3);
            NewCondition.Content = "New Condition";
            NewCondition.Click += AddCondition;

            In.SetValue(Grid.RowProperty, 1);
            In.SetValue(Grid.ColumnProperty, 0);

            In.Parent = this;

            Label label = new Label();

            label.Content = "If";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.Children.Add(In);
            MainGrid.Children.Add(label);
            MainGrid.Children.Add(NewCondition);

        }

        public override void InputChanged()
        {
            try
            {
                int indexL = In.Value.IndexOf(" ") + 1;
                int indexR = In.Value.IndexOf(":");
                string labelName = In.Value.Substring(indexL, indexR - indexL);
                Manager.Labels[labelName] = In.Value + "  jump " + Value;
                Manager.ReloadCode();
            }
            catch { }
        }
    }
}
