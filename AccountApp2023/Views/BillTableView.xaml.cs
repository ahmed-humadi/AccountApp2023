using System;
using System.Windows.Controls;
using System.Windows.Input;
using FrameWorkLib;
namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for BillTableView.xaml
    /// </summary>
    public partial class BillTableView : UserControl, IView
    { 
        public BillTableView()
        {
            
            InitializeComponent();

        }
        public bool IsAnyProprtyChanged 
        { 
            get => billTableViewModel.IsAnyProprtyChanged;
            set => billTableViewModel.IsAnyProprtyChanged = value;
        }
        public ViewModes ViewMode 
        { 
            get => billTableViewModel.ViewMode; 
            set => billTableViewModel.ViewMode = value; 
        }

        public void Save()
        {
            billTableViewModel.SaveBillAsync();
        }
    }
}
