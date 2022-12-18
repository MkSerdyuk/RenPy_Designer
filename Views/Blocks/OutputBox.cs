using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Flowchart_Framework.View.Blocks
{
    public class OutputBox : Block
    {
        public InPort Port = new InPort();

        public TextBox Out = new TextBox();

        public OutputBox()
        {
            InitializeComponent();
            Height = 300;
            Width = 150;


            Port.SetValue(Grid.RowProperty, 1);
            Port.SetValue(Grid.ColumnProperty, 0);

            Port.Parent = this;

            
            Out.Background = Brushes.White;
            Out.OpacityMask = Brushes.Black;
            Out.Height = 240;
            Out.Width = 140;
            Out.SetValue(Grid.RowProperty, 2);
            Out.SetValue(Grid.ColumnProperty, 0);
            Out.SetValue(Grid.ColumnSpanProperty, 3);
            Out.VerticalAlignment = VerticalAlignment.Stretch;
            Out.IsReadOnly = true;
            Out.TextWrapping = TextWrapping.Wrap;

            Label label = new Label();

            label.Content = "Output Block";
            label.SetValue(Grid.RowProperty, 0);
            label.SetValue(Grid.ColumnProperty, 0);
            label.SetValue(Grid.ColumnSpanProperty, 3);

            MainGrid.RowDefinitions[0].Height = new GridLength(30);
            MainGrid.RowDefinitions[1].Height = new GridLength(20);
            MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            
            MainGrid.Children.Add(Port);
            MainGrid.Children.Add(Out); 
            MainGrid.Children.Add(label);
        }

        public override void InputChanged()
        {
            Out.Text = Port.Value;
        }
    }
}
