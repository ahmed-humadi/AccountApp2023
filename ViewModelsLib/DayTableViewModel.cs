using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Windows.Input;
using FrameWorkLib;
using ModelsLib;

namespace ViewModelsLib
{
    public class DayTableViewModel : FrameWorkLib.BindableBase
    {
       
        private DayTableModel dayTableModel;
        /// <summary>
        /// catch the return accounts from combox textchanged
        /// </summary>
        private List<Tuple<string, int, int>> accountsListCatch;
        /// <summary>
        /// this collection for data grid
        /// </summary>
        private readonly object dataGridCollectionLock = new object();

        private ObservableCollection<DayDataGridTableViewModel> dataGridCollection;
        public ObservableCollection<DayDataGridTableViewModel> DataGridCollection 
        {
            get
            {
                lock (dataGridCollectionLock)
                {
                    if (dataGridCollection is null)
                        return dataGridCollection = new ObservableCollection<DayDataGridTableViewModel>();
                    else
                        return dataGridCollection;
                }
            }
            set
            {
                lock (dataGridCollectionLock)
                {
                    SetProperty(ref dataGridCollection, value);
                }
            }
        }

        private string dayID;
        public string DayID 
        { 
            get => dayID;
            set
            {
                SetProperty<string>(ref dayID, value);
            }
        }

        private string dayDate;
        public string DayDate
        {
            get => dayDate;
            set
            {
                SetProperty(ref dayDate, value);
            }
        }

        private string dayNote = string.Empty;
        public string DayNote
        {
            get => dayNote;
            set
            {
                SetProperty(ref dayNote, value);
            }
        }

        private int dayType;
        public int DayType
        {
            get => dayType;
            set
            {
                SetProperty(ref dayType, value);
            }
        }
        private string day_Note;
        public string Day_Note
        {
            get => day_Note;
            set
            {
                SetProperty(ref day_Note, value);
            }
        }

        private decimal totalDebit = 0;
        public decimal TotalDebit
        {
            get => totalDebit;
            set
            {
                SetProperty(ref totalDebit, value);
            }
        }
        
        private decimal totalCredit = 0;
        public decimal TotalCredit
        {
            get => totalCredit;
            set
            {
                SetProperty(ref totalCredit, value);
            }
        }
        private bool iSEnable_Date = true;
        private bool iSEnable_DataGrid = true;
        private bool iSEnable_Note = true;
        private bool iSEnable_AddButton = true;
        public bool ISEnable_Date 
        { 
            get => iSEnable_Date;
            set
            {
                SetProperty(ref iSEnable_Date, value);
            }
        }
        public bool ISEnable_DataGrid
        {
            get => iSEnable_DataGrid;
            set
            {
                SetProperty(ref iSEnable_DataGrid, value);
            }
        }
        public bool ISEnable_Note
        {
            get => iSEnable_Note;
            set
            {
                SetProperty(ref iSEnable_Note, value);
            }
        }
        public bool ISEnable_AddButton
        {
            get => iSEnable_AddButton;
            set
            {
                SetProperty(ref iSEnable_AddButton, value);
            }
        }
        private bool isSaving = true;
        public bool IsSaving
        {
            get => isSaving;
            set
            {
                SetProperty(ref isSaving, value);
                ((FrameWorkLib.DelegateCommand)SaveDaysCommand).RaiseCanExecuteChanged();
            }
        }

