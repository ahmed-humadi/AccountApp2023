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
    public class CashDayViewModel : ViewModelBase
    {
        //private CashDayModel cashDayModel;
        private CashDayBL cashDayBL;
        private AccountsBL accountsBL;
        private DaysBL daysBL;

        private readonly object lockToken = new object();

        private ObservableCollection<CashDayRow> cashedDayCollection;
        public ObservableCollection<CashDayRow> CashDayCollection
        {
            get
            {
                lock (lockToken)
                {
                    if (cashedDayCollection is null)
                    {
                        return cashedDayCollection = new ObservableCollection<CashDayRow>();
                    }
                    return cashedDayCollection;
                }
            }
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref cashedDayCollection, value);
                }
            }
        }
        private int CashDayNumberCache { get; set; }

        private int dayID;
        public int DayID
        {
            get => dayID;
            set
            {
                SetProperty(ref dayID, value);
            }
        }
        private int dayNumber;
        public int DayNumber
        {
            get => dayNumber;
            set
            {
                SetProperty(ref dayNumber, value);
            }
        }
        private int cashDayID;
        public int CashDayID
        {
            get => cashDayID;
            set
            {
                SetProperty(ref cashDayID, value);
            }
        }
        private string cashDayDate;
        public string CashDayDate
        {
            get => cashDayDate;
            set
            {
                SetProperty(ref cashDayDate, value);
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
        private string cashDayTypeID;
        public string CashDayTypeID
        {
            get => cashDayTypeID;
            set
            {
                SetProperty(ref cashDayTypeID, value);
            }
        }
        private string cashDayTypeNumber;
        public string CashDayTypeNumber
        {
            get => cashDayTypeNumber;
            set
            {
                SetProperty(ref cashDayTypeNumber, value);
            }
        }
        private string cashDayTypeName;
        public string CashDayTypeName
        {
            get => cashDayTypeName;
            set
            {
                SetProperty(ref cashDayTypeName, value);
            }
        }
        private string cashDayNote;
        public string CashDayNote
        {
            get => cashDayNote;
            set
            {
                SetProperty(ref cashDayNote, value);
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
        private string cashDayTotal;
        public string CashDayTotal
        {
            get => cashDayTotal;
            set
            {
                SetProperty(ref cashDayTotal, value);
            }
        }
        private string cashDayNumber;
        public string CashDayNumber
        {
            get => cashDayNumber;
            set
            {
                SetProperty(ref cashDayNumber, value);
            }
        }
        private bool isDebitVisible;
        public bool IsDebitVisible
        {
            get => isDebitVisible;
            set
            {
                SetProperty(ref isDebitVisible, value);
            }
        }
        private bool isCreditVisible;
        public bool IsCreditVisible
        {
            get => isCreditVisible;
            set
            {
                SetProperty(ref isCreditVisible, value);
            }
        }
        private int cashDayRowIndex;
        public int CashDayRowIndex
        {
            get => cashDayRowIndex;
            set
            {
                SetProperty(ref cashDayRowIndex, value);
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

        #region CashDay Account Properties

        private string cashDayAccountName;
        public string CashDayAccountName
        {
            get => cashDayAccountName;
            set
            {
                SetProperty(ref cashDayAccountName, value);
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
        private int selectedCashDayAccountID;
        public int SelectedCashDayAccountID
        {
            get => selectedCashDayAccountID;
            set
            {
                SetProperty(ref selectedCashDayAccountID, value);
            }
        }
        private List<Tuple<int, string>> cashDayAccountList;
        public List<Tuple<int, string>> CashDayAccountList
        {
            get => cashDayAccountList;
            set
            {
                SetProperty(ref cashDayAccountList, value);
            }
        }
        private bool isLoading_Account;
        public bool IsLoading_Account
        {
            get => isLoading_Account;
            set
            {
                SetProperty(ref isLoading_Account, value);
            }
        }
        private bool cashDayAccountIsDropOpen;
        public bool CashDayAccountIsDropOpen
        {
            get => cashDayAccountIsDropOpen;
            set
            {
                SetProperty(ref cashDayAccountIsDropOpen, value);
            }
        }
        private bool isSelectedAccountNameChanged;
        private bool IsSelectedAccountNameChanged
        {
            get => isSelectedAccountNameChanged;
            set => isSelectedAccountNameChanged = value;
        }

        #endregion
        public ICommand AddRowCommand { get; set; }
        public ICommand RemoveRowCommand { get; set; }
        public ICommand RepeatRowCommand { get; set; }
        public ICommand DayNumberTextChangedCommand { get; set; }
        #region Account TextBox Command
        public ICommand AccountNameTextChangedCommand { get; set; }
        public ICommand AccountNameSelectionChnagedCommand { get; set; }
        public ICommand AccountNameSearchCommand { get; set; }
        public ICommand DayRowTotalCellTextChangedCommand { get; set; }
        #endregion

        // this is the one inside the listView it is difere from the one in the header of the cash day
        public ICommand AccountNameSelectionChangedChangedCommandList { get; set; }
        public ICommand AccountNameTextChangedCommandList { get; set; }
        public ICommand SearchAccountNameCommandList { get; set; }

        public ICommand ShowAssociatedDayCommand { get; set; }

        public event EventHandler AskForShowDayView;
        protected void RaiseAskForShowDayView(int dayNumer)
        {
            AskForShowDayView?.Invoke(this, new ViewModelEventArgs.ViewModelEventArgs() { DayNumber = dayNumer });
        }
        public CashDayViewModel()
        {
            //cashDayModel = new CashDayModel();
            cashDayBL = new CashDayBL();
            daysBL = new DaysBL();
            accountsBL = new AccountsBL();
            AccountNameTextChangedCommand = new DelegateCommand(AccountNameTextChanged);
            AccountNameSelectionChnagedCommand = new DelegateCommand(AccountNameSelectionChnaged);
            AccountNameSearchCommand = new DelegateCommand(AccountNameSearch);

            AccountNameSelectionChangedChangedCommandList = new DelegateCommand(AccountNameSelectionChangedChangedList);
            AccountNameTextChangedCommandList = new DelegateCommand(AccountNameTextChangedList);
            SearchAccountNameCommandList = new DelegateCommand(SearchAccountNameList);

            DayRowTotalCellTextChangedCommand = new DelegateCommand(DayRowTotalCellTextChanged);
            DayNumberTextChangedCommand = new DelegateCommand(CashDayNumberTextChnaged);

            AddRowCommand = new DelegateCommand(AddRow);
            RemoveRowCommand = new DelegateCommand(RemoveRow);
            RepeatRowCommand = new DelegateCommand(RepeatRow);

            ShowAssociatedDayCommand = new DelegateCommand(ShowAssociatedDay);


            this.CashDayDate = DateTime.Today.ToShortDateString();
        }
        private void ShowAssociatedDay()
        {
            try
            {
                int dayNubmer;
                if (!int.TryParse(this.DayNumber.ToString(), out dayNubmer))
                {
                    MessageBox.Show("لايوجد سند قيد");
                    return;
                }
                RaiseAskForShowDayView(dayNubmer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        protected override void NewView()
        {
            try
            {
                base.ViewMode = ViewModes.Editing;
                this.CashDayNumber = this.cashDayBL.GetMax1CashDayNumber(int.Parse(this.CashDayTypeID)).ToString();

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
            if (this.CashDayTypeNumber == 0.ToString()) // قبض
            {
                IsCreditVisible = true;
                IsDebitVisible = false;
            }
            else if (this.CashDayTypeNumber == 1.ToString()) // صرف
            {
                IsCreditVisible = false;
                IsDebitVisible = true;
            }
            if (this.CashDayNumberCache == 0)
            {
                base.ViewMode = ViewModes.Editing;

                this.CashDayNumber = this.cashDayBL.GetMax1CashDayNumber(int.Parse(this.CashDayTypeID)).ToString();

                InitializingFields();

                base.ToggleCommandBtns();
            }
            else
            {
                // this will not trigger the textchanged event
                this.CashDayNumber = this.CashDayNumberCache.ToString();
                this.CashDayNumberCache = 0;
            }
            //CreateNewCashDay();

        }
        public override void InitializingFields()
        {
            this.SelectedCashDayAccountID = 0;
            this.CashDayAccountName = null;
            this.CashDayNote = string.Empty;
            this.CashDayTotal = "0";
            this.CashDayCollection.Clear();
            this.DayNumber = 0;
        }
        protected override void UnloadView()
        {
            try
            {
                this.CashDayNumberCache = int.Parse(this.CashDayNumber);
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
                if (this.CashDayCollection.Count == 0)
                    return;
                this.TotalCredit = 0; this.TotalDebit = 0;
                foreach (CashDayRow dayRow in this.CashDayCollection)
                {
                    this.TotalCredit += (dayRow.Total - decimal.Parse(dayRow.Debit));
                    this.TotalDebit += (dayRow.Total - decimal.Parse(dayRow.Credit));
                }
                if (this.CashDayTypeNumber == 0.ToString()) // قبض
                {
                    this.CashDayTotal = this.TotalCredit.ToString();
                }
                else if (this.CashDayTypeNumber == 1.ToString()) // صرف
                {
                    this.CashDayTotal = this.TotalDebit.ToString();
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
        private void AccountNameSelectionChnaged()
        {
            try
            {
                this.CashDayAccountList = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void AccountNameTextChanged()
        {
            try
            {
                string searchname = this.CashDayAccountName;

                this.IsLoading_Account = true;

                this.CashDayAccountIsDropOpen = true;


                if (String.IsNullOrEmpty(searchname))
                {
                    this.CashDayAccountIsDropOpen = false;
                    this.SelectedCashDayAccountID = 0;
                    this.CashDayAccountList = null;
                    return;
                }

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.CashDayAccountList = accountsBL.GetAccounts(searchname);

                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Account = false;
            }
        }
        private async void AccountNameSearch()
        {
            try
            {
                this.IsLoading_Account = true;

                this.CashDayAccountIsDropOpen = true;

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.CashDayAccountList = this.accountsBL.GetAccounts();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Account = false;
            }
        }
        private void RepeatRow()
        {
            try
            {

                CashDayRow selectedRow = this.CashDayCollection[this.CashDayRowIndex];
                CashDayRow newDayRow = selectedRow.Copy();
                this.CashDayCollection.Insert(this.CashDayRowIndex + 1, newDayRow);
                //DayRowTotalCellTextChangedCommand.Execute(null);

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
                this.CashDayCollection.RemoveAt(this.CashDayRowIndex);
                //DayRowTotalCellTextChangedCommand.Execute(null);
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
                CashDayRow DayRow = new CashDayRow();
                this.CashDayCollection.Add(DayRow);

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
        private async void SearchAccountNameList()
        {
            int rowIndex = this.CashDayRowIndex;
            // get the row that reqused the item table
            CashDayRow dayRow = this.CashDayCollection[rowIndex];
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
        private async void AccountNameTextChangedList()
        {
            int rowIndex = this.CashDayRowIndex;
            // get the row that reqused the item table
            CashDayRow dayRow = this.CashDayCollection[rowIndex];

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
            }
        }
        int i = 0; // becuse this event occurs two times
        private void AccountNameSelectionChangedChangedList()
        {
            try
            {
                i++;
                int rowIndex = this.CashDayRowIndex;
                // get the row that reqused the item table
                CashDayRow dayRow = this.CashDayCollection[rowIndex];

                dayRow.AccountCode = dayRow.SelectedAccountCode;
                IsSelectedAccountNameChanged = true;

                if (i == 2)
                {
                    this.AddRow();
                    i = 0;
                    IsSelectedAccountNameChanged = false;
                }
                //dayRow.AccountsList = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private async void CashDayNumberTextChnaged()
        {
            try
            {
                if (string.IsNullOrEmpty(this.CashDayNumber) || string.IsNullOrWhiteSpace(this.CashDayNumber))
                    return;
                if (this.CashDayCollection.Count > 0)
                    this.CashDayCollection.Clear();
                if (this.ViewMode == ViewModes.Viewing)
                {
                    base.ToggleCommandBtns();
                    this.IsLoading = true;
                    await Task.Delay(500);
                    await Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            // CashDay
                            Tuple<int, int, string, int, string, DateTime> cashDay =
                                cashDayBL.GetCashDay(int.Parse(CashDayNumber), int.Parse(this.CashDayTypeID));
                            if (cashDay is null)
                                return;
                            this.CashDayID = cashDay.Item1;
                            this.SelectedCashDayAccountID = cashDay.Item2;
                            this.CashDayAccountName = cashDay.Item3;
                            this.CashDayNote = cashDay.Item5;
                            this.CashDayDate = cashDay.Item6.ToShortDateString();
                            // day1
                            Tuple<int, int> tuple1 = daysBL.GetDay1NumberAndID(this.CashDayID);
                            int day1Number = tuple1.Item1;
                            int day1ID = tuple1.Item2;
                            this.DayNumber = day1Number;
                            this.DayID = day1ID;
                            // day2
                            int bill2ParentID = day1ID;

                            List<Tuple<double, double, int, string, int, int, string>> bill2Rows =
                                daysBL.GetDay2(bill2ParentID);
                            if (bill2Rows.Count == 0)
                            {
                                this.CashDayTotal = "0";
                                return;
                            }
                            // skip first row
                            for (int i = 1; i < bill2Rows.Count; i++)
                            {
                                var tuple = bill2Rows[i];

                                CashDayRow day2Row = new CashDayRow();
                                day2Row.Debit = Convert.ToString(tuple.Item1);
                                day2Row.Credit = Convert.ToString(tuple.Item2);
                                day2Row.AccountName = tuple.Item4;
                                day2Row.AccountCode = tuple.Item5.ToString();
                                day2Row.SelectedAccountID = tuple.Item6;
                                day2Row.Note = tuple.Item7;
                                this.CashDayCollection.Add(day2Row);

                            }
                            // to update the total
                            DayRowTotalCellTextChanged();
                            // and get the associated day number
                        });
                    });
                }
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
        protected override void PreviousView()
        {
            try
            {
                if (IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveCashAsync();
                    else
                        IsAnyProprtyChanged = false;
                }
                else if (!base.IsAnyProprtyChanged)
                {
                    this.ViewMode = ViewModes.Viewing;

                    int minCashDay = cashDayBL.GetMinCashDayNumber(int.Parse(this.CashDayTypeID));

                    int cashDayNumber = int.Parse(this.CashDayNumber) - 1;

                    if (cashDayNumber < minCashDay)
                        return;

                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.CashDayNumber = cashDayNumber.ToString();

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
                if (IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveCashAsync();
                    else
                        IsAnyProprtyChanged = false;
                }
                if (!this.IsAnyProprtyChanged)
                {
                    this.ViewMode = ViewModes.Viewing;

                    int maxCashDay = cashDayBL.GetMaxCashDayNumber(int.Parse(this.CashDayTypeID));

                    int cashDayNumber = int.Parse(this.CashDayNumber) + 1;

                    if (cashDayNumber > maxCashDay)
                        return;
                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.CashDayNumber = cashDayNumber.ToString();

                    base.ToggleCommandBtns();
                }
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
                        SaveCashAsync();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                else
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;
                    this.CashDayNumber = cashDayBL.GetMinCashDayNumber(int.Parse(this.CashDayTypeID)).ToString();

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
                        SaveCashAsync();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                else
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;
                    this.CashDayNumber = cashDayBL.GetMaxCashDayNumber(int.Parse(this.CashDayTypeID)).ToString();
                    //EnableEditing = false;

                    base.ToggleCommandBtns();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void SaveView()
        {
            try
            {
                if (MessageBox.Show("هل تريد حفظ السند", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SaveCashAsync();
                }
            }
            catch (Exception ex) { }
        }

        private int day1ID;
        public async void SaveCashAsync()
        {
            try
            {
                this.IsLoading = true;
                if (!(CheckingFields() == ErrorType.NoError))
                    return;

                if (!base.IsUpdate)
                {
                    
                    // first insert day1 and wait for it to finish
                    await Task.Run(() =>
                    {

                        // CashDay
                        int dayType = int.Parse(this.CashDayTypeID);  // 
                        if (String.IsNullOrEmpty(this.CashDayNote))
                            this.CashDayNote = this.CashDayNumber + "/" + this.CashDayTypeName;
                        Tuple<int, string, int, int, DateTime> parametersTupleCashDay = new Tuple<int, string, int, int, DateTime>
                        (
                            this.SelectedCashDayAccountID, this.CashDayNote, dayType,
                            int.Parse(this.CashDayNumber), DateTime.Parse(this.CashDayDate)
                        );

                        cashDayBL.InsertCashDay(parametersTupleCashDay);
                        // after we save day1 take number and get the id
                        this.CashDayID = cashDayBL.GetCashDayID(int.Parse(this.CashDayNumber), int.Parse(this.CashDayTypeID));
                        // day1
                        string dayNote = this.CashDayNumber + "/" + this.CashDayTypeName;
                        int dayNumber = daysBL.GetMax_1DayNumber();
                        Tuple<DateTime, string, int, string, int> day1 = new Tuple<DateTime, string, int, string, int>
                        (
                            DateTime.Parse(this.CashDayDate), dayNote, this.CashDayID, dayNote, dayNumber
                        );
                        daysBL.InsertDay1(day1);
                        day1ID = daysBL.GetDay1ID(dayNumber);
                        this.DayID = day1ID;
                        this.DayNumber = dayNumber;
                    }).ContinueWith(async (task) => await AddDay2());

                    this.IsLoading = false;
                    MessageBox.Show("تم الحفظ");
                }
                else if (base.IsUpdate)
                {
                    await Task.Run(() =>
                    {

                        // day1 data type
                        int dayType = int.Parse(this.CashDayTypeID);  //   
                        if (String.IsNullOrEmpty(this.CashDayNote))
                            this.CashDayNote = this.CashDayNumber + "/" + this.CashDayTypeName;
                        int cashDayID = this.CashDayID;
                        Tuple<int, string, int, int, DateTime> paraCashDay = new Tuple<int, string, int, int, DateTime>
                        (
                           this.SelectedCashDayAccountID, this.CashDayNote, dayType,
                           int.Parse(this.CashDayNumber), DateTime.Parse(this.CashDayDate)
                        );

                        cashDayBL.UpdateCashDay(paraCashDay, this.CashDayID);


                        // delete days day 2 will be deleted automaticlly be the trigger
                        daysBL.DeleteDay1(this.cashDayID);

                        // add day1
                        string dayNote = this.CashDayNumber + "/" + this.CashDayTypeName;
                        int dayNumber = this.DayNumber;
                        this.DayNumber = dayNumber;
                        Tuple<DateTime, string, int, string, int> day1 = new Tuple<DateTime, string, int, string, int>
                        (
                            DateTime.Parse(this.CashDayDate), dayNote, this.CashDayID, dayNote, dayNumber
                        );
                        daysBL.InsertDay1(day1);
                        day1ID = daysBL.GetDay1ID(dayNumber);
                        this.DayID = day1ID;

                    }).ContinueWith(async (task) => await AddDay2());

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
        }
        private Task AddDay2()
        {

            return Task.Run(() =>
            {
                if (int.Parse(this.CashDayTypeNumber) == 0) // سند قبض
                {
                    List<Tuple<int, int, float, float, string>> day2List = new List<Tuple<int, int, float, float, string>>();
                    string note = this.CashDayNumber + "/" + this.CashDayTypeName;
                    // من حساب الصندوق
                    Tuple<int, int, float, float, string> tuple = new Tuple<int, int, float, float, string>
                    (
                        day1ID, this.SelectedCashDayAccountID, float.Parse(this.CashDayTotal)
                        , 0.0f, note
                    );
                    day2List.Add(tuple);
                    // الى حساب الاقلام
                    foreach (CashDayRow cashDayRow in this.CashDayCollection)
                    {
                        if (!String.IsNullOrEmpty(cashDayRow.AccountName) && cashDayRow.Total != 0)
                        {
                            if (String.IsNullOrEmpty(cashDayRow.Note))
                                cashDayRow.Note = this.CashDayNumber + "/" + this.CashDayTypeName;
                            Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                            (
                                day1ID, cashDayRow.SelectedAccountID, 0.0f
                                , float.Parse(cashDayRow.Credit), cashDayRow.Note
                            );
                            day2List.Add(tuple1);
                        }
                    }
                    daysBL.InsertDay2(day2List);
                }
                else if (int.Parse(this.CashDayTypeNumber) == 1) // سند صرف 
                {
                    List<Tuple<int, int, float, float, string>> day2List = new List<Tuple<int, int, float, float, string>>();
                    string note = this.CashDayNumber + "/" + this.CashDayTypeName;
                    // الى حساب الصندوق
                    Tuple<int, int, float, float, string> tuple = new Tuple<int, int, float, float, string>
                    (
                        day1ID, this.SelectedCashDayAccountID, 0.0f
                        , float.Parse(this.CashDayTotal), note
                    );
                    day2List.Add(tuple);
                    // من حساب الاقلام
                    foreach (CashDayRow cashDayRow in this.CashDayCollection)
                    {
                        if (!String.IsNullOrEmpty(cashDayRow.AccountName) && cashDayRow.Total != 0)
                        {
                            if (String.IsNullOrEmpty(cashDayRow.Note))
                                cashDayRow.Note = this.CashDayNumber + "/" + this.CashDayTypeName;
                            Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                            (
                                day1ID, cashDayRow.SelectedAccountID, float.Parse(cashDayRow.Debit)
                                , 0.0f, cashDayRow.Note
                            );
                            day2List.Add(tuple1);
                        }
                    }
                    daysBL.InsertDay2(day2List);
                }
            });
        }
        public override ErrorType CheckingFields()
        {
            if (this.cashDayTotal == 0.ToString())
            {
                MessageBox.Show("الدائن او الدين = 0");
                return ErrorType.CashDayTotalEmpty;
            }

            if (this.SelectedCashDayAccountID == 0)
            {
                MessageBox.Show("اختر صندوق");
                return ErrorType.CashDayAccountEmpty;
            }
            if (this.CashDayCollection.Count == 0)
            {
                MessageBox.Show("القيد فارغ");
                return ErrorType.CashDayCollectionEmpty;
            }
            foreach (CashDayRow dayRow in this.CashDayCollection)
            {
                if (String.IsNullOrEmpty(dayRow.AccountName) && dayRow.Total != 0)
                {
                    MessageBox.Show("اسم الحساب فارغ");
                    return ErrorType.CashDayCollectionEmpty;
                }
            }

            return ErrorType.NoError;
        }
    }
    public class CashDayRow : BindableBase
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
        public CashDayRow Copy()
        {
            CashDayRow dcashDayRow = new CashDayRow();
            dcashDayRow.accountCode = this.AccountCode;
            dcashDayRow.AccountName = this.AccountName;
            dcashDayRow.Debit = this.Debit;
            dcashDayRow.Credit = this.Credit;
            dcashDayRow.Note = this.Note;

            if (!(this.AccountsList is null))
            {
                foreach (var ele in this.AccountsList)
                    dcashDayRow.AccountsList.Add(ele);
            }

            return dcashDayRow;
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
