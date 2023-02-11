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
    /// Interaction logic for CashDayView.xaml
    /// </summary>
    public partial class CashDayView : UserControl, IView
    {
        public CashDayView()
        {
            InitializeComponent();
        }

        public bool IsAnyProprtyChanged 
        { 
            get => CashDayViewModel.IsAnyProprtyChanged; 
            set => CashDayViewModel.IsAnyProprtyChanged = value;
        }
        public void Save()
        {
            try
            {
                CashDayViewModel.SaveCashAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ViewModes ViewMode { get => CashDayViewModel.ViewMode; set => CashDayViewModel.ViewMode = value; }
    }
}
