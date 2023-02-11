using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DataEntitiesLib;
using System.Windows.Input;
using System.Windows;
using ModelsLib;
using System.Data;
using System.Windows.Controls;
using FrameWorkLib;
namespace ViewModelsLib
{
    /// <summary>
    /// Accounts (دليل الحسابات) Guid ViewMode
    /// </summary>
    public class AccountTableViewModel: BindableBase,  IDisposable 
    {
        private bool isTableLoadedFromTreeView = false;

        private bool isTableLoadedFromSearch = false;

        // accounts Model
        private AccountTableModel accountsTableModel;
        //
        private AccountTable accountTable;
        public AccountTable AccountTable 
        { 
            get
            {
                if (accountTable is null)
                   return accountTable = new AccountTable();
                else
                    return accountTable;
            }
            set
            {
                SetProperty(ref accountTable, value);
            }
        }

        private DataRowView listViewSelectedItem;

        private string accountParent;
        public string AccountParent
        {
            get => accountParent;
            set
            {
                SetProperty(ref accountParent, value);
            }
        }

        private string selectedEndAccount;
        public string SelectedEndAccount
        {
            get => selectedEndAccount;
            set
            {
                SetProperty(ref selectedEndAccount, value);
            }
        }

        private int selectedEndAccountID;
        public int SelectedEndAccountID
        {
            get => selectedEndAccountID;
            set
            {
                SetProperty(ref selectedEndAccountID, value);
            }
        }

        private List<string> endAccountCmBxList;
        public List<string> EndAccountCmBxList
        {
            get => endAccountCmBxList;
            set
            {
                SetProperty(ref endAccountCmBxList, value);
            }
        }

        private string accountName;
        public string AccountName 
        { 
            get => accountName;
            set
            {
                SetProperty(ref accountName, value);
            }
        }

        private string selectedAccountParent;
        public string SelectedAccountParent
        {
            get => selectedAccountParent;
            set
            {
                SetProperty(ref selectedAccountParent, value);
            }
        }

        private bool isDropOpenParentAccountCmBx;
        public bool IsDropOpenParentAccountCmBx
        {
            get => isDropOpenParentAccountCmBx;
            set
            {
                SetProperty(ref isDropOpenParentAccountCmBx, value);
            }
        }

        private List<string> listParentAccountCmBx;
        public List<string> ListParentAccountCmBx
        {
            get => listParentAccountCmBx;
            set
            {
                SetProperty(ref listParentAccountCmBx, value);
            }
        }

        private int selectedAccountParentID;
        internal int SelectedAccountParentID
        {
            get => selectedAccountParentID;
            set => selectedAccountParentID = value;
        }

        private string accountCode;
        public string AccountCode 
        {
            get => accountCode;
            set
            {
                SetProperty(ref accountCode, value);
            }
        }

        private string accountDate;
        public string AccountDate
        {
            get => accountDate;
            set
            {
                SetProperty(ref accountDate, value);
            }
        }
        public DataRowView ListViewSelectedItem 
        {
            get => listViewSelectedItem;
            set
            {
                SetProperty(ref listViewSelectedItem, value);
            }
        }

        private bool disposedValue = false;

