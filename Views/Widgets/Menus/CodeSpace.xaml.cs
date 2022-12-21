using Flowchart_Framework.View;
using Ren_Py_Designer.Models;
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

namespace Ren_Py_Designer.Views.Widgets.Menus
{
    /// <summary>
    /// Interaction logic for CodeSpace.xaml
    /// </summary>
    public partial class CodeSpace : Page
    {
        public CodeSpace()
        {
            InitializeComponent();

            Manager.ValueChanged += WriteCode;
            WriteCode(null, null);
        }

        public void WriteCode(object sender, ValueChangedEventArgs e)
        {
            try
            {
                Code.Text = Manager.GetCode();
            }
            catch { }
        }
    }
}
