using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Flowchart_Framework.View
{
    public class TextEditor : Editor
    {
        private TextBox _textBox = new TextBox();

        public delegate void EventHandler(object sender, ValueChangedEventArgs e);

        public event EventHandler ValueChanged;

        public override string Value
        {
            get { return _value; }
            set 
            {
                _value = value;
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(_textBox.Text));
                Out.Value = value + Command.Replace("{val}", _textBox.Text) + Endl;
            }
        }

        public string FullCommand
        {
            get
            {
                return Command.Replace("{val}", _textBox.Text) + Endl;
            }
        }

        public TextEditor()
        {
            InitializeComponent();

            _textBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            _textBox.TextChanged += TextChanged;
            _textBox.SetValue(Grid.ColumnProperty, 0);
            MainGrid.Children.Add(_textBox);
        }

        private void TextChanged(object sender, EventArgs e)
        {
            Value = _value;
        }

        public void SetText(string text)
        {
            _textBox.Text = text;
        }
    }
}
