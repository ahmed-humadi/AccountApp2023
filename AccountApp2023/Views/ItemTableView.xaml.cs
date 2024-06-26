﻿using System;
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
using ViewModelsLib;
using ModelsLib;
using System.Windows.Media.Animation;

namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for ItemTableView.xaml
    /// </summary>
    public partial class ItemTableView : UserControl
    {
        public ItemTableView()
        {
            InitializeComponent();
        }

        private void ItemCategoryCmbBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox)
            {
                if((sender as ComboBox).IsKeyboardFocusWithin)
                    ItemCategoryCmbBx.RaiseTextCahanged();
            }
        }
    }
}
