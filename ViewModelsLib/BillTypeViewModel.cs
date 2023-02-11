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
using ViewModelsLib.ViewModelEventArgs;
namespace ViewModelsLib
{
    public class BillTypeViewModel : BindableBase
    {
        private BillTypeModel billTypeModel;

        private DataTable billTypeTable;

        private string billTypeName;
        public string BillTypeName
        {
            get => billTypeName;
            set
            {
                SetProperty(ref billTypeName, value);
                IsSaving = true;
            }
        }

        private int billTypeNumber;
        public int BillTypeNumber
        {
            get => billTypeNumber;
            set
            {
                SetProperty(ref billTypeNumber, value);
                IsSaving = true;
            }
        }

        private bool isSaving = false;
        public bool IsSaving
        {
            get => isSaving;
            set
            {
                SetProperty(ref isSaving, value);
                ((DelegateCommand)SaveBillTypeCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand SaveBillTypeCommand { get; set; }

        public ICommand LoadBillTypeTableCommand { get; set; }
        public BillTypeViewModel()
        {
            billTypeModel = new BillTypeModel();

            SaveBillTypeCommand = new DelegateCommand(SaveBillType, () => CanExcute());

            LoadBillTypeTableCommand = new DelegateCommand(LoadBillTypeTable);
        }

        private void LoadBillTypeTable()
        {
            try
            {
                this.billTypeTable = billTypeModel.GetBillTypeTable();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExcute()
        {

            if(!String.IsNullOrEmpty(this.billTypeName) && !String.IsNullOrWhiteSpace(this.billTypeName))
            {
                return true;
            }
            return false;
        }
        private void SaveBillType()
        {
            try
            {
        
                DataRow dataRow = this.billTypeTable.NewRow();
                dataRow["Name"] = this.BillTypeName;
                dataRow["Number"] = this.BillTypeNumber;
                billTypeTable.Rows.Add(dataRow);

                this.billTypeModel.UpdateBillTypeTable(billTypeTable);

                this.billTypeTable.AcceptChanges();

                OnBillTypeCreated(this.BillTypeName, (int)dataRow["ID"], (int)dataRow["Number"]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event EventHandler BillTypeCreated;

        protected void OnBillTypeCreated( string billTypeName, int id, int number)
        {
            BillTypeCreated?.Invoke(this, new BillTypeEventArgs() {BillTypeName = billTypeName, BillTypeID = id, BillTypeNumber = number });
        }
    }
    public class BillTypeEventArgs: EventArgs
    {
        public string BillTypeName { get; set; }
        public int BillTypeID { get; set; }
        public int BillTypeNumber { get; set; }
    }
}
