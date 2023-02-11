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
using System.Diagnostics;
namespace ViewModelsLib
{
    public class BillTableViewModel : ViewModelBase
    {
        private readonly object lockToken = new object();
        public int BillNumberCache { get; set; }

        private BillTableModel billTableModel;

        private ObservableCollection<BillRow> billCollection;
        public ObservableCollection<BillRow> BillCollection
        {
            get
            {
                lock (lockToken)
                {
                    if (billCollection is null)
                    {
                        return billCollection = new ObservableCollection<BillRow>();
                    }
                    return billCollection;
                }
            }
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref billCollection, value);
                    if (this.ViewMode == ViewModes.Editing)
                    {
                        this.IsAnyProprtyChanged = true;
                    }
                    else if (this.ViewMode == ViewModes.Viewing)
                    {
                        this.IsAnyProprtyChanged = false;
                    }
                }
            }
        }

        #region Bill Account Properties
        private string billDayNumber;
        public string BillDayNumber
        {
            get => billDayNumber;
            set
            {
                SetProperty(ref billDayNumber, value);
            }
        }
        private string billPaymentType;
        public string BillPaymentType
        {
            get => billPaymentType;
            set
            {
                SetProperty(ref billPaymentType, value);
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
        private string billDiscount = "0";
        public string BillDiscount
        {
            get => billDiscount;
            set
            {
                SetProperty(ref billDiscount, value);
            }
        }
        private string billNet = "0";
        public string BillNet
        {
            get => billNet;
            set
            {
                SetProperty(ref billNet, value);
            }
        }
        private string billAccountName;
        public string BillAccountName
        {
            get => billAccountName;
            set
            {
                SetProperty(ref billAccountName, value);
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
        private int selectedBillAccountID;
        public int SelectedBillAccountID
        {
            get => selectedBillAccountID;
            set
            {
                SetProperty(ref selectedBillAccountID, value);
            }
        }
        private List<Tuple<int, string>> billAccountList;
        public List<Tuple<int, string>> BillAccountList
        {
            get => billAccountList;
            set
            {
                SetProperty(ref billAccountList, value);
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
        private bool billAccountIsDropOpen;
        public bool BillAccountIsDropOpen
        {
            get => billAccountIsDropOpen;
            set
            {
                SetProperty(ref billAccountIsDropOpen, value);
            }
        }
        #endregion
        #region Bill Store Properties

        private string billTotal = "0";
        public string BillTotal
        {
            get => billTotal;
            set
            {
                SetProperty(ref billTotal, value);
            }
        }

        private string billStoreName;
        public string BillStoreName
        {
            get => billStoreName;
            set
            {
                SetProperty(ref billStoreName, value);
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
        private int selectedBillStoreID;
        public int SelectedBillStoreID
        {
            get => selectedBillStoreID;
            set
            {
                SetProperty(ref selectedBillStoreID, value);
            }
        }
        private List<Tuple<int, string>> billStoreList;
        public List<Tuple<int, string>> BillStoreList
        {
            get => billStoreList;
            set
            {
                SetProperty(ref billStoreList, value);
            }
        }
        private bool isLoading_Store;
        public bool IsLoading_Store
        {
            get => isLoading_Store;
            set
            {
                SetProperty(ref isLoading_Store, value);
            }
        }
        private bool billStoreIsDropOpen;
        public bool BillStoreIsDropOpen
        {
            get => billStoreIsDropOpen;
            set
            {
                SetProperty(ref billStoreIsDropOpen, value);
            }
        }
        #endregion

        // this wiil be set from the constructor
        private int billTypeID;
        public int BillTypeID
        {
            get => billTypeID;
            set
            {
                SetProperty(ref billTypeID, value);
            }
        }
        private int billTypeNumber;
        public int BillTypeNumber
        {
            get => billTypeNumber;
            set
            {
                SetProperty(ref billTypeNumber, value);
            }
        }

        private string billTypeName;
        public string BillTypeName
        {
            get => billTypeName;
            set
            {
                SetProperty(ref billTypeName, value);
            }
        }
        private int billID;
        public int BillID
        {
            get => billID;
            set
            {
                SetProperty(ref billID, value);
            }
        }
        private string billNumber;
        public string BillNumber
        {
            get => billNumber;
            set
            {
                SetProperty(ref billNumber, value);
            }
        }
        private string billDate;
        public string BillDate
        {
            get => billDate;
            set
            {
                SetProperty(ref billDate, value);
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

        private string billNote = string.Empty;
        public string BillNote
        {
            get => billNote;
            set
            {
                SetProperty(ref billNote, value);
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

        private int billRowIndex;
        public int BillRowIndex
        {
            get => billRowIndex;
            set
            {
                SetProperty(ref billRowIndex, value);
            }
        }
        private bool isLoadingBarCodeCmBx = false;
        public bool IsLoadingBarCodeCmBx
        {
            get => isLoadingBarCodeCmBx;
            set
            {
                SetProperty(ref isLoadingBarCodeCmBx, value);
            }
        }

        private bool isSelectedItemGroupNameChanged = false;
        public bool IsSelectedItemGroupNameChanged
        {
            get => isSelectedItemGroupNameChanged;
            set
            {
                SetProperty(ref isSelectedItemGroupNameChanged, value);
            }
        }
        #region ICommand
        public ICommand AddRowCommand { get; set; }
        public ICommand RemoveRowCommand { get; set; }
        public ICommand RepeatRowCommand { get; set; }
        public ICommand BillNumberTextChnagedCommand { get; set; }
        public ICommand SearchItemNameCommand { get; set; }
        public ICommand ItemNameTextChangedCommand { get; set; }
        public ICommand ItemNameSelectionChangedChangedCommand { get; set; }
        public ICommand ItemBarCodeTextChangedCommand { get; set; }
        public ICommand ItemBarCodeSearchCommand { get; set; }
        public ICommand BillRowTotalCellTextChangedCommand { get; set; }
        public ICommand DiscountTotalTextChanedCommand { get; set; }
        public ICommand ShowAssociatedDayCommand { get; set; }
        #region Account TextBox Command
        public ICommand AccountNameTextChangedCommand { get; set; }
        public ICommand AccountNameSelectionChnagedCommand { get; set; }
        public ICommand AccountNameSearchCommand { get; set; }
        #endregion
        #region Store TextBox Command
        public ICommand StoreNameTextChangedCommand { get; set; }
        public ICommand StoreNameSelectionChnagedCommand { get; set; }
        public ICommand StoreNameSearchCommand { get; set; }
        #endregion
        #endregion

        public event EventHandler AskForShowDayView;
        protected void RaiseAskForShowDayView(int dayNumer)
        {
          AskForShowDayView?.Invoke(this, new ViewModelEventArgs.ViewModelEventArgs() { DayNumber = dayNumer});
        }
        public BillTableViewModel()
        {
            //this.BillType = BillType;
            billTableModel = new BillTableModel();
            AddRowCommand = new DelegateCommand(AddRow);
            RemoveRowCommand = new DelegateCommand(RemoveRow);
            RepeatRowCommand = new DelegateCommand(RepeatRow);
            SearchItemNameCommand = new DelegateCommand(SearchItemName);
            ItemNameTextChangedCommand = new DelegateCommand(ItemNameTextChanged);
            ItemNameSelectionChangedChangedCommand = new DelegateCommand(ItemNameSelectionChangedChanged);
            ItemBarCodeSearchCommand = new DelegateCommand(ItemBarCodeSearch);

            ItemBarCodeTextChangedCommand = new DelegateCommand(ItemBarCodeTextChanged);

            BillNumberTextChnagedCommand = new DelegateCommand(BillNumberTextChnaged);

            AccountNameTextChangedCommand = new DelegateCommand(AccountNameTextChanged);
            AccountNameSelectionChnagedCommand = new DelegateCommand(AccountNameSelectionChnaged);
            AccountNameSearchCommand = new DelegateCommand(AccountNameSearch);

            StoreNameTextChangedCommand = new DelegateCommand(StoreNameTextChanged);
            StoreNameSelectionChnagedCommand = new DelegateCommand(StoreNameSelectionChnaged);
            StoreNameSearchCommand = new DelegateCommand(StoreNameSearch);

            BillRowTotalCellTextChangedCommand = new DelegateCommand(BillRowTotalCellTextChanged);

            DiscountTotalTextChanedCommand = new DelegateCommand(DiscountTotalTextChaned);

            ShowAssociatedDayCommand = new DelegateCommand(ShowAssociatedDay);

            this.BillDate = DateTime.Today.ToShortDateString();
        }
        private void ItemBarCodeTextChanged()
        {
            ItemBarCodeSearchCommand.Execute(null);
        }
        private void ShowAssociatedDay()
        {
            try
            {
                int dayNubmer;
                if (!int.TryParse(this.BillDayNumber, out dayNubmer))
                {
                    MessageBox.Show("لايوجد سند للفاتورة");
                    return;
                }
                RaiseAskForShowDayView(dayNubmer);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void DeleteView()
        {
            try
            {
                if (MessageBox.Show("هل تريد حذف الفاتورة", "حذف", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if(this.BillID == 0)
                    {
                        MessageBox.Show("الفاتورة غير موجودة");
                        return;
                    }
                    billTableModel.DeleteBill1(this.BillID);
                    billTableModel.DeleteBill2(this.BillID);
                    // when the bill is modified we delete the associated day this will automaticlly delete day2 in database triggers
                    int DayType = this.BillID;
                    billTableModel.DeleteDay1(DayType);

                    // create new bill

                    NewView();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DiscountTotalTextChaned()
        {
            try
            {
                this.BillNet = (decimal.Parse(this.BillTotal) - decimal.Parse(this.BillDiscount)).ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BillRowTotalCellTextChanged()
        {
            decimal tot = 0;
            try
            {
                if (this.BillCollection.Count > 0)
                {
                    foreach (BillRow row in this.BillCollection)
                    {
                        tot += row.Total;
                        this.BillTotal = tot.ToString();
                    }
                }
                else
                {
                    this.BillTotal = "0";
                }
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
                if(this.BillNumberCache == 0)
                {
                    base.ViewMode = ViewModes.Editing;

                    this.BillNumber = this.billTableModel.GetMax1BillNumber(this.BillTypeID).ToString();

                    InitializingFields();

                    base.ToggleCommandBtns();
                }
                else
                {
                   // this will not trigger the textchanged event
                    this.BillNumber = this.BillNumberCache.ToString();
                    this.BillNumberCache = 0;
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
                this.BillNumberCache = int.Parse(this.BillNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void BillNumberTextChnaged()
        {
           try
           {
                if (string.IsNullOrEmpty(this.BillNumber) || string.IsNullOrWhiteSpace(this.BillNumber))
                    return;

                    if (this.BillCollection.Count > 0)
                    this.BillCollection.Clear();
                // do not query the data base since the user is just want to create new bill
                // and this event trigged because of the billnumber being set in the creatnewbill method
               
                if (this.ViewMode == ViewModes.Viewing)
                {
                    //IsEditting = true;
                    //IsDeleting = true;
                    //IsSaving = false;
                    base.ToggleCommandBtns();
                    this.IsLoading = true;
                    await Task.Run(() =>
                     {
                         System.Windows.Application.Current.Dispatcher.Invoke(() =>
                         {
                            // bill1
                            Tuple<int, string, string, double, string, int, string, Tuple<int, string>> bill1 =
                                 billTableModel.GetBill1(int.Parse(billNumber), this.BillTypeID);
                             if (bill1 is null)
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
                             this.BillID = bill1.Item1;
                             this.BillNote = bill1.Item2;
                             this.BillDate = bill1.Item3;
                             this.BillDiscount = bill1.Item4.ToString();
                             this.BillPaymentType = bill1.Item5;
                             this.SelectedBillAccountID = bill1.Item6;
                             this.BillAccountName = bill1.Item7;
                             this.SelectedBillStoreID = bill1.Rest.Item1;
                             this.BillStoreName = bill1.Rest.Item2;
                            //this.BillDate = bill1
                            // bill 2
                            int bill2ParentID = this.BillID;
                             List<Tuple<int, double, double, string, int, string, string, Tuple<double>>> bill2Rows =
                                 billTableModel.GetBill2(bill2ParentID);
                             if (bill2Rows.Count == 0)
                             {
                                 this.BillTotal = "0";
                                 return;
                             }
                             foreach (var tuple in bill2Rows)
                             {
                                 BillRow billRow = new BillRow();
                                 int rowID = tuple.Item1;
                                 billRow.Quantity = Convert.ToString(tuple.Item2);
                                 billRow.Price = Convert.ToString(tuple.Item3);
                                 billRow.Note = tuple.Item4;
                                 billRow.SelectedItemID = tuple.Item5;
                                 billRow.ItemName = tuple.Item6;
                                 billRow.BarCode = tuple.Item7;
                                 billRow.Total = (decimal)tuple.Rest.Item1;
                                 this.BillCollection.Add(billRow);
                             }
                        // to update the total
                        BillRowTotalCellTextChanged();
                        // and get the associated day number
                        int dayType = this.BillID;
                             this.BillDayNumber = billTableModel.GetDayNumber(dayType).ToString();
                         });
                     });
                    await Task.Delay(500);
                    this.IsLoading = false;

                }
                // and get the associated day number
                //await GetDayNumberTask();
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
                        SaveBillAsync();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                else
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;
                    this.BillNumber = billTableModel.GetMinBillNumber(this.BillTypeID).ToString();
                    
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
                        SaveBillAsync();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                else
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;
                    this.BillNumber = billTableModel.GetMaxBillNumber(this.BillTypeID).ToString();
                    //EnableEditing = false;

                    base.ToggleCommandBtns();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void PreviousView()
        {
            try
            {
                if (base.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveBillAsync();
                    else
                        base.IsAnyProprtyChanged = false;
                }
                else if (!base.IsAnyProprtyChanged)
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;

                    int minBill = billTableModel.GetMinBillNumber(this.BillTypeID);

                    int billNumber = int.Parse(this.BillNumber) - 1;

                    if (billNumber < minBill)
                        return;
                    

                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.BillNumber = billNumber.ToString();

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
                if (this.IsAnyProprtyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        SaveBillAsync();
                    else
                        this.IsAnyProprtyChanged = false;
                }
                if(!this.IsAnyProprtyChanged)
                {
                    // before the bill number changes the view mode should set first
                    this.ViewMode = ViewModes.Viewing;

                    int maxBill = billTableModel.GetMaxBillNumber(this.BillTypeID);

                    int billNumber = int.Parse(this.BillNumber) + 1;

                    if (billNumber > maxBill)
                        return;
                    // this will cuase the BillNumberTextChnaged to be fired
                    // so we do not need to get the bill1 and 2 here 
                    this.BillNumber = billNumber.ToString();

                    
                    //EnableEditing = false;

                    base.ToggleCommandBtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void StoreNameSearch()
        {
            try
            {
                this.IsLoading_Store = true;

                this.BillStoreIsDropOpen = true;

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BillStoreList = this.billTableModel.GetStores();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Store = false;
            }
        }
        private void StoreNameSelectionChnaged()
        {
            try
            {
                this.BillStoreList = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void StoreNameTextChanged()
        {
            try
            {
                string searchname = this.BillStoreName;

                this.IsLoading_Store = true;

                this.BillStoreIsDropOpen = true;

                if (String.IsNullOrEmpty(searchname))
                {
                    this.BillStoreIsDropOpen = false;
                    this.SelectedBillStoreID = 0;
                    this.BillStoreList = null;
                    return;
                }

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BillStoreList = billTableModel.GetStores(searchname);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Store = false;
            }
        }
        private void AccountNameSelectionChnaged()
        {
            try
            {
               this.BillAccountList = null;
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
                string searchname = this.BillAccountName;

                this.IsLoading_Account = true;

                this.BillAccountIsDropOpen = true;


                if (String.IsNullOrEmpty(searchname))
                {
                    this.BillAccountIsDropOpen = false;
                    this.SelectedBillAccountID = 0;
                    this.BillAccountList = null;
                    return;
                }

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BillAccountList = billTableModel.GetAccounts(searchname);

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

                this.BillAccountIsDropOpen = true;

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BillAccountList = this.billTableModel.GetAccounts();
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
        private void ItemBarCodeSearch()
        {
            try
            {
                int rowIndex = this.BillRowIndex;
                // get the row that reqused the item table
                BillRow billRow = this.BillCollection[rowIndex];
                string barCode = billRow.BarCode;
                Tuple<int, string, string> tuple = billTableModel.GetItem(barCode);
                if (tuple is null)
                    return;
                billRow.ItemName = tuple.Item3;
                billRow.SelectedItemID = tuple.Item1;
                this.AddRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// this just to clear the the list after selection is made
        /// since it did not work from the MycomboBox class
        /// </summary>
        int i = 0; // becuse this event occurs two times
        private void ItemNameSelectionChangedChanged()
        {
            try
            {
                i++;
                int rowIndex = this.BillRowIndex;
                // get the row that reqused the item table
                BillRow billRow = this.BillCollection[rowIndex];

                billRow.BarCode = billRow.SelectedItemBarCode;
                IsSelectedItemGroupNameChanged = true;

                if (i == 2)
                {
                    this.AddRow();
                    i = 0;
                    IsSelectedItemGroupNameChanged = false;

                }
                billRow.ItemsList = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private async void ItemNameTextChanged()
        {

            int rowIndex = this.BillRowIndex;
            // get the row that reqused the item table
            BillRow billRow = this.BillCollection[rowIndex];

            // it means still loading from prev task
            if (this.IsLoading || billRow.IsLoadingItemCmBx)
                return;

            if (IsSelectedItemGroupNameChanged)
            {
                IsSelectedItemGroupNameChanged = false;
                return;
            }

            billRow.IsItemNameDropOpened = true;
            billRow.IsLoadingItemCmBx = true;

            string textEntered = billRow.ItemName;

            if (String.IsNullOrEmpty(textEntered))
            {
                billRow.IsItemNameDropOpened = false;
                billRow.IsLoadingItemCmBx = false;
                billRow.ItemsList = null;
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        billRow.ItemsList = billTableModel.GetItems(textEntered);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                billRow.IsLoadingItemCmBx = false;
            }
        }
        private async void SearchItemName()
        {
            int rowIndex = this.BillRowIndex;
            // get the row that reqused the item table
            BillRow billRow = this.BillCollection[rowIndex];
            billRow.IsItemNameDropOpened = true;
            billRow.IsLoadingItemCmBx = true;
            try
            {
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        billRow.ItemsList = billTableModel.GetItems();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                billRow.IsLoadingItemCmBx = false;
            }
        }
        protected override void SaveView()
        {

            if (MessageBox.Show("هل تريد حفظ الفاتورة", "حفظ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
               SaveBillAsync();
            }
        }
        public async void SaveBillAsync()
        {
            try
            {                    
                this.IsLoading = true;
                if (!(CheckingFields() == ErrorType.NoError))
                    return;

                if (!IsUpdate)
                {
                  
                    await Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            // bill1
                            Tuple<int, string, int, int, string, int, double, Tuple<string>> tuple = new Tuple<int, string, int, int, string, int, double, Tuple<string>>
                            (
                                this.BillTypeID, this.BillNote, this.SelectedBillAccountID,
                                this.selectedBillStoreID, this.BillDate, int.Parse(this.BillNumber),
                                double.Parse(this.BillDiscount),new Tuple<string>(this.BillPaymentType)
                            );
                            billTableModel.InsertBill1(tuple);
                            // after we save bill1 take number and get the id
                            this.BillID = billTableModel.GetBill1ID(int.Parse(this.BillNumber), this.BillTypeID);
                            // bill2
                            AddBill2();
                        });
                    });

                    // after the add bills tasks continou with add days for this bill
                    int dayNumer = billTableModel.GetMax_1DayNumber();
                    // اذا كانت مشتريات
                    if (this.BillTypeNumber == 0)
                        await AddPurchaseDayTask(dayNumer);

                    // اذا كانت مردود مشتريات
                    else if (this.BillTypeNumber == 1)
                        await AddReturnPurchaseDayTask(dayNumer);

                    // اذا كانت مبيعات
                    else if (this.BillTypeNumber == 2)
                        await AddSaleDayTask(dayNumer);
                    // اذا كانت مردود مبيعات
                    else if (this.BillTypeNumber == 3)
                        await AddReturnSaleDayTask(dayNumer);

                    await GetDayNumberTask();

                    this.IsLoading = false;
                    MessageBox.Show("Saved");

                }
                else if(IsUpdate) // modify bill
                {
                    // bill1
                    await Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            Tuple<int, string, int, int, string, int, double, Tuple<string>> tuple =
                            new Tuple<int, string, int, int, string, int, double, Tuple<string>>
                                (
                                    this.BillTypeID, this.BillNote,
                                    this.SelectedBillAccountID,
                                    this.SelectedBillStoreID,
                                    this.BillDate,
                                    int.Parse(this.BillNumber),
                                    double.Parse(this.BillDiscount),
                                    new Tuple<string>(this.BillPaymentType)
                                );
                            billTableModel.UpdateBill1(tuple, this.BillID);

                            // bill 2

                            this.billTableModel.DeleteBill2(this.BillID);

                            AddBill2();

                            // when the bill is modified we delete the associated day this will automaticlly delete day2 in database triggers
                            int DayType = this.BillID;
                            billTableModel.DeleteDay1(DayType);
                        });
                    });
                    // then add it again
                    int dayNumer = int.Parse(this.BillDayNumber);
                    // اذا كانت مشتريات
                    if (this.BillTypeNumber == 0)
                        await AddPurchaseDayTask(dayNumer);

                    // اذا كانت مردود مشتريات
                    else if (this.BillTypeNumber == 1)
                        await AddReturnPurchaseDayTask(dayNumer);

                    // اذا كانت مبيعات
                    else if (this.BillTypeNumber == 2)
                        await AddSaleDayTask(dayNumer);
                    // اذا كانت مردود مبيعات
                    else if (this.BillTypeNumber == 3)
                        await AddReturnSaleDayTask(dayNumer);

                    this.IsLoading = false;
                    MessageBox.Show("تم حفظ التعديلات");
                }
                IsAnyProprtyChanged = false;
                this.ViewMode = ViewModes.Viewing;
                base.ToggleCommandBtns();

                /*EnableEditing = false;
                IsSaving = false;
                IsEditting = true;
                IsDeleting = true;
                IsUpdate = false;*/
                //Keyboard.ClearFocus();
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
        // نفس مردود المشتريات بس الحسابات تختلف
        private Task AddSaleDayTask(int dayNumer)
        {
            return Task.Run(() =>
            {
                int DayNumber = dayNumer;
                // day1 data type
                int DayType = this.BillID;
                string Day_Note = this.BillNumber + "/" + this.BillTypeName;
                Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                (
                    DateTime.Parse(this.BillDate), Day_Note, DayType, Day_Note, DayNumber
                );

                billTableModel.InsertDay1(parametersTupleDay1);

                // day2 data type

                int day1ID = billTableModel.GetDay1ID(DayNumber);


                List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();

                // اثبات مبيعات
                // add Debit الى حساب المبيعات  AccountID = 36
                int parentID = day1ID;
                float Debit = 0;
                float Credit = float.Parse(this.BillTotal);
                int accountID = 36;

                Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                (
                        parentID, accountID, Debit, Credit, Day_Note
                );



                if (this.BillPaymentType.Contains("نقدي"))
                {
                    // add Credit 26 من حساب الصندوق 
                    accountID = 26;
                }
                else if (this.BillPaymentType.Contains("اجل"))
                {
                    // add Credit من حساب المورد 

                    accountID = this.SelectedBillAccountID;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(accountID));
                }
                Debit = float.Parse(this.BillTotal);
                Credit = 0;
                Tuple<int, int, float, float, string> tuple2 = new Tuple<int, int, float, float, string>
                 (
                        parentID, accountID, Debit, Credit, Day_Note
                );
                list.Add(tuple2);
                list.Add(tuple1);


                // اثبات حسم المبيعات

                if (double.Parse(this.BillDiscount) > 0)
                {
                    Day_Note = this.BillNumber + "/" + this.BillTypeName + "/" + "حسم";
                    // add Debit من حساب حسم ممنوع  AccountID = 35
                    parentID = day1ID;
                    Debit = float.Parse(this.BillDiscount);
                    Credit = 0;
                    accountID = 35;

                    Tuple<int, int, float, float, string> tuple3 = new Tuple<int, int, float, float, string>
                    (
                            parentID, accountID, Debit, Credit, Day_Note
                    );


                    if (this.BillPaymentType.Equals("نقدي"))
                    {
                        // add Credit 26 الى حساب الصندوق 
                        accountID = 26;
                    }
                    else if (this.BillPaymentType.Equals("اجل"))
                    {
                        // add Credit الى حساب المورد 

                        accountID = this.SelectedBillAccountID;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(accountID));
                    }
                    Debit = 0;
                    Credit = float.Parse(this.BillDiscount);
                    Tuple<int, int, float, float, string> tuple4 = new Tuple<int, int, float, float, string>
                     (
                            parentID, accountID, Debit, Credit, Day_Note
                    );

                    list.Add(tuple4);
                    list.Add(tuple3);
                }
                billTableModel.InsertDay2(list);
            });
        }
        private Task AddReturnSaleDayTask(int dayNumer)
        {
            return Task.Run(() =>
            {
                int DayNumber = dayNumer;
                // day1 data type
                int DayType = this.BillID;
                string Day_Note = this.BillNumber + "/" + this.BillTypeName;
                Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                (
                    DateTime.Parse(this.BillDate), Day_Note, DayType, Day_Note, DayNumber
                );

                billTableModel.InsertDay1(parametersTupleDay1);

                // day2 data type

                int day1ID = billTableModel.GetDay1ID(DayNumber);


                List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();

                // اثبات مردود مبيعات
                // add Debit من حساب المبيعات  AccountID = 36
                int parentID = day1ID;
                float Debit = 0;
                float Credit = float.Parse(this.BillTotal);
                int accountID = 36;

                Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                (
                        parentID, accountID, Debit, Credit, Day_Note
                );



                if (this.BillPaymentType.Contains("نقدي"))
                {
                    // add Credit 26 الى حساب الصندوق 
                    accountID = 26;
                }
                else if (this.BillPaymentType.Contains("اجل"))
                {
                    // add Credit الى حساب المورد 

                    accountID = this.SelectedBillAccountID;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(accountID));
                }
                Debit = float.Parse(this.BillTotal);
                Credit = 0;
                Tuple<int, int, float, float, string> tuple2 = new Tuple<int, int, float, float, string>
                 (
                        parentID, accountID, Debit, Credit, Day_Note
                );
                list.Add(tuple2);
                list.Add(tuple1);


                // اثبات حسم مردود المبيعات

                if (double.Parse(this.BillDiscount) > 0)
                {
                    Day_Note = this.BillNumber + "/" + this.BillTypeName + "/" + "حسم";
                    // add Debit الى حساب حسم ممنوع  AccountID = 35
                    parentID = day1ID;
                    Debit = 0;
                    Credit = float.Parse(this.BillDiscount);
                    accountID = 35;

                    Tuple<int, int, float, float, string> tuple3 = new Tuple<int, int, float, float, string>
                    (
                            parentID, accountID, Debit, Credit, Day_Note
                    );


                    if (this.BillPaymentType.Equals("نقدي"))
                    {
                        // add Credit 26 من حساب الصندوق 
                        accountID = 26;
                    }
                    else if (this.BillPaymentType.Equals("اجل"))
                    {
                        // add Credit من حساب المورد 

                        accountID = this.SelectedBillAccountID;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(accountID));
                    }
                    Debit = float.Parse(this.BillDiscount);
                    Credit = 0;
                    Tuple<int, int, float, float, string> tuple4 = new Tuple<int, int, float, float, string>
                     (
                            parentID, accountID, Debit, Credit, Day_Note
                    );

                    list.Add(tuple4);
                    list.Add(tuple3);
                }
                billTableModel.InsertDay2(list);
            });
        }
        private Task AddReturnPurchaseDayTask(int dayNumer)
        {
            return Task.Run(() =>
            {
                int DayNumber = dayNumer;
                // day1 data type
                int DayType = this.BillID;
                string Day_Note = this.BillNumber + "/" + this.BillTypeName;
                Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                (
                    DateTime.Parse(this.BillDate), Day_Note, DayType, Day_Note, DayNumber
                );

                billTableModel.InsertDay1(parametersTupleDay1);

                // day2 data type

                int day1ID = billTableModel.GetDay1ID(DayNumber);


                List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();

                // اثبات مرتجع المشتريات
                // add Debit الى حساب المشتريات  AccountID = 31
                int parentID = day1ID;
                float Debit = 0;
                float Credit = float.Parse(this.BillTotal);
                int accountID = 31;

                Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                (
                        parentID, accountID, Debit, Credit, Day_Note
                );

                

                if (this.BillPaymentType.Contains("نقدي"))
                {
                    // add Credit 26 من حساب الصندوق 
                    accountID = 26;
                }
                else if (this.BillPaymentType.Contains("اجل"))
                {
                    // add Credit من حساب المورد 

                    accountID = this.SelectedBillAccountID;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(accountID));
                }
                Debit = float.Parse(this.BillTotal);
                Credit = 0;
                Tuple<int, int, float, float, string> tuple2 = new Tuple<int, int, float, float, string>
                 (
                        parentID, accountID, Debit, Credit, Day_Note
                );
                list.Add(tuple2);
                list.Add(tuple1);


                // اثبات حسم مرتجع مشتريات

                if (double.Parse(this.BillDiscount) > 0)
                {
                    Day_Note = this.BillNumber + "/" + this.BillTypeName + "/" + "حسم";
                    // add Debit من حساب حسم مكتسب  AccountID = 34
                    parentID = day1ID;
                    Debit = float.Parse(this.BillDiscount);
                    Credit = 0;
                    accountID = 34;

                    Tuple<int, int, float, float, string> tuple3 = new Tuple<int, int, float, float, string>
                    (
                            parentID, accountID, Debit, Credit, Day_Note
                    );


                    if (this.BillPaymentType.Equals("نقدي"))
                    {
                        // add Credit 26 الى حساب الصندوق 
                        accountID = 26;
                    }
                    else if (this.BillPaymentType.Equals("اجل"))
                    {
                        // add Credit الى حساب المورد 

                        accountID = this.SelectedBillAccountID;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(accountID));
                    }
                    Debit = 0;
                    Credit = float.Parse(this.BillDiscount);
                    Tuple<int, int, float, float, string> tuple4 = new Tuple<int, int, float, float, string>
                     (
                            parentID, accountID, Debit, Credit, Day_Note
                    );

                    list.Add(tuple4);
                    list.Add(tuple3);
                }
                billTableModel.InsertDay2(list);
            });
        }
        private Task AddPurchaseDayTask(int dayNumer)
        {
            return Task.Run(() =>
            {

                int DayNumber = dayNumer;
                // day1 data type
                int DayType = this.BillID;
                string Day_Note = this.BillNumber + "/" + this.BillTypeName;
                Tuple<DateTime, string, int, string, int> parametersTupleDay1 = new Tuple<DateTime, string, int, string, int>
                (
                    DateTime.Parse(this.BillDate), Day_Note, DayType, Day_Note, DayNumber
                );

                billTableModel.InsertDay1(parametersTupleDay1);

                // day2 data type

                int day1ID = billTableModel.GetDay1ID(DayNumber);


                List<Tuple<int, int, float, float, string>> list = new List<Tuple<int, int, float, float, string>>();

                // اثبات المشتريات
                // add Debit من حساب المشتريات  AccountID = 33
                int parentID = day1ID;
                float Debit = float.Parse(this.BillTotal);
                float Credit = 0;
                int accountID = 33;

                Tuple<int, int, float, float, string> tuple1 = new Tuple<int, int, float, float, string>
                (
                        parentID, accountID, Debit, Credit, Day_Note
                );

             

                if(this.BillPaymentType.Contains("نقدي"))
                {
                    // add Credit 26 الى حساب الصندوق 
                    accountID = 26;
                }
                else if (this.BillPaymentType.Contains("اجل"))
                {
                    // add Credit الى حساب المورد 

                    accountID = this.SelectedBillAccountID;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(accountID));
                }
                Debit = 0;
                Credit = float.Parse(this.BillTotal);
                Tuple<int, int, float, float, string> tuple2 = new Tuple<int, int, float, float, string>
                 (
                        parentID, accountID, Debit, Credit, Day_Note
                );
                list.Add(tuple1);
                list.Add(tuple2);


                // اثبات حسم مشتريات

                if(double.Parse(this.BillDiscount) > 0)
                {
                    Day_Note = this.BillNumber + "/" + this.BillTypeName + "/" + "حسم";
                    // add Debit الى حساب حسم مكتسب  AccountID = 34
                    parentID = day1ID;
                    Debit = 0;
                    Credit = float.Parse(this.BillDiscount);
                    accountID = 34;

                    Tuple<int, int, float, float, string> tuple3 = new Tuple<int, int, float, float, string>
                    (
                            parentID, accountID, Debit, Credit, Day_Note
                    );


                    if (this.BillPaymentType.Equals("نقدي"))
                    {
                        // add Credit 26 من حساب الصندوق 
                        accountID = 26;
                    }
                    else if (this.BillPaymentType.Equals("اجل"))
                    {
                        // add Credit من حساب المورد 

                        accountID = this.SelectedBillAccountID;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(accountID));
                    }
                    Debit = float.Parse(this.BillDiscount);
                    Credit = 0;
                    Tuple<int, int, float, float, string> tuple4 = new Tuple<int, int, float, float, string>
                     (
                            parentID, accountID, Debit, Credit, Day_Note
                    );

                    list.Add(tuple4);
                    list.Add(tuple3);
                }
                billTableModel.InsertDay2(list);

            });
        }
        private Task GetDayNumberTask()
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    // and get the associated day number
                    int dayType = this.BillID;
                    this.BillDayNumber = billTableModel.GetDayNumber(dayType).ToString();
                });
            });
        }
        private void AddBill2()
        {

            List<Tuple<int, int, decimal, decimal, string>> parametersList = new List<Tuple<int, int, decimal, decimal, string>>();

            foreach (BillRow billRow in this.BillCollection)
            {
              
                if (!String.IsNullOrEmpty(billRow.ItemName) && billRow.Total != 0)
                {
                    // first get the item to know in which store it is, if the storeID is 0 that item has no store
                    ItemTable itemTable = this.billTableModel.GetItem(billRow.SelectedItemID);
                    if (itemTable.Count > 0)
                    {
                        ItemRow itemRow = itemTable[0];
                        // if the storeID is 0 item should be assigned to a store 
                        // what i mean by store is inventory
                        //if ((int)itemRow["StoreID"] == 0)
                        {
                            this.billTableModel.AssignItmeInvetory(billRow.SelectedItemID, this.selectedBillStoreID);
                        }
                    }

                    // after that update the item balance in that store
                    if (String.IsNullOrEmpty(billRow.Note))
                        billRow.Note = this.BillNumber + "/" + this.BillTypeName;

                    Tuple<int, int, decimal, decimal, string> tuple = new Tuple<int, int, decimal, decimal, string>
                    (
                        this.BillID, billRow.SelectedItemID, decimal.Parse(billRow.Quantity),
                        decimal.Parse(billRow.Price), billRow.Note
                    );

                    parametersList.Add(tuple);

                    if(this.BillTypeNumber == 0 || this.BillTypeNumber == 3) // مشتريات او مردود مبيعات اضف الى رصيد الاضناف
                    {
                        this.billTableModel.AddItemBalance(billRow.SelectedItemID,
                          double.Parse(billRow.Quantity));
                    }

                    if (this.BillTypeNumber == 1 || this.BillTypeNumber == 2) // مبيعات مردودات مشتريات اخصم من رصيد الاضناف
                    {
                        this.billTableModel.SubstractItemBalance(billRow.SelectedItemID,
                            double.Parse(billRow.Quantity));
                    }
                }
            }
            this.billTableModel.InsertBill2(parametersList);
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
        protected override void NewView()
        {
            try
            {
                base.ViewMode = ViewModes.Editing;
                //GetBillLastNumber();
                this.BillNumber = this.billTableModel.GetMax1BillNumber(this.BillTypeID).ToString();

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
        public override void InitializingFields()
        {
            this.BillID = 0;
            this.SelectedBillAccountID = 0;
            this.BillAccountName = null;

            this.SelectedBillStoreID = 0;
            this.BillStoreName = null; 

            this.BillNote = string.Empty;
            this.BillTotal = "0";
            this.BillDiscount = "0";
            this.BillNet = "0";

            this.BillDayNumber = string.Empty;
            this.BillPaymentType = string.Empty;
            this.BillCollection.Clear();
        }
        public override ErrorType CheckingFields()
        {
            if (this.selectedBillAccountID == 0)
            {
                MessageBox.Show("الحساب فارغ");
                return ErrorType.AccountEmpty;
            }
            if (this.SelectedBillStoreID == 0)
            {
                MessageBox.Show("المستودع فارغ");
                return ErrorType.StoreEmpty;
            }
            if (int.Parse(BillTotal) == 0)
            {
                MessageBox.Show("الفاتورة فارغ");
                return ErrorType.BillTotalEmpty;
            }
            if (this.BillCollection.Count == 0)
            {
                MessageBox.Show("الفاتورة فارغ");
                return ErrorType.BillCollectionEmpty;
            }
            if (String.IsNullOrEmpty(this.BillPaymentType))
            {
                MessageBox.Show("اختر طريقة الدفع");
                return ErrorType.BillPaymentTypeEmpty;
            }
            foreach (BillRow billRow in this.BillCollection)
            {
                if (String.IsNullOrEmpty(billRow.ItemName) && billRow.Total != 0)
                {
                    MessageBox.Show("الفاتورة فارغ");
                    return ErrorType.BillCollectionEmpty;
                }
            }

            return ErrorType.NoError;
        }
        private void RepeatRow()
        {
            try
            {

                BillRow selectedRow = this.BillCollection[this.BillRowIndex];
                BillRow newBillRow = selectedRow.Copy();
               
                this.BillCollection.Insert(this.BillRowIndex + 1, newBillRow);

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
        private void RemoveRow()
        {
            try
            {

                this.BillCollection.RemoveAt(this.BillRowIndex);
               
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
        private void AddRow()
        {
            try
            {
                BillRow billRow = new BillRow();
                //billRow.ItemName = "fdghfghfg";
                //billRow.RowIndex = rowIndex++;
                this.BillCollection.Add(billRow);

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
    public class BillRow : BindableBase
    {
        private readonly object lockToken = new object();

        private string barCode = string.Empty;
        public string BarCode
        {
            get => barCode;
            set
            {
                SetProperty(ref barCode, value);
            }
        }

        private string itemName = string.Empty;
        public string ItemName
        {
            get => itemName;
            set
            {
                SetProperty(ref itemName, value);
            }
        }
        private int selectedItemID;
        public int SelectedItemID
        {
            get => selectedItemID;
            set
            {
                SetProperty(ref selectedItemID, value);
            }
        }

        private string selectedItemBarCode = string.Empty;
        public string SelectedItemBarCode
        {
            get => selectedItemBarCode;
            set
            {
                SetProperty(ref selectedItemBarCode, value);
            }
        }


        private string quantity = string.Empty;
        public string Quantity
        {
            get => quantity;
            set
            {
                SetProperty(ref quantity, value);
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        Total = decimal.Parse(price) * decimal.Parse(quantity);
                    }
                    catch (Exception ex) { /*Debug.Fail(ex.Message);*/ }
                }
                else
                {
                    Total = 0;
                }
            }
        }

        private string price = string.Empty;
        public string Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        Total = decimal.Parse(price) * decimal.Parse(quantity);
                    }
                    catch (Exception ex) { /*Debug.Fail(ex.Message);*/ }
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
                note = value;
            }
        }
        private List<Tuple<int, string, string>> itemsList;
        public List<Tuple<int, string, string>> ItemsList
        {
            get
            {
                lock (lockToken)
                {

                    return itemsList;
                }
            }
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref itemsList, value);
                }
            }
        }
        public BillRow Copy()
        {
            BillRow billRow = new BillRow();
            billRow.ItemName = this.ItemName;
            billRow.Quantity = this.Quantity;
            billRow.Price = this.Price;
            billRow.Total = this.Total;
            billRow.Note = this.Note;
            if (!(this.ItemsList is null))
            {
                foreach (var ele in this.ItemsList)
                    billRow.ItemsList.Add(ele);
            }
            
            return billRow;
        }
        private bool isItemNameDropOpened;
        public bool IsItemNameDropOpened
        {
            get => isItemNameDropOpened;
            set
            {
                SetProperty(ref isItemNameDropOpened, value);
            }
        }
        private bool isLoadingItemCmBx = false;
        public bool IsLoadingItemCmBx
        {
            get => isLoadingItemCmBx;
            set
            {
                SetProperty(ref isLoadingItemCmBx, value);
            }
        }
    }
}
