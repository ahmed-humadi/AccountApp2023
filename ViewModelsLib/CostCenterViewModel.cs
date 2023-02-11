using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FrameWorkLib;
using BusinessLib;
using ModelLib1;
using System.Windows.Input;

namespace ViewModelsLib
{
    public class CostCenterViewModel : ViewModelBase
    {
        private List<CostCenterModel> costCenterViewModels;

        public List<CostCenterModel> CostCenterViewModels
        {
            get
            {
                return costCenterViewModels;
            }
            set
            {
                SetProperty(ref costCenterViewModels, value);
            }
        }

        private CostCenterBL costCenterBL { get; set; }

        private int iD;
        public   int ID
        {
            get { return iD; }
            set
            {
                SetProperty(ref iD, value);
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }
        private int rowIndex;
        public int RowIndex
        {
            get { return rowIndex; }
            set
            {
                SetProperty(ref rowIndex, value);
            }
        }

        #region Commands
        public ICommand ListViewSelectionChangeCommand { get; set; }
        #endregion
        public CostCenterViewModel()
        {
            costCenterBL = new CostCenterBL();

            ListViewSelectionChangeCommand = new DelegateCommand(ListViewSelectionChange);
        }
        private void ListViewSelectionChange()
        {
            try
            {
                if (this.RowIndex == -1)
                    return;

                CostCenterModel costCenterModel = this.CostCenterViewModels[this.RowIndex];

                if (costCenterModel is null)
                    return;

                this.Name = costCenterModel.Name;
                this.ID = costCenterModel.ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void NewView()
        {
            try
            {
                base.ViewMode = ViewModes.Editing;

                InitializingFields();

                base.ToggleCommandBtns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void DeleteView()
        {
            throw new NotImplementedException();
        }

        protected override void SaveView()
        {

            if (MessageBox.Show("هل تريد الحفظ ", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveCostCenter();
            }

        }

        public void SaveCostCenter()
        {
            try
            {
                this.IsLoading = true;
                if (!(CheckingFields() == ErrorType.NoError))
                    return;

                if (!IsUpdate)
                {

                    costCenterBL.InsertNewCostCenter(new CostCenterModel()
                    {
                        Name = this.Name,
                    });
                    this.IsLoading = false;
                    MessageBox.Show("Saved");

                }
                else if (IsUpdate) // modify bill
                {
                    // bill1

                    costCenterBL.UpdateCostCenter(new CostCenterModel()
                    {
                        ID = this.ID,
                        Name = this.Name,
                    });

                    this.IsLoading = false;
                    MessageBox.Show("تم حفظ التعديلات");
                }
                IsAnyProprtyChanged = false;
                this.ViewMode = ViewModes.Viewing;
                base.ToggleCommandBtns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        protected override void ModView()
        {
            try
            {
                this.ViewMode = ViewModes.Editing;
                base.ToggleCommandBtns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void LoadView()
        {
            try
            {
                this.CostCenterViewModels = this.costCenterBL.GetCostCenters();

                InitializingFields();

                base.ToggleCommandBtns();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void UnloadView()
        {
            this.CostCenterViewModels = null;
        }
        public override ErrorType CheckingFields()
        {
           if(this.ID == 0)
            {
                MessageBox.Show("يجب الاختيار");
                return ErrorType.AccountEmpty;
            }
           else if (string.IsNullOrEmpty(this.Name) || string.IsNullOrWhiteSpace(this.Name))
            {
                MessageBox.Show("الاسم فارغ");
                return ErrorType.AccountEmpty;
            }
            return ErrorType.NoError;
        }
        public override void InitializingFields()
        {
            this.Name = string.Empty;
            this.ID = 0;
        }
    }
}
