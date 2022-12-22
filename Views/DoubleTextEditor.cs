using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Flowchart_Framework.View
{
    public class DoubleTextEditor : Editor
    {
        private TextBox _textBox1 = new TextBox();
        private TextBox _textBox2 = new TextBox();

        public override string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                string command = Command;
                int i = command.IndexOf("{val}");
                command = command.Remove(i, 5).Insert(i, _textBox1.Text);
                i = command.IndexOf("{val}");
                command = command.Remove(i, 5).Insert(i, _textBox2.Text);
                Out.Value = _value + command + Endl;
            }
        }

        public void SetText(string val1, string val2)
        {
            _textBox1.Text = val1;
            _textBox2.Text = val2;
        }

        public DoubleTextEditor()
        {
            InitializeComponent();

            _textBox1.TextWrapping = System.Windows.TextWrapping.Wrap;
            _textBox2.TextWrapping = System.Windows.TextWrapping.Wrap;
            Height = 100;
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions[1].Height = new System.Windows.GridLength(40);
            MainGrid.RowDefinitions[0].Height = new System.Windows.GridLength(40);
            _textBox1.TextChanged += TextChanged;
            _textBox2.TextChanged += TextChanged;
            _textBox1.SetValue(Grid.ColumnProperty, 0);
            _textBox2.SetValue(Grid.ColumnProperty, 0);
            _textBox2.SetValue(Grid.RowProperty, 1);
            _textBox1.SetValue(Grid.RowProperty, 0);
            MainGrid.Children.Add(_textBox1);
            MainGrid.Children.Add(_textBox2);
        }

        private void TextChanged(object sender, EventArgs e)
        {
            Value = _value;
        }
    }
}
