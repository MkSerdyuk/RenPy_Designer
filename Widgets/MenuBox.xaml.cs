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

namespace Ren_Py_Designer.Widgets
{
    /// <summary>
    /// Interaction logic for MenuBox.xaml
    /// </summary>
    public partial class MenuBox : UserControl
    {
        public MenuBox()
        {
            InitializeComponent();
            foreach (string item in Menus.MenuList.Pages.Keys)
            { 
                MenuChooser.Items.Add(item);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Page examplePage = Menus.MenuList.Pages[MenuChooser.SelectedItem.ToString()]; //поулчаем тип страницы, чтобы создать такую же
            Type pageType = examplePage.GetType();
            Content.Navigate(Activator.CreateInstance(pageType)); //создаем страницу с нужным нам типом
        }
    }
}
