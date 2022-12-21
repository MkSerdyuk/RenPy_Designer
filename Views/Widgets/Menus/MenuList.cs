using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ren_Py_Designer.Views.Widgets.Menus;

namespace Ren_Py_Designer.Widgets.Menus
{
    public class MenuList
    {
        public static Dictionary<string, string> Descriptions = new Dictionary<string, string>();
        public static Dictionary<string, Page> Pages = new Dictionary<string, Page>() 
            { 
                { "WorkSpace", new WorkSpace() },
                { "CodeSpace", new CodeSpace() },
                { "NodeList", new NodeList() } 
            };
    }
}
