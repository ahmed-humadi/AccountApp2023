using FrameWorkLib;
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

namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for CurrencyTableView.xaml
    /// </summary>
    public partial class CurrencyTableView : UserControl, IView
    {
        public CurrencyTableView()
        {
            InitializeComponent();
        }
        public bool IsAnyProprtyChanged 
        {
            get => currencyTableViewModel.IsAnyProprtyChanged;
            set => currencyTableViewModel.IsAnyProprtyChanged = value;
        }
        public ViewModes ViewMode { get => currencyTableViewModel.ViewMode; set => currencyTableViewModel.ViewMode = value; }

        public void Save()
        {
            currencyTableViewModel.SaveCurrency();
        }
    }
}