        #region Main Accounts Tab binding properties
        private AccountTable accountsTable_Main;
        public AccountTable AccountsTable_Main
        {
            get => accountsTable_Main;
            set
            {
                SetProperty(ref accountsTable_Main, value);
            }
        }
        private string accountName_Main = string.Empty;
        private string endaccount_Main = string.Empty;
        private string accountCode_Main = string.Empty;
        private string accountDate_Main = string.Empty;
        public string AccountName_Main
        {
            get => accountName_Main;
            set
            {
                SetProperty(ref accountName_Main, value);
            }
        }
        public string Endaccount_Main
        {
            get => endaccount_Main;
            set
            {
                SetProperty(ref endaccount_Main, value);
            }
        }
        public string AccountCode_Main
        {
            get => accountCode_Main;
            set
            {
                SetProperty(ref accountCode_Main, value);
            }
        }
        public string AccountDate_Main
        {
            get => accountDate_Main;
            set
            {
                SetProperty(ref accountDate_Main, value);
            }
        }
        #endregion
        #region Commands
        public ICommand InsertAccountCommand { get; set; }
        public ICommand NewAccountCommand { get; set; }
        public ICommand ModifyAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public ICommand ExpandTreeViewCommand { get; set; }
        public ICommand CollapseTreeViewCommand { get; set; }
        public ICommand ShowAllAccountsCommand { get; set; }
        public ICommand SearchAccountsCommand { get; set; }
        public ICommand GetAccountCodeCommand { get; set; }
        public ICommand AccountNameTextChangCommand { get; set; }
        public ICommand AccountsTreeViewSelectionChangedCommand { get; set; }
        public ICommand SelectionChangedListViewCommand { get; set; }
        public ICommand TextChangedSearchByNameCommand { get; set; }
        public ICommand CloseNewAccountFormCommand { get; set; }
        public ICommand ShowNewAccountFormCommand { get; set; }
        #region main accounts
        public ICommand InsertMainCommand { get; set; }
        public ICommand ModMainCommand { get; set; }
        public ICommand SaveMainCommand { get; set; }
        public ICommand ViewLoadedCommand { get; set; }
        #endregion
        #region end accounts
        public ICommand SelectionChangedEndCommand { get; set; }
        public ICommand DropOpenedChangedEndCommand { get; set; }
        #endregion
        #endregion
        public AccountTableViewModel()
        {
            accountsTableModel = new AccountTableModel();
            ExpandTreeViewCommand = new FrameWorkLib.DelegateCommand(ExpandTreeView);
            CollapseTreeViewCommand = new FrameWorkLib.DelegateCommand(CollapseTreeView);
            ShowAllAccountsCommand = new FrameWorkLib.DelegateCommand(ShowAllAccounts);
            SearchAccountsCommand = new FrameWorkLib.DelegateCommand(SearchAccounts);
            GetAccountCodeCommand = new FrameWorkLib.DelegateCommand(GetAccountCode);
            AccountNameTextChangCommand = new FrameWorkLib.DelegateCommand(AccountNameTextChanged);
            AccountsTreeViewSelectionChangedCommand = new FrameWorkLib.DelegateCommand(AccountsTreeViewSelectionChanged);
            SelectionChangedListViewCommand = new FrameWorkLib.DelegateCommand(SelectionChangedListView);

            TextChangedSearchByNameCommand = new FrameWorkLib.DelegateCommand(TextChangedSearchByName);

            ModifyAccountCommand = new FrameWorkLib.DelegateCommand(ModifyAccount);
            InsertAccountCommand = new FrameWorkLib.DelegateCommand(InsertNewAccounts);
            SaveAccountCommand = new FrameWorkLib.DelegateCommand(Save);

            // main
            InsertMainCommand = new FrameWorkLib.DelegateCommand(Insert_Main);
            ModMainCommand = new FrameWorkLib.DelegateCommand(Modify_Main);
            SaveMainCommand = new FrameWorkLib.DelegateCommand(Save_Main);

            ViewLoadedCommand = new FrameWorkLib.DelegateCommand(ViewLoaded);
            // end
            SelectionChangedEndCommand = new FrameWorkLib.DelegateCommand(SelectionChangedEnd);
            DropOpenedChangedEndCommand = new FrameWorkLib.DelegateCommand(DropOpenedChangedEnd);

        }
        private void DropOpenedChangedEnd()
        {
            try
            {

                List<Tuple<string, int>> tuples = new List<Tuple<string, int>>();
                accountsTableModel.GetEndAccounts(tuples);
                List<string> list = new List<string>();
                foreach (Tuple<string, int> tuple in tuples)
                {
                    list.Add(tuple.Item1);
                }
                EndAccountCmBxList = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectionChangedEnd()
        {
            try
            {
                if (String.IsNullOrEmpty(this.SelectedEndAccount))
                    return;
                this.SelectedEndAccountID = accountsTableModel.GetEndAccount(this.SelectedEndAccount).Item2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ViewLoaded()
        {
            try
            {
                this.AccountsTable_Main = new AccountTable();
                accountsTableModel.GetAccountsFromAccountTable(this.AccountsTable_Main, 0);
                AccountsTable_Main.AcceptChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Save_Main()
        {
            try
            {
                accountsTableModel.UpdateAccountTable(AccountsTable_Main);
                MessageBox.Show("تم الحفظ");
                AccountsTable_Main.AcceptChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Modify_Main()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Insert_Main()
        {
            try
            {
                             
               int endAccountID = int.Parse((((string)Endaccount_Main).Split('(', ',', ')')).ToArray()[2]);
                
                
                AccountRow newAccountRow = AccountsTable_Main.NewAccountRow();
                newAccountRow.Name = AccountName_Main;
                newAccountRow.Code = int.Parse(AccountCode_Main);
                newAccountRow.ParentID = 0;
                newAccountRow.EndAccountID = endAccountID;
                newAccountRow.Date = DateTime.Parse(AccountDate_Main).Date;

                AccountsTable_Main.AddAccountsRow(newAccountRow);

                MessageBox.Show("تم ادراج الحساب لحفظ التغيرات يرجاء الضغط على زر الحفظ");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("يوجد حقل فارغ");
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"يوجد حساب مشابة {ex.ToString()}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void TextChangedSearchByName(object parameter)
        {

            string accountName = parameter as string;
            if (String.IsNullOrEmpty(accountName) == true)
                return;
            DataTable table = new DataTable();
            accountsTableModel.GetAccounts(table, accountName);

            // clear AccountTable 
            AccountTable.Clear();
            foreach(DataRow row in table.Rows)
            {
                AccountRow accountRow = AccountTable.NewAccountRow();

                accountRow.ID           = (int)row[4];
                accountRow.Code         = (int)row[2];
                accountRow.Name         = (string)row[0];
                accountRow.ParentID     = (int)row[1];
                accountRow.EndAccountID = (int)row[6];

                AccountTable.AddAccountsRow(accountRow);
                AccountTable.AcceptChanges();
                isTableLoadedFromSearch = true;
            }

        }
        /// <summary>
        /// list view selection changed command handler
        /// </summary>
        /// <param name="parameter">listview instant</param>
        private void SelectionChangedListView(object parameter)
        {
            //try
            //{
            //    ListView listView = (parameter as ListView);
            //    if (listView.SelectedItem is null)
            //        return;
            //    DataRowView selsectedRow = listView.SelectedItem as DataRowView;
            //    int accountID = (int)selsectedRow["ID"];
            //    DataTable table = new DataTable();
            //    accountsTableModel.GetAccountsFromAccountTable_ByID(table, accountID);
            //    if (table.Rows.Count > 0)
            //    {
            //        foreach (DataRow accountRow in AccountTable.Rows)
            //        {
            //            if ((int)accountRow["ID"] == accountID)
            //            {
            //                if (accountRow.RowState == DataRowState.Unchanged)
            //                    accountRow.SetModified();
            //            }
            //        }
            //        AccountName = selsectedRow["Name"] as string;
            //        AccountCode = (int)selsectedRow["Code"];
            //        AccountParent = new Tuple<string, int>((string)table.Rows[0][3], (int)table.Rows[0][1]);
            //        AccountEndAccount = new Tuple<string, int>((table.Rows[0][5] as string), (int)table.Rows[0][6]);
            //    }
            //    else //  if it is in the acccountTable not in the database
            //    {
            //        // TODO (is done) this if the selsected item it recently added 
            //        // and not saved in the database
            //        if(AccountTable.Count > 0)
            //        {
            //            foreach(AccountRow accountRow in AccountTable.Rows)
            //            {
            //                if ((int)accountRow["ID"] == accountID)
            //                {
            //                    if (accountRow.RowState == DataRowState.Unchanged)
            //                        accountRow.SetModified();

            //                    AccountName = accountRow["Name"] as string;

            //                    AccountCode = (int)accountRow["Code"];

            //                    Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

            //                    int id = (int)accountRow["ParentID"];

            //                    accountsTableModel.GetAccountsFromAccountTable_ByID(ref account, id);

            //                    AccountParent = new Tuple<string, int>(account.Item1, id);

            //                    id = (int)accountRow["EndAccountID"];

            //                    accountsTableModel.GetEndAccount_ByID(ref account, id);

            //                    AccountEndAccount = new Tuple<string, int>(account.Item1, id);
            //                }
            //            }
            //        }
            //    }

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        private void ModifyAccount()
        {
            try
            {
                if (!(ListViewSelectedItem is null))
                {
                    DataRowView rowView = ListViewSelectedItem;
                    AccountRow accountRow = (AccountRow)AccountTable.Rows.Find(rowView["ID"]);

                    // don allow modify the account if its state is added
                    if (accountRow.RowState == DataRowState.Added)
                    {
                        MessageBox.Show("احفظ الحساب ثم قم بتعديله");
                        return;
                    }

                    accountRow.Name = AccountName;
                    accountRow.Code = int.Parse(AccountCode);
                    accountRow.ParentID = accountsTableModel.GetAccount(AccountParent).Item2;
                    accountRow.EndAccountID = accountsTableModel.GetEndAccount(SelectedEndAccount).Item2;
                    accountRow.Date = DateTime.Parse(this.AccountDate);
                    MessageBox.Show("تم تعديل الحساب لحفظ التغيرات يرجاء الضغط على زر الحفظ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void AccountsTreeViewSelectionChanged(object parameter)
        {
            try
            {
                TreeViewItem item = (TreeViewItem)((parameter as RoutedEventArgs).OriginalSource);

                if ((item.Header as TextBlock).Text.Equals("دليل الحسابات"))
                    return; // Do nothing
                else
                {
                    int id = Convert.ToInt32(item.Tag.ToString());
                    // go to the database and grab this account
                    DataTable accountTable = new DataTable();
                    accountsTableModel.GetAccountsFromAccountTable_ByID(accountTable, id);
                    // this is for main accounts ()
                    if (accountTable.Rows.Count == 0)
                    {
                        AccountName = string.Empty;
                        AccountCode = string.Empty;
                        AccountParent = string.Empty;
                        SelectedEndAccount = string.Empty;
                        AccountTable.Clear();
                        return;
                    }
                    DataRow accountRow = ((DataRow)accountTable.Rows[0]);

                    AccountName = accountRow[0] as string;

                    AccountParent = accountRow[3] as string;

                    SelectedEndAccount = accountRow[5] as string;
                    // If this line moved up its value will change once the AccountEndAccount is set 
                    // becuase it will trigger the GetAccountCode
                    AccountCode = ((int)accountRow[2]).ToString();

                    AccountDate = ((DateTime)accountRow[7]).ToShortDateString();
                    // add the selected account to the account table
                    AccountTable.Clear();
                    AccountRow newAccountRow = AccountTable.NewAccountRow();
                    newAccountRow.ID = (int)accountRow[4];
                    newAccountRow.Name = AccountName;
                    newAccountRow.Code = int.Parse(AccountCode);
                    // TODO: object not set to an instant exception occurs sometimes

                    newAccountRow.ParentID = (int)accountRow[1];

                    newAccountRow.EndAccountID = (int)accountRow[6];

                    newAccountRow.Date = ((DateTime)accountRow[7]);

                    AccountTable.AddAccountsRow(newAccountRow);

                    AccountTable.AcceptChanges();

                    isTableLoadedFromTreeView = true;
                    isTableLoadedFromSearch = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void AccountNameTextChanged(object parameter)
        {
            try
            {
                TextBox textBox = parameter as TextBox;
                AccountName = textBox.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  this command occurs when the main account combo box selecttion changed occurs
        /// </summary>
        /// <param name="parameter">the combo box instant</param>
        private void GetAccountCode()
        {
            try
            {
                
                if (!(String.IsNullOrEmpty(this.selectedAccountParent)))
                {
                    int parentID = accountsTableModel.GetAccount(this.selectedAccountParent).Item2;

                    this.SelectedAccountParentID = parentID;
                    AccountTable table = new AccountTable();

                    // get the account children
                    accountsTableModel.GetAccountsFromAccountTable(table, parentID);

                    if (table.Count == 0)
                    {
                        Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

                        accountsTableModel.GetAccountsFromAccountTable_ByID(ref account, parentID);

                        int parentCode = account.Item2;

                        AccountCode = Convert.ToInt32($"{parentCode}{1}").ToString();
                    }
                    else
                    {
                        int id = parentID;

                        AccountCode = FindMaxChildCode(table , id).ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// This command is binded to the treeview in view
        /// </summary>
        /// <param name="parameter"></param>
        private void CollapseTreeView(object parameter)
        {
            AccountTable.Clear();
        }
        /// <summary>
        /// This command is binded to the treeview in view 
        /// </summary>
        /// <param name="parameter"></param>
        private void ExpandTreeView(object parameter)
        {
            try
            {
                isTableLoadedFromTreeView = false;
                AccountTable.Clear();
                TreeViewItem item = (TreeViewItem)((parameter as RoutedEventArgs).OriginalSource);
                item.Items.Clear();
                List<Tuple<string, int, int>> accountsName = new List<Tuple<string, int, int>>();
                accountsTableModel.GetAccountsFromAccountTable(accountsName, int.Parse(item.Tag.ToString()));
                foreach (Tuple<string, int, int> ele in accountsName)
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = ele.Item2;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"{ele.Item3} {ele.Item1}";
                    newItem.Header = textBlock;
                    newItem.Items.Add("");
                    item.Items.Add(newItem);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }     
        /// <summary>
        /// this command is called when main account combo box drop event occurs
        /// </summary>
        /// <param name="parameter">is ComboBox</param>
        private void ShowAllAccounts(object parameter)
        {
            try
            {
                ComboBox comboBox = parameter as ComboBox;
                List<Tuple<string, int>> allaccountsList = new List<Tuple<string, int>>();
                accountsTableModel.GetAccountsFromAccountTable(allaccountsList);
                comboBox.ItemsSource = allaccountsList;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// this command will search the database by name intered in the combo box
        /// </summary>
        /// <param name="parameter"></param>
        private void SearchAccounts()
        {
            try
            {
                // if the user clear the textbox allow the user to make search again
                if(String.IsNullOrEmpty(this.AccountParent))
                {
                    this.SelectedAccountParent = string.Empty;
                    this.ListParentAccountCmBx = null;
                    return;
                }

                // if there is a selection return
                if (!String.IsNullOrEmpty(this.SelectedAccountParent))
                    return;

                this.IsDropOpenParentAccountCmBx = true;
                string name = this.AccountParent;
                List<Tuple<string, int>> allaccountsList = new List<Tuple<string, int>>();
                accountsTableModel.GetAccountsFromAccountTable(allaccountsList, name);
                List<string> list = new List<string>();
                foreach(Tuple<string, int> tuple in allaccountsList)
                {
                    list.Add(tuple.Item1);
                }
                this.ListParentAccountCmBx = list;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InsertNewAccounts()
        {
            AccountTable.Clear();
            int previuosCode = int.Parse(AccountCode);
            try
            {
                // get the account children
                AccountTable table = new AccountTable();

                int parentID = this.SelectedAccountParentID;


                int endAccountID = this.selectedEndAccountID;
               
                // account code from database
                accountsTableModel.GetAccountsFromAccountTable(table, parentID);
                AccountCode = FindMaxChildCode(table, parentID).ToString();
                // account code from the current account table that is bieing inserted into
                // if count is greater than 0
                if (AccountTable.Count > 0)
                {
                    int code = FindMaxChildCode(AccountTable, parentID);
                    // take the max
                    if (code >= int.Parse(AccountCode))
                        AccountCode = code.ToString();
                    //
                }
                AccountRow newAccountRow = AccountTable.NewAccountRow();

                newAccountRow.Name = AccountName;
                newAccountRow.Code = int.Parse(AccountCode);
                newAccountRow.ParentID = parentID;
                newAccountRow.EndAccountID = endAccountID;
                newAccountRow.Date = DateTime.Parse(this.AccountDate);


                accountsTableModel.UpdateAccountTable(AccountTable);
                MessageBox.Show("تم ادراج الحساب ");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("يوجد حقل فارغ");
                AccountCode = previuosCode.ToString();
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"يوجد حساب مشابة {ex.ToString()}");

                AccountCode = previuosCode.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AccountCode = previuosCode.ToString();
            }
        }
        private void Save()
        {
            try
            {
                if(AccountTable.Count == 0)
                {
                    MessageBox.Show("لم يتم ادراج حساب");
                    return;
                }
                accountsTableModel.UpdateAccountTable(AccountTable);
                MessageBox.Show("تم الحفظ");
                AccountTable.Clear();
            }
            catch (Exception ex)
            {
                AccountTable.Clear();
                MessageBox.Show(ex.Message);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
           
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                InsertAccountCommand = null;
                NewAccountCommand = null;
                ModifyAccountCommand = null;
                SaveAccountCommand = null;
                disposedValue = true;
            }
        }
        private int FindMaxChildCode(AccountTable table, int parentID)
        {
            // get the account code
            Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

            int id = parentID;

            accountsTableModel.GetAccountsFromAccountTable_ByID(ref account, id);

            int parentCode = account.Item2;

            int max = 0;
            foreach (AccountRow accountRow in table.AccountRows)
            {
                if (((int)accountRow["ParentID"]) == parentID)
                {
                    int rowCode = (int)accountRow[3];

                    if (rowCode >= max)
                        max = rowCode;
                }
            }

            if(max == 0)
                return Convert.ToInt32($"{parentCode}{1}");

            List<char> c = max.ToString().ToCharArray().ToList();

            List<char> p_code = parentCode.ToString().ToCharArray().ToList();

            c.RemoveRange(0,p_code.Count);

            int childCode = Convert.ToInt32(new string(c.ToArray()));  //split last digit from number

            childCode = childCode + 1;

            return Convert.ToInt32($"{parentCode}{childCode}");
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
