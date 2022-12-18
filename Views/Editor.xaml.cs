using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flowchart_Framework.View
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        protected string _value = "";
        
        private string _command = "";
        private string _endl = "";

        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }        
        
        public string Endl
        {
            get { return _endl; }
            set { _endl = value; }
        }

        public virtual string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Editor()
        {
            InitializeComponent();
        }
    }
}
