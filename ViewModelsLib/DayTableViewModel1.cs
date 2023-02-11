using FrameWorkLib;
using ModelsLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BusinessLib;
namespace ViewModelsLib
{
    public class DayTableViewModel1 : ViewModelBase
    {
        private readonly object lockToken = new object();
       
        //private DayTableModel dayTableModel;

        private DaysBL daysBL;
        private AccountsBL accountsBL;
        public int DayNumberCache { get; set; }

        private ObservableCollection<DayRow> dayCollection;
        public ObservableCollection<DayRow> DayCollection
        {
            get
            {
                lock (lockToken)
                {
                    if (dayCollection is null)
                    {
                        return dayCollection = new ObservableCollection<DayRow>();
                    }
                    return dayCollection;
                }
            }
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref dayCollection, value);
                }
            }
        }
        private bool IsAttached { get; set; } = false;
        public bool IsRequestedFromAnotherView { get; set; } = false;
        private int dayID;
        public int DayID
        {
            get => dayID;
            set
            {
                SetProperty(ref dayID, value);
            }
        }
        private string dayNumber;
        public string DayNumber
        {
            get => dayNumber;
            set
            {
                SetProperty(ref dayNumber, value);
            }
        }
        private string dayDate;
        public string DayDate
        {
            get => dayDate;
            set
            {
                SetProperty(ref dayDate, value);

                if (this.ViewMode == ViewModes.Editing)
                {
                    if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
                        this.IsAnyProprtyChanged = true;
                }
                else if (this.ViewMode == ViewModes.Viewing)
                {
                   this.IsAnyProprtyChanged = false;
                }
            }
        }
        private string dayNote;
        public string DayNote
        {
            get => dayNote;
            set
            {
                SetProperty(ref dayNote, value);
                if (this.ViewMode == ViewModes.Editing)
                {
                    if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
                        this.IsAnyProprtyChanged = true;
                }
                else if (this.ViewMode == ViewModes.Viewing)
                {
                    this.IsAnyProprtyChanged = false;
                }
            }
        }
        private int dayRowIndex;
        public int DayRowIndex
        {
            get => dayRowIndex;
            set
            {
                SetProperty(ref dayRowIndex, value);
            }
        }
        private decimal totalDebit;
        public decimal TotalDebit
        {
            get => totalDebit;
            set
            {
                SetProperty(ref totalDebit, value);
            }
        }
        private decimal totalCredit;
        public decimal TotalCredit
        {
            get => totalCredit;
            set
            {
                SetProperty(ref totalCredit, value);
            }
        }
        private bool isSelectedAccountNameChanged;
        private bool IsSelectedAccountNameChanged
        {
            get => isSelectedAccountNameChanged;
            set => isSelectedAccountNameChanged = value;
        }
        #region Commands region
        public ICommand AddRowCommand { get; set; }
        public ICommand RemoveRowCommand { get; set; }
        public ICommand RepeatRowCommand { get; set; }
        public ICommand AccountNameSelectionChangedChangedCommand { get; set; }
        public ICommand AccountNameTextChangedCommand { get; set; }
        public ICommand SearchAccountNameCommand { get; set; }
        public ICommand AccountCodeSearchCommand { get; set; }
        public ICommand DayNumberTextChangedCommand { get; set; }
        public ICommand DayRowTotalCellTextChangedCommand { get; set; }
        #endregion
        public DayTableViewModel1()
        {
            daysBL = new DaysBL();
            accountsBL = new AccountsBL();
            AddRowCommand = new DelegateCommand(AddRow);
            RemoveRowCommand = new DelegateCommand(RemoveRow);
            RepeatRowCommand = new DelegateCommand(RepeatRow);

            AccountNameSelectionChangedChangedCommand = new DelegateCommand(AccountNameSelectionChangedChanged);
            AccountNameTextChangedCommand = new DelegateCommand(AccountNameTextChanged);
            SearchAccountNameCommand = new DelegateCommand(SearchAccountName);
            AccountCodeSearchCommand = new DelegateCommand(AccountCodeSearch);

            DayNumberTextChangedCommand = new DelegateCommand(DayNumberTextChanged);
            DayRowTotalCellTextChangedCommand = new DelegateCommand(DayRowTotalCellTextChanged);

            this.DayDate = DateTime.Today.ToShortDateString();


        }
        protected override void DeleteView()
        {
            //throw new NotImplementedException();
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
        private void DayRowTotalCellTextChanged()
        {
            try
            {
                if (this.DayCollection.Count == 0)
                    return;
                this.TotalCredit = 0; this.TotalDebit = 0;
                foreach (DayRow dayRow in this.DayCollection)
                {
                    this.TotalCredit += (dayRow.Total - decimal.Parse(dayRow.Debit));
                    this.TotalDebit += (dayRow.Total - decimal.Parse(dayRow.Credit));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        protected override void SaveView()
        {
            try
            {
                if (MessageBox.Show("هل تريد حفظ السند", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Save();
                }
            }
            catch (Exception ex) { }
        }
        public async void Save()
        {

            if (!IsUpdate)
            {
                if (this.TotalCredit == 0 || this.TotalDebit == 0)
                {
                    MessageBox.Show("الدائن او الدين = 0");
                    return;
                }
                if ((this.TotalDebit - this.TotalCredit) != 0)
                {
                    MessageBox.Show("القيد غير متزن");
                    return;
                }
                //TODO :  check if all the day2 is empty 
                if (this.dayCollection.Count == 0)
                {
                    MessageBox.Show("القيد فارغ");
                    return;
                }
                foreach (DayRow dayRow in this.DayCollection)
                {
                    if (String.IsNullOrEmpty(dayRow.AccountName) && dayRow.Total != 0)
                    {
                        MessageBox.Show("اسم الحساب فارغ");
                        return;
                    }
                }
                IsLoading = true; // show loading spiner
                IsSaving = false; // it will not allow saving
                                  // first insert day1 and wait for it to finish
                await Task.Run(() =>
                {

                    // day1 data type
                    int dayType = 0;  // سند قيد الدرس 29
                    this.DayNote = this.DayNumber + "/" + "سند قيد";
                    Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                    (
                        DateTime.Parse(this.DayDate), this.DayNote, dayType, this.DayNote, int.Parse(this.DayNumber)
                    );

                    daysBL.InsertDay1(parametersTupleDay1);
                    // after we save day1 take number and get the id
                    this.DayID = daysBL.GetDay1ID(int.Parse(this.DayNumber));

                    // day2 data type
                    AddDay2();
                });
                
                IsLoading = false; // show loading spiner
                MessageBox.Show("تم الحفظ");
            }
            else if (IsUpdate)
            {
                if(this.DayID == 0)
                {
                    MessageBox.Show("لايمكن تعديل هذا القيد لانه كان مربوط بعملية اخرى و قد تم حذفها");
                    goto lable1;
                }
                await Task.Run(() =>
                {

                    // day1 data type
                    int dayType = 0;  // سند قيد الدرس 29
                    this.DayNote = this.DayNumber + "/" + "سند قيد";
                    int dayid = this.DayID;
                    Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                    (
                        DateTime.Parse(this.DayDate), this.DayNote, dayType, this.DayNote, int.Parse(this.DayNumber)
                    );

                    daysBL.UpdateDay1(parametersTupleDay1, dayid);

                    // day2 
                    // first delete it
                    daysBL.DeleteDay2(dayid);
                    // add the new rows
                    AddDay2();

                });
                IsLoading = false; // show loading spiner
                MessageBox.Show("تم حفظ التعديلات");
            }
            lable1:
            IsAnyProprtyChanged = false;
            this.ViewMode = ViewModes.Viewing;
            base.ToggleCommandBtns();
        }
        private bool AddDay2()
        {
            try
            {
                List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();
                foreach (DayRow dayRow in this.DayCollection)
                {
                    if (!String.IsNullOrEmpty(dayRow.AccountName) && dayRow.Total != 0)
                    {
                        // الدرس 25
                        if (String.IsNullOrEmpty(dayRow.Note))
                            dayRow.Note = this.DayNumber + "/" + "سند قيد";
                        Tuple<int, int, float, float, string> parametersTupleDay2 = new Tuple<int, int, float, float, string>
                            (
                                this.DayID,
                                dayRow.SelectedAccountID,
                                float.Parse(dayRow.Debit),
                                float.Parse(dayRow.Credit),
                                dayRow.Note
                            );
                        list.Add(parametersTupleDay2);
                    }
                }
                daysBL.InsertDay2(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        protected override void NewView()
        {
            try
            {
                base.ViewMode = ViewModes.Editing;
                this.DayNumber = this.daysBL.GetMax_1DayNumber().ToString();

                InitializingFields();

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
                if (this.DayNumberCache == 0)
                {
                    base.ViewMode = ViewModes.Editing;

                    this.DayNumber = this.daysBL.GetMax_1DayNumber().ToString();

                    InitializingFields();

                    base.ToggleCommandBtns();

                }
                else
                {
                    this.DayNumber = this.DayNumberCache.ToString();
                    // go and grab it from data base
                    if (this.IsRequestedFromAnotherView)
                    {
                        DayNumberTextChangedCommand.Execute(null);
                        this.IsRequestedFromAnotherView = false;
                    }
                    this.DayNumberCache = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void UnloadView()
        {
            try
            {
                this.DayNumberCache = int.Parse(this.DayNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public override void InitializingFields()
        {

            this.DayNote = string.Empty;
            this.TotalDebit = 0;
            this.TotalCredit = 0;

            this.DayCollection.Clear();
        }
        private async void DayNumberTextChanged()
        {
            try
            {
                if (string.IsNullOrEmpty(this.DayNumber) || string.IsNullOrWhiteSpace(this.DayNumber))
                    return;

                if (this.DayCollection.Count > 0)
                    this.DayCollection.Clear();

                if (base.ViewMode == ViewModes.Viewing)
                {
                    this.IsLoading = true;
                    await Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                        // day 1
                        Tuple<int, DateTime, string, int> day1Table = daysBL.GetDay1(int.Parse(this.DayNumber));

                        // day 2
                        this.DayID = day1Table.Item1;
                            int parentID = day1Table.Item1;
                            int dayType = day1Table.Item4;
                        // this means if the bay is assosiated with a bill or anything it is not possible to modify it 
                        if (dayType != 0)
                        {
                                this.IsAttached = true;
                        }
                        else if (dayType == 0)
                        {
                                this.IsAttached = false;
                        }
                        List<Tuple<double, double, int, string, int, int, string>> day2List = daysBL.GetDay2(parentID);

                        // fill day view

                        this.DayDate = day1Table.Item2.ToString();

                            this.DayNote = day1Table.Item3;

                        // day view
                        this.DayCollection.Clear();
                            this.TotalCredit = 0; this.TotalDebit = 0;
                            foreach (Tuple<double, double, int, string, int, int, string> tuple in day2List)
                            {
                                DayRow dayRow = new DayRow();
                                dayRow.SelectedAccountID = tuple.Item6;
                                dayRow.AccountCode = tuple.Item5.ToString();
                                dayRow.AccountName = tuple.Item4.ToString();
                                dayRow.Debit = tuple.Item1.ToString();
                                dayRow.Credit = tuple.Item2.ToString();
                                dayRow.Note = tuple.Item7.ToString();
                                this.TotalCredit += (decimal)tuple.Item2;
                                this.TotalDebit += (decimal)tuple.Item1;
                                this.DayCollection.Add(dayRow);
                            }
                        });
                    });
                    base.ToggleCommandBtns();

                    // this is a special case for daysView only
                    if(this.IsAttached)
                    {
                        // disable deleting, editing, and saving
                        base.IsDeleting = base.IsEditting = base.IsSaving = false;
                        this.IsAttached = false;
                    }
                }
                await Task.Delay(500);
               
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
        protected override void MinView()
        {
            throw new NotImplementedException();
        }
        protected override void MaxView()
        {
            throw new NotImplementedException();
        }
        protected override void PreviousView()
        {
            try
            {
                if (base.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        Save();
                    else
                        base.IsAnyProprtyChanged = false;
                }
                else if (!base.IsAnyProprtyChanged)
                {
                    base.ViewMode = ViewModes.Viewing;

                    int minBill = daysBL.GetMinDayNumber();

                    int billNumber = int.Parse(this.DayNumber) - 1;

                    if (billNumber < minBill)
                        return;
                    //IsEnable_Date = false;
                    //IsEnable = true;

                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.DayNumber = billNumber.ToString();
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
                if (base.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        Save();
                    else
                        IsAnyProprtyChanged = false;
                }
                else if (!base.IsAnyProprtyChanged)
                {
                    int maxBill = daysBL.GetMaxDayNumber();

                    int billNumber = int.Parse(this.DayNumber) + 1;

                    if (billNumber > maxBill)
                        return;
                    //IsEnable_Date = false;
                    //IsEnable = true;
                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.DayNumber = billNumber.ToString();
                    base.ToggleCommandBtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// we will implemnt this later
        /// </summary>
        private void AccountCodeSearch()
        {
            //try
            //{
            //    int rowIndex = this.DayRowIndex;
            //    // get the row that reqused the item table
            //    DayRow dayRow = this.DayCollection[rowIndex];
            //    int accountCode = int.Parse(dayRow.AccountCode);
            //    Tuple<int, int, string> tuple = dayTableModel.GetAccounts(accountCode);
            //    if (tuple is null)
            //        return;
            //    dayRow.ItemName = tuple.Item3;
            //    dayRow.SelectedItemID = tuple.Item1;
            //    this.AddRow();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private async void SearchAccountName()
        {
            int rowIndex = this.DayRowIndex;
            // get the row that reqused the item table
            DayRow dayRow = this.DayCollection[rowIndex];
            dayRow.IsAccountNameDropOpened = true;
            dayRow.IsLoadingAccountCmBx = true;
            try
            {
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        dayRow.AccountsList = accountsBL.GetAccounts1();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dayRow.IsLoadingAccountCmBx = false;
            }
        }
        private async void AccountNameTextChanged()
        {
           int rowIndex = this.DayRowIndex;
            // get the row that reqused the item table
            DayRow dayRow = this.DayCollection[rowIndex];

            // it means still loading from prev task
            if (this.IsLoading || dayRow.IsLoadingAccountCmBx)
                return;

           if (IsSelectedAccountNameChanged)
            {
                IsSelectedAccountNameChanged = false;
                return;
            }

            dayRow.IsAccountNameDropOpened = true;
            dayRow.IsLoadingAccountCmBx = true;

            string textEntered = dayRow.AccountName;

            if (String.IsNullOrEmpty(textEntered))
            {
                dayRow.IsAccountNameDropOpened = false;
                dayRow.IsLoadingAccountCmBx = false;
                dayRow.AccountsList = null;
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        dayRow.AccountsList = accountsBL.GetAccounts1(textEntered);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dayRow.IsLoadingAccountCmBx = false;
                dayRow.IsAccountNameDropOpened = true;

            }
        }
        /// <summary>
        /// this just to clear the the list after selection is made
        /// since it did not work from the MycomboBox class
        /// </summary>
        private void AccountNameSelectionChangedChanged()
        {
            
            /*try
            {
                i++;
                int rowIndex = this.DayRowIndex;
                // get the row that reqused the item table
                DayRow dayRow = this.DayCollection[rowIndex];

                dayRow.AccountCode = dayRow.SelectedAccountCode;
                IsSelectedAccountNameChanged = true;

                if (i == 2)
                {
                    this.AddRow();
                    i = 0;
                    IsSelectedAccountNameChanged = false;
                }
                dayRow.AccountsList = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }*/
        }
        private void RepeatRow()
        {
            try
            {

                DayRow selectedRow = this.DayCollection[this.DayRowIndex];
                DayRow newDayRow = selectedRow.Copy();
                this.DayCollection.Insert(this.DayRowIndex + 1, newDayRow);
                DayRowTotalCellTextChangedCommand.Execute(null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveRow()
        {
            try
            {
                this.DayCollection.RemoveAt(this.DayRowIndex);
                DayRowTotalCellTextChangedCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddRow()
        {
            try
            {
                DayRow DayRow = new DayRow();
                this.DayCollection.Add(DayRow);

                if (this.ViewMode == ViewModes.Editing)
                {
                    this.IsAnyProprtyChanged = true;
                }
                else if (this.ViewMode == ViewModes.Viewing)
                {
                    this.IsAnyProprtyChanged = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class DayRow : BindableBase
    {
        private readonly object lockToken = new object();

        private string accountCode = string.Empty;
        public string AccountCode
        {
            get => accountCode;
            set
            {
                SetProperty(ref accountCode, value);
            }
        }

        private string accountName = string.Empty;
        public string AccountName
        {
            get => accountName;
            set
            {
                SetProperty(ref accountName, value);
            }
        }
        private int selectedAccountID;
        public int SelectedAccountID
        {
            get => selectedAccountID;
            set
            {
                SetProperty(ref selectedAccountID, value);
            }
        }

        private string selectedAccountCode = string.Empty;
        public string SelectedAccountCode
        {
            get => selectedAccountCode;
            set
            {
                SetProperty(ref selectedAccountCode, value);
            }
        }

        private string debit = "0";
        public string Debit
        {
            get => debit;
            set
            {
                SetProperty(ref debit, value);
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        Total = decimal.Parse(debit) + decimal.Parse(credit);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Total = 0;
                }
            }
        }
        private string credit = "0";
        public string Credit
        {
            get
            {
                return credit;
            }
            set
            {
                SetProperty(ref credit, value);
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        Total = decimal.Parse(debit) + decimal.Parse(credit);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Total = 0;
                }
            }
        }
        private decimal total;
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                SetProperty(ref total, value);
            }
        }
        private string note = string.Empty;
        public string Note
        {
            get => note;
            set
            {
                SetProperty(ref note, value);
            }
        }
        private List<Tuple<int, int, string>> accountsList;
        public List<Tuple<int, int, string>> AccountsList
        {
            get
            {
                lock (lockToken)
                {

                    return accountsList;
                }
            }
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref accountsList, value);
                }
            }
        }
        public DayRow Copy()
        {
            DayRow dayRow = new DayRow();
            dayRow.accountCode = this.AccountCode;
            dayRow.AccountName = this.AccountName;
            dayRow.Debit = this.Debit;
            dayRow.Credit = this.Credit;
            dayRow.Note = this.Note;

            if (!(this.AccountsList is null))
            {
                foreach (var ele in this.AccountsList)
                dayRow.AccountsList.Add(ele);
            }
            
            return dayRow;
        }
        private bool isAccountNameDropOpened;
        public bool IsAccountNameDropOpened
        {
            get => isAccountNameDropOpened;
            set
            {
                SetProperty(ref isAccountNameDropOpened, value);
            }
        }
        private bool isLoadingAccountCmBx = false;
        public bool IsLoadingAccountCmBx
        {
            get => isLoadingAccountCmBx;
            set
            {
                SetProperty(ref isLoadingAccountCmBx, value);
            }
        }
    }
}
