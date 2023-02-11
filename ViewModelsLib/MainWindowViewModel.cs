using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkLib;
using ModelsLib;
using DataEntitiesLib;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace ViewModelsLib
{
    public class MainWindowViewModel
    {
        private MainWindowModel mainWindowModel;

        private DataTable billTypeTable;

        public DataTable BillTypeTable
        {
            get => billTypeTable;
            set => billTypeTable = value;
        }
        public ICommand LoadViewModelCommand { get; set; }
        public MainWindowViewModel()
        {
            mainWindowModel = new MainWindowModel();
            LoadViewModelCommand = new DelegateCommand(LoadViewModel);
        }
        private void LoadViewModel()
        {
            try
            {
                BillTypeTable = mainWindowModel.GetBillTypes();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
