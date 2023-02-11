using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FrameWorkLib;
using ModelLib1;
using BusinessLib;
using System.Windows;
namespace ViewModelsLib
{
    public class CurrencyTableViewModel : ViewModelBase
    {
        #region Private Fields
        private CurrencyBL currencyBL;
        #endregion
        #region DataCatch Properties
        private int CurrNumberCache { get; set; }
        #endregion
        #region Control Properties 
        public bool IsNewCurrerncy { get; set; }
        #endregion
        #region Bindable Properties
        private bool enableEdit = false;
        public bool EnableEdit
        {
            get { return enableEdit; }
            set
            {
                SetProperty(ref enableEdit, value);
            }
        }
        private int currID;
        public int CurrID
        {
            get
            {
                return currID;
            }
            set
            {
                SetProperty<int>(ref currID, value);
            }
        }
        private string currName;
        public string CurrName
        {
            get
            {
                return currName;
            }
            set
            {
                SetProperty<string>(ref currName, value);
                if (base.ViewMode == ViewModes.Editing)
                {
                    if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
                        this.IsAnyProprtyChanged = true;
                }
                else if (base.ViewMode == ViewModes.Viewing)
                {
                    this.IsAnyProprtyChanged = false;
                }
            }
        }
        private string currPartName;
        public string CurrPartName
        {
            get { return currPartName; }
            set
            {
                SetProperty<string>(ref currPartName, value);
                if (base.ViewMode == ViewModes.Editing)
                {
                    if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
                        this.IsAnyProprtyChanged = true;
                }
                else if (base.ViewMode == ViewModes.Viewing)
                {
                    this.IsAnyProprtyChanged = false;
                }
            }
        }
        private string currValue;
        public string CurrValue
        {
            get { return currValue; }
            set
            {
                SetProperty<string>(ref currValue, value);
                if (base.ViewMode == ViewModes.Editing)
                {
                    if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
                        this.IsAnyProprtyChanged = true;
                }
                else if (base.ViewMode == ViewModes.Viewing)
                {
                    this.IsAnyProprtyChanged = false;
                }
            }
        }
        private int currNumber;
        public int CurrNumber
        {
            get { return currNumber; }
            set
            {
                SetProperty<int>(ref currNumber, value);
            }
        }
        #endregion
        #region Excution Properties
    
        #endregion
        #region Commands
        public ICommand CurrNumberTxtBxTextChangedCommand { get; set; }
        #endregion

