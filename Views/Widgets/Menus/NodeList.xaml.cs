﻿using Flowchart_Framework.View;
using Flowchart_Framework.View.Blocks;
using Ren_Py_Designer.Views.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Ren_Py_Designer.Widgets.Menus
{
    /// <summary>
    /// Interaction logic for NodeList.xaml
    /// </summary>
    /// 

    public partial class NodeList : Page
    {


        public NodeList()
        {
            InitializeComponent();
            PortManager.NodeList = Blocks;
            AddLabel(new  LabelBlock());
            AddLabel(new  JumpBlock());
            AddLabel(new  IfBlock());
            AddLabel(new  ReturnBlock());
            Blocks.SelectedItem = null;
        }

        public void AddLabel(Block block) 
        {
            Blocks.Items.Add(block);

        }

        private void Blocks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Blocks.SelectedItem != null)
                PortManager.SelectedType = Blocks.SelectedItem.GetType();
            else
                PortManager.SelectedType = null;
        }
    }
}