        private bool isEditing = false;
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                SetProperty(ref isEditing, value);
                ((FrameWorkLib.DelegateCommand)ModDaysCommand).RaiseCanExecuteChanged();
            }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
            }
        }
        public ICommand NextDayCommand { get; set; }
        public ICommand PrevDayCommand { get; set; }
        public ICommand MaxDayCommand { get; set; }
        public ICommand MinDayCommand { get; set; }
        public ICommand DayViewLoadedCommand { get; set; }
        public ICommand CodeCellTextChangedCommand { get; set; }
        public ICommand NameCellTextChangedCommand { get; set; }
        public ICommand NameCellSelectionChangedCommand { get; set; }
        public ICommand NameCellDropClosedCommand { get; set; }
        public ICommand DayIDTextChangedCommand { get; set; }
        public ICommand DayNoteTextChangedCommand { get; set; }
        public ICommand DebitCellTextChangedCommand { get; set; }
        public ICommand CreditCellTextChangedCommand { get; set; }
        public ICommand SaveDaysCommand { get; set; }
        public ICommand NewDaysCommand { get; set; }
        public ICommand ModDaysCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        /// <summary>
        /// Consturctor
        /// </summary>
        public DayTableViewModel()
        {
            accountsListCatch  = new List<Tuple<string, int, int>>();
            dayTableModel = new DayTableModel();
            NextDayCommand = new FrameWorkLib.DelegateCommand(NextDay);
            PrevDayCommand = new FrameWorkLib.DelegateCommand(PrevDay);
            MaxDayCommand = new FrameWorkLib.DelegateCommand(MaxDay);
            MinDayCommand = new FrameWorkLib.DelegateCommand(MinDay);
            CodeCellTextChangedCommand = new FrameWorkLib.DelegateCommand(CodeCellTextChanged);
            DayViewLoadedCommand = new FrameWorkLib.DelegateCommand(DayViewLoaded);
            NameCellTextChangedCommand = new FrameWorkLib.DelegateCommand(NameCellTextChanged);
            DayIDTextChangedCommand = new FrameWorkLib.DelegateCommand(DayIDTextChanged);
            DayNoteTextChangedCommand = new FrameWorkLib.DelegateCommand(DayNoteTextChanged);
            NameCellSelectionChangedCommand = new FrameWorkLib.DelegateCommand(NameCellSelectionChanged);
            NameCellDropClosedCommand = new FrameWorkLib.DelegateCommand(NameCellDropClosed);
            DebitCellTextChangedCommand = new FrameWorkLib.DelegateCommand(DebitCellTextChanged);
            CreditCellTextChangedCommand = new FrameWorkLib.DelegateCommand(CreditCellTextChanged);
            SaveDaysCommand = new FrameWorkLib.DelegateCommand(SaveDays, () => IsSaving);
            NewDaysCommand = new FrameWorkLib.DelegateCommand(NewDays);
            ModDaysCommand = new FrameWorkLib.DelegateCommand(ModDays, () => IsEditing);
            AddRowCommand = new FrameWorkLib.DelegateCommand(AddRow);
            // here i add 20 empty rows in the dataGrid
            for (int i = 0; i < 50; i++)
            {
               this.DataGridCollection.Add(new DayDataGridTableViewModel());
            }
        }
        /// <summary>
        /// add new row to datagridCollection
        /// </summary>
        private void AddRow()
        {
            this.DataGridCollection.Add(new DayDataGridTableViewModel());
        }
        private async void SaveDays()
        {
            if (MessageBox.Show("هل تريد حفظ السند", "حفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (this.TotalCredit == 0 || this.TotalDebit == 0)
                    {
                        MessageBox.Show("الدائن او الدين = 0", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if ((this.TotalDebit - this.TotalCredit) != 0)
                    {
                        MessageBox.Show("القيد غير متزن", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //TODO :  check if all the day2 is empty 
                  

                    IsLoading = true; // show loading spiner
                    IsSaving = false; // it will not allow saving
                                      // first insert day1 and wait for it to finish 
                    await Task.Run(() =>
                    {

                        // here implement save method

                        // day1 data type
                        this.DayType = 0;  // سند قيد الدرس 29
                        this.Day_Note = this.DayID + "/" + "سند قيد";
                        Tuple<DateTime, string, int, string> parametersTupleDay1 = new Tuple<DateTime, string, int, string>(

                            DateTime.Parse(this.DayDate), this.DayNote, this.DayType, this.Day_Note);

                        dayTableModel.InsertDay1(parametersTupleDay1);

                        // day2 data type
                        AddDay2();
                    });
                    // wait 
                    await Task.Delay(1000);
                    IsSaving = true;
                    IsLoading = false;
                    MessageBox.Show("تم الحفظ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    IsSaving = true;
                    IsLoading = false;
                }
            }
        }
        private void AddDay2()
        {
            List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();
            foreach (DayDataGridTableViewModel dayTable in this.DataGridCollection)
            {
                if (!String.IsNullOrEmpty(dayTable.AccountID)) // skip the empty rows
                {
                    // do not save them
                    int parentID = int.Parse(this.dayID);
                    int Debit;
                    int Credit;
                    string note = string.Empty;
                    if (!int.TryParse(dayTable.Debit, out Debit))
                        Debit = 0;
                    if (!int.TryParse(dayTable.Credit, out Credit))
                        Credit = 0;
                    // الدرس 25
                    if (String.IsNullOrEmpty(dayTable.Note))
                        note = this.DayID + "/" + "سند قيد";
                    else
                        note = dayTable.Note;

                    Tuple<int, int, float, float, string> parametersTupleDay2 = new Tuple<int, int, float, float, string>
                        (
                            parentID,
                            int.Parse(dayTable.AccountID),
                            Debit,
                            Credit,
                            note
                        );
                    list.Add(parametersTupleDay2);
                }
            }
            dayTableModel.InsertDay2(list);
        }
        private async void ModDays()
        {
            try
            {
                if (MessageBox.Show("هل تريد تعديل السند", "حفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.TotalCredit == 0 || this.TotalDebit == 0)
                    {
                        MessageBox.Show("الدائن او الدين = 0", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if ((this.TotalDebit - this.TotalCredit) != 0)
                    {
                        MessageBox.Show("القيد غير متزن", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    IsLoading = true; // show loading spiner
                    IsEditing = false;

                    await Task.Run(() => {

                        // day1 data type
                        this.DayType = 0;  // سند قيد الدرس 29
                        this.Day_Note = this.DayID + "/" + "سند قيد";
                        int dayid = int.Parse(this.DayID);
                        Tuple<DateTime, string, int, string> parametersTupleDay1 = new Tuple<DateTime, string, int, string>(

                            DateTime.Parse(this.DayDate), this.DayNote, this.DayType, this.Day_Note);

                        dayTableModel.UpdateDay1(parametersTupleDay1, dayid);

                        // day2 
                        // first delete it
                        dayTableModel.DeleteDay2(dayid);
                        // add the new rows
                        AddDay2();

                    });
                    // wait 
                    await Task.Delay(1000);
                    IsLoading = false; //
                    IsEditing = true;
                    MessageBox.Show("تم االتعديل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsLoading = false; // 
                IsEditing = true;
            }
        }
        private void NewDays()
        {
            try
            {
                // get new day id
                DayID = dayTableModel.GetMax_1DayID().ToString();

                // enable 
                this.ISEnable_AddButton = true;
                this.ISEnable_DataGrid = true;
                this.ISEnable_Date = true;
                this.ISEnable_Note = true;
                this.DayDate = DateTime.Today.ToString();
                this.DayNote = string.Empty;
                this.TotalCredit = 0;
                this.TotalDebit = 0;
                this.IsSaving = true;
                this.IsEditing = false;
                this.DataGridCollection.Clear();
                for (int i = 0; i < 20; i++)
                    this.dataGridCollection.Add(new DayDataGridTableViewModel());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DayIDTextChanged(object parameter)
        {
            try
            {

                System.Windows.Controls.TextBox dayidTxtBx = parameter as System.Windows.Controls.TextBox;
                if (!dayidTxtBx.IsKeyboardFocusWithin)
                    return;
                if(!String.IsNullOrEmpty(dayID))
                {
                    List<Tuple<double, double, int, string, int, int, string>> day =
                        dayTableModel.GetDay2(int.Parse(dayID));
                    dataGridCollection.Clear();

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void MinDay()
        {
            try
            {
                this.DayID = dayTableModel.GetMinDayID().ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void MaxDay()
        {
            try
            {
                this.DayID = dayTableModel.GetMaxDayID().ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void DayViewLoaded()
        {
            try
            {
                this.DayID = dayTableModel.GetMax_1DayID().ToString();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        #region here the command to regulate the datagrid behavior 
        /*
         * 1- The NameCell is a combo box (cmbx), in order to make the combo box support 
         * search the database when the end-user starts typing I made the cmbx fire textchanged event
         * which inturn send a command to here and handled in (NameCellTextChanged) method
         * this method queries the database and fill the cmbx with the return query also 
         * catch the result in accountsListCatch list.
         * 2- The CodeCell is a Text box (txtbx), txtbx is searchable 
         * so when the end user type a number it will query the database and fill the 
         * Namecell cmbx with the return results
         * 3- NameCellDropClosed will clear the catch list and the cmbx list exxept of the selected 
         * value
         */
        /// <summary>
        /// when Code Cell changed it takes the value of the cell 
        /// and queries (using Like key work looking for the accounts contain the code enterd) 
        /// the data base the result catched in list
        /// and fill the Name Cell (combo box) with the result as account name
        /// as well as the Code Cell. if the return list contains exact code 
        /// it sets the combo box selected value with the account name matches the code enterd
        /// </summary>
        /// <param name="parameter"></param>
        private async void CodeCellTextChanged(object parameter)
        {
            try
            {
                System.Windows.Controls.DataGrid dataGrid = parameter as System.Windows.Controls.DataGrid;

                if (!(dataGrid.CurrentItem is null))
                {

                    if (!(dataGrid.CurrentColumn.GetCellContent(dataGrid?.CurrentItem) is null))
                    {
                        DayDataGridTableViewModel dayTable = dataGrid.CurrentItem as DayDataGridTableViewModel;

                        if (dayTable is null)
                            return;

                        if (String.IsNullOrEmpty(dayTable.AccountCode))
                        {
                            dayTable.SelectedValue = string.Empty;
                            dayTable.DataGridComBxCollection.Clear();
                            dayTable.AccountName = string.Empty;
                            dayTable.IsComboBoxDropOpen = false;
                            return;
                        }
                        IsLoading = true;
                        await Task.Run(() => {
                            accountsListCatch = dayTableModel.GetAccounts(int.Parse(dayTable.AccountCode));
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (accountsListCatch.Count == 0)
                                {
                                    dayTable.AccountName = string.Empty;
                                    dayTable.SelectedValue = string.Empty;
                                    dayTable.DataGridComBxCollection.Clear();
                                    dayTable.IsComboBoxDropOpen = false;
                                    return;
                                }
                                else
                                {
                                    // clear it first
                                    dayTable.DataGridComBxCollection.Clear();

                                    foreach (Tuple<string, int, int> tuple in accountsListCatch)
                                    {

                                        dayTable.DataGridComBxCollection.Add(tuple.Item1);

                                        if (tuple.Item2 == int.Parse(dayTable?.AccountCode))
                                        {
                                            dayTable.SelectedValue = tuple.Item1;
                                            dayTable.AccountID = tuple.Item3.ToString();
                                        }
                                    }
                                    dayTable.IsComboBoxDropOpen = true;
                                }
                            });
                        });
                        IsLoading = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// when Name Cell changed it takes the value of the cell 
        /// and queries (using Like key work looking for the accounts start with letter enterd) 
        /// the data base the result catched in list
        /// and fill the Code Cell with the result as account Code
        /// as well as the Name Cell. 
        /// this will fire twice when selection changed and text changed
        /// </summary>
        /// <param name="parameter"></param>
        private void NameCellTextChanged(object parameter)
        {
            try
            { 
                System.Windows.Controls.DataGrid dataGrid = parameter as System.Windows.Controls.DataGrid;

                if (!(dataGrid.CurrentItem is null))
                {

                    if (!(dataGrid.CurrentColumn.GetCellContent(dataGrid?.CurrentItem) is null))
                    {
                        DayDataGridTableViewModel dayTable = dataGrid.CurrentItem as DayDataGridTableViewModel;

                        if (dayTable is null)
                            return;

                        if (!String.IsNullOrEmpty(dayTable.AccountName) &&
                            !String.IsNullOrEmpty(dayTable.SelectedValue))
                            return; // here the user selected a value and the selectionchanged event will fire
                        // to handle the selection made

                        if (String.IsNullOrEmpty(dayTable.AccountName))
                        {
                            dayTable.AccountName = string.Empty;
                            dayTable.AccountCode = string.Empty;
                            dayTable.SelectedValue = string.Empty;
                            dayTable.DataGridComBxCollection.Clear();
                            dayTable.IsComboBoxDropOpen = false;
                            return;
                        }
                        accountsListCatch = dayTableModel.GetAccounts(dayTable.AccountName);

                        if (accountsListCatch.Count == 0)
                        {
                            dayTable.DataGridComBxCollection.Clear();
                            dayTable.IsComboBoxDropOpen = false;
                            return;
                        }
                        else
                        {
                            List<string> list = new List<string>();
                            dayTable.DataGridComBxCollection.Clear();
                            foreach (Tuple<string, int, int> tuple in accountsListCatch)
                            {
                                dayTable.DataGridComBxCollection.Add(tuple.Item1);
                            }
                            dayTable.IsComboBoxDropOpen = true;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void NameCellDropClosed(object parameter)
        {
            try
            {
                System.Windows.Controls.DataGrid dataGrid = parameter as System.Windows.Controls.DataGrid;

                if (!(dataGrid.CurrentItem is null))
                {

                    if (!(dataGrid.CurrentColumn.GetCellContent(dataGrid?.CurrentItem) is null))
                    {
                        DayDataGridTableViewModel dayTable = dataGrid.CurrentItem as DayDataGridTableViewModel;

                        if (dayTable is null)
                            return;

                        if (!String.IsNullOrEmpty(dayTable.SelectedValue)) // user selected a value
                        {

                            if (accountsListCatch.Count > 0)
                            {
                                foreach (Tuple<string, int, int> tuple in accountsListCatch)
                                {
                                    if (tuple.Item1.Equals(dayTable.SelectedValue))
                                        dayTable.AccountCode = tuple.Item2.ToString();
                                    else
                                    {
                                        dayTable.DataGridComBxCollection.Remove(tuple.Item1);
                                    }
                                }
                                accountsListCatch.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void NameCellSelectionChanged(object parameter)
        {
            try
            {
                System.Windows.Controls.DataGrid dataGrid = parameter as System.Windows.Controls.DataGrid;

                if (!(dataGrid.CurrentItem is null))
                {

                    if (!(dataGrid.CurrentColumn.GetCellContent(dataGrid?.CurrentItem) is null))
                    {
                        DayDataGridTableViewModel dayTable = dataGrid.CurrentItem as DayDataGridTableViewModel;

                        if (dayTable is null)
                            return;
                        if (!String.IsNullOrEmpty(dayTable.SelectedValue)) // user selected a value
                        {

                            if (accountsListCatch.Count > 0)
                            {
                                foreach (Tuple<string, int, int> tuple in accountsListCatch)
                                {
                                    if (tuple.Item1.Equals(dayTable.SelectedValue))
                                    {
                                        dayTable.AccountCode = tuple.Item2.ToString();
                                        dayTable.AccountID = tuple.Item3.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void DebitCellTextChanged(object parameter)
        {
            try
            {
                if (DataGridCollection.Count == 0)
                    return;
                TotalDebit = 0;
                foreach (DayDataGridTableViewModel dayTable in this.DataGridCollection)
                {
                    decimal d;
                    if (decimal.TryParse(dayTable.Debit, out d))
                    {
                        if (!String.IsNullOrEmpty(dayTable.Debit))
                            TotalDebit += d;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void CreditCellTextChanged(object parameter)
        {
            try
            {
                if (DataGridCollection.Count == 0)
                    return;
                TotalCredit = 0;
                foreach (DayDataGridTableViewModel dayTable in this.DataGridCollection)
                {
                    decimal c;
                    if (decimal.TryParse(dayTable.Credit, out c))
                    {
                        if (!String.IsNullOrEmpty(dayTable.Credit))
                            TotalCredit += c;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void DayNoteTextChanged(object parameter)
        {
            try
            {
                string dayNote = parameter as string;
                if (!String.IsNullOrEmpty(dayNote))
                {

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion
        private async void PrevDay(object parameter)
        {
         
            try
            {
                int min = dayTableModel.GetMinDayID();

                int dayid = int.Parse(DayID);

                dayid = dayid - 1;

                if (dayid < min)
                    return;

                //this.ISEnable_AddButton = false;
                //this.ISEnable_DataGrid = false;
                //this.ISEnable_Date = false;
                //this.ISEnable_Note = false;
                this.IsSaving = false;
                this.IsEditing = true;
                IsLoading = true;

                await Task.Run(() => {
                
                    // day 1
                    Tuple<int, DateTime, string, int> day1Table =  dayTableModel.GetDay1(dayid);

                    // day 2

                    List<Tuple<double, double, int, string, int, int, string>> day2List = dayTableModel.GetDay2(dayid);

                    System.Windows.Application.Current.Dispatcher.Invoke(() =>{
                    
                        // fill day view

                        this.DayID = dayid.ToString();

                        this.DayDate = day1Table.Item2.ToString();

                        this.DayNote = day1Table.Item3;

                        this.DayType = day1Table.Item4;

                        // day view
                        this.DataGridCollection.Clear();
                        this.TotalCredit = 0; this.TotalDebit = 0;
                        foreach (Tuple<double, double, int, string, int, int, string> tuple in day2List)
                        {
                            DayDataGridTableViewModel dayTable = new DayDataGridTableViewModel();
                            dayTable.AccountID = tuple.Item6.ToString();
                            dayTable.AccountCode = tuple.Item5.ToString();
                            dayTable.AccountName = tuple.Item4.ToString();
                            dayTable.Debit = tuple.Item1.ToString();
                            dayTable.Credit = tuple.Item2.ToString();
                            dayTable.Note = tuple.Item7.ToString();
                            this.TotalCredit += (decimal)tuple.Item2;
                            this.TotalDebit += (decimal)tuple.Item1;
                            this.DataGridCollection.Add(dayTable);
                        }
                    });
                });
                IsLoading = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private async void NextDay(object parameter)
        {
            try
            {
                int max = dayTableModel.GetMaxDayID();

                int dayid = int.Parse(DayID);

                dayid = dayid + 1;

                if (dayid > max)
                    return;

                //this.ISEnable_AddButton = false;
                //this.ISEnable_DataGrid = false;
                //this.ISEnable_Date = false;
                //this.ISEnable_Note = false;
                this.IsSaving = false;
                this.IsEditing = true;
                IsLoading = true;

                await Task.Run(() => {

                    // day 1
                    Tuple<int, DateTime, string, int> day1Table = dayTableModel.GetDay1(dayid);

                    // day 2

                    List<Tuple<double, double, int, string, int, int, string>> day2List = dayTableModel.GetDay2(dayid);

                    System.Windows.Application.Current.Dispatcher.Invoke(() => {

                        // fill day view

                        this.DayID = dayid.ToString();

                        this.DayDate = day1Table.Item2.ToString();

                        this.DayNote = day1Table.Item3;

                        this.DayType = day1Table.Item4;

                        // day view
                        this.DataGridCollection.Clear();
                        this.TotalCredit = 0; this.TotalDebit = 0;
                        foreach (Tuple<double, double, int, string, int, int, string> tuple in day2List)
                        {
                            DayDataGridTableViewModel dayTable = new DayDataGridTableViewModel();
                            dayTable.AccountID = tuple.Item6.ToString();
                            dayTable.AccountCode = tuple.Item5.ToString();
                            dayTable.AccountName = tuple.Item4.ToString();
                            dayTable.Debit = tuple.Item1.ToString();
                            dayTable.Credit = tuple.Item2.ToString();
                            dayTable.Note = tuple.Item7.ToString();
                            this.TotalCredit += (decimal)tuple.Item2;
                            this.TotalDebit += (decimal)tuple.Item1;
                            this.DataGridCollection.Add(dayTable);
                        }
                    });
                });
                IsLoading = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
    /// <summary>
    /// this class to halod data grid columns which is combination of account table and day2 table
    /// </summary>
    public class DayDataGridTableViewModel : FrameWorkLib.BindableBase
    {
        private readonly object dataGridCollectionLock = new object();
        private string accountCode = string.Empty;
        private string accountID = string.Empty;
        private string accountName = string.Empty;
        private string debit = string.Empty;
        private string credit = string.Empty;
        private string note = string.Empty;

        public string AccountCode
        {
            get => accountCode;
            set
            {
                SetProperty(ref accountCode, value);
            }
        }
        public string AccountID
        {
            get => accountID;
            set
            {
                SetProperty(ref accountID, value);
            }
        }
        public string AccountName
        { 
            get => accountName;
            set
            {
                SetProperty(ref accountName, value);
            }
        }
        public string Debit
        { 
            get => debit;
            set
            {
                SetProperty(ref debit, value);
            }
        }
        public string Credit
        {
            get => credit;
            set
            {
                SetProperty(ref credit, value);
            }
        }
        public string Note
        { 
            get => note;
            set 
            {
                SetProperty(ref note, value); 
            } 
        }

        private ObservableCollection<string> dataGridComBxCollection;
        public ObservableCollection<string> DataGridComBxCollection
        {
            get
            {
                lock (dataGridCollectionLock)
                {
                    if (dataGridComBxCollection is null)
                        return dataGridComBxCollection = new ObservableCollection<string>();
                    else
                        return dataGridComBxCollection;
                }
            }
            set
            {
                SetProperty(ref dataGridComBxCollection, value);
            }
        }

        private string selectedValue = string.Empty;

        public string SelectedValue
        {
            get => selectedValue;
            set
            {
                SetProperty(ref selectedValue, value);
            }
        }

        private bool isComboBoxDropOpen = false;
        public bool IsComboBoxDropOpen
        {
            get => isComboBoxDropOpen;
            set
            {
                SetProperty(ref isComboBoxDropOpen, value);
            }
        }
    }
}