        #region Constructor
        public CurrencyTableViewModel()
        {
            this.currencyBL = new CurrencyBL();
            CurrNumberTxtBxTextChangedCommand = new DelegateCommand(CurrNumberTxtBxTextChanged);
        }
        protected override void ModView()
        {
            try
            {
                this.ViewMode = ViewModes.Editing;
                base.ToggleCommandBtns();
                /*this.IsFirstTxtBxFocused = true;
                this.IsFirstTxtBxFocused = false;
                IsEditting = false;
                EnableEditing = true;
                IsSaving = true;
                IsUpdate = true;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void SaveView()
        {
            if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveCurrency();
            }
        }
        public void SaveCurrency()
        {
            try
            {
                if (!(CheckingFields() == ErrorType.NoError))
                    return;

                if (this.IsUpdate)
                {
                    CurrencyModel currency = new CurrencyModel()
                    {
                        ID = this.CurrID,
                        Name = this.CurrName,
                        PartName = this.CurrPartName,
                        CurrencyValue = double.Parse(this.CurrValue),
                        Number = this.currNumber,
                    };

                    this.currencyBL.UpdateCurrency(currency);

                    MessageBox.Show("تم التعديل");
                }
                else
                {
                    CurrencyModel currency = new CurrencyModel()
                    {
                        Name = this.CurrName,
                        PartName = this.CurrPartName,
                        CurrencyValue = double.Parse(this.CurrValue),
                        Number = this.currNumber,
                    };

                    this.currencyBL.InsertNewCurrency(currency);

                    MessageBox.Show("تم حفظ");
                }
                /////////////////////////////////////
                //when save command recieved 
                //disable saving and enable new and editing
                IsAnyProprtyChanged = false;
                this.ViewMode = ViewModes.Viewing;
                base.ToggleCommandBtns();
                /////////////////////////////////////
            }
            catch (Exception ex)
            {

            }
        }
        protected override void NewView()
        {
            try
            {
                base.ViewMode = ViewModes.Editing;
                //GetBillLastNumber();
                this.CurrNumber = currencyBL.GetMaxCurrencyNumber_1();

                InitializingFields();

                base.ToggleCommandBtns();

                //IsUpdate = false;
                //EnableEditing = true;
                //IsEditting = false;
                //IsDeleting = false;
                //IsSaving = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void DeleteView()
        {
            try
            {
                if (MessageBox.Show("هل تريد حذف العملة", "حذف", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                   if(this.CurrNumber == 1)
                    {
                        MessageBox.Show("لايمكن حذف العملة الاساسية ");
                        return;
                    }
                    if (this.CurrID == 0)
                    {
                        MessageBox.Show("العملة غير موجودة");
                        return;
                    }
                    currencyBL.DeleteCurrency(this.CurrID);
                    // create new bill
                    PreviousCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// eveytime the user navigates though the currency table
        /// the currNumber txtbx text will change
        /// </summary>
        private async void CurrNumberTxtBxTextChanged()
        {
            try
            {
                if (this.ViewMode == ViewModes.Viewing)
                {
                    base.ToggleCommandBtns();
                    this.IsLoading = true;

                    await Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            CurrencyModel currency = this.currencyBL.GetCurrency(this.CurrNumber, "Number");
                            if (currency.ID == 0)
                            {
                                // i added this cs if the user deleted a bill in the middle btw two bills
                                // will still able to see the number for example 1, 2, 3, are 3 bills
                                // and bill number 2 was deleted when navigating with the btns
                                // if 2 shows the GetBill1 will return null
                                // so init field and disable delete and mod
                                InitializingFields();
                                this.IsDeleting = false;
                                this.IsEditting = false;
                                return;
                            }
                            // when the curr number txtbx text changed it means that the 
                            this.CurrID = currency.ID;
                            this.CurrName = currency.Name;
                            this.CurrPartName = currency.PartName;
                            this.CurrValue = currency.CurrencyValue.ToString();
                            /////////////////////////////////////
                        });
                    });
                    await Task.Delay(500);
                    this.IsLoading = false;
                }
            }
            catch (Exception ex) { };
        }
        protected override void PreviousView()
        {
            try
            {
                if (base.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveCurrency();
                    else
                        base.IsAnyProprtyChanged = false;
                }
                else if (!base.IsAnyProprtyChanged)
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;

                    int minBill = currencyBL.GetMinCurrencyNumber();

                    int billNumber = this.CurrNumber - 1;

                    if (billNumber < minBill)
                        return;


                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.CurrNumber = billNumber;

                    //EnableEditing = false;
                    base.ToggleCommandBtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void NextView()
        {
            try
            {

                int max = currencyBL.GetMaxCurrencyNumber();

                int currNumber = this.CurrNumber + 1;

                if (currNumber > max)
                    return;

                // this will cuase the BillNumberTextChnaged to be fired
                // so we do not need to get the bill1 and 2 here 
                this.CurrNumber = currNumber;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void MinView()
        {
            try
            {
                if (this.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveCurrency();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                else
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;
                    this.CurrNumber = currencyBL.GetMinCurrencyNumber();

                    base.ToggleCommandBtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void MaxView()
        {
            try
            {
                if (this.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveCurrency();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                if (!this.IsAnyProprtyChanged)
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;

                    int maxBill = currencyBL.GetMaxCurrencyNumber();

                    int billNumber = this.CurrNumber + 1;

                    if (billNumber > maxBill)
                        return;
                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.CurrNumber = billNumber;


                    //EnableEditing = false;

                    base.ToggleCommandBtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Methods 
        /// <summary>
        /// When the CurrTableView is loaded the is command is exxcuted to get the max curr number plus 1
        /// and set the CurrNumber property
        /// </summary>
        protected override void LoadView()
        {
            try
            {               
                if (this.CurrNumberCache == 0)
                { 
                    
                    base.ViewMode = ViewModes.Editing;

                    this.CurrNumber = this.currencyBL.GetMaxCurrencyNumber_1();

                    InitializingFields();

                    base.ToggleCommandBtns();
                }
                else
                {
                    this.CurrNumber = this.CurrNumberCache;
                    this.CurrNumberCache = 0;
                }
               
            }
            catch (Exception ex) { }
        }
        public override void InitializingFields()
        {
            this.CurrID = 0;
            this.CurrName = string.Empty;
            this.CurrPartName = string.Empty;
            this.CurrValue = string.Empty;
        }
        protected override void UnloadView()
        {
            try
            {
                // when the view first loaded enable new and save
                this.CurrNumberCache = this.CurrNumber;
            }
            catch (Exception ex) { }
        }
        public override ErrorType CheckingFields()
        {
            if (string.IsNullOrEmpty(this.CurrName) || string.IsNullOrWhiteSpace(this.CurrName))
            {
                MessageBox.Show("الاسم فارغ");
                return ErrorType.AccountEmpty;
            }
            if (string.IsNullOrEmpty(this.CurrPartName) || string.IsNullOrWhiteSpace(this.CurrPartName))
            {
                MessageBox.Show("جزءالعملة فارغ");
                return ErrorType.StoreEmpty;
            }
            if (string.IsNullOrEmpty(this.CurrValue) || string.IsNullOrWhiteSpace(this.CurrValue))
            {
                MessageBox.Show("قيمةالعملة فارغ");
                return ErrorType.BillTotalEmpty;
            }

            return ErrorType.NoError;
        }
        #endregion
    }
}
