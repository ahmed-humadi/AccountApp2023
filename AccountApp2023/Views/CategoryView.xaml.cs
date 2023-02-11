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

namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for CatogryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryViewModel categoryViewModel { get; set; }
        public CategoryView()
        {
            categoryViewModel = new CategoryViewModel();
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
