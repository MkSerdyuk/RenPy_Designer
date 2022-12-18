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
                var regex = new Regex(Regex.Escape("{val}"));
                string temp = regex.Replace(Command, _textBox1.Text, 1);
                temp = regex.Replace(temp, _textBox2.Text, 1);
                Out.Value = value + temp + Endl ;
            }
        }

        public DoubleTextEditor()
        {
            InitializeComponent();

            _textBox1.TextWrapping = System.Windows.TextWrapping.Wrap;
            _textBox2.TextWrapping = System.Windows.TextWrapping.Wrap;
            Height = 60;
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions[1].Height = new System.Windows.GridLength(38);
            MainGrid.RowDefinitions[0].Height = new System.Windows.GridLength(22);
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
