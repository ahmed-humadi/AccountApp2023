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

namespace ViewModelsLib
{
    /// <summary>
    /// Accounts (دليل الحسابات) Guid ViewMode
    /// </summary>
    public class AccountTableViewModel:IDisposable ,INotifyPropertyChanged
    {
        private bool isTableLoadedFromTreeView = false;

        private bool isTableLoadedFromSearch = false;

        private ModelsLib.AccountTableModel accountsTableModel;

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
                accountTable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountTable)));
            }
        }

        private DataRowView listViewSelectedItem;

        private string accountName;

        private object accountParent;

        private int accountCode;

        private object accountEndAccount;
        public string AccountName 
        { 
            get => accountName;
            set
            {
                accountName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountName)));
            }
        }
        public object AccountParent
        {
            get => accountParent;
            set
            {
                accountParent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountParent)));
            }
        }
        public int AccountCode 
        {
            get => accountCode;
            set
            {
                accountCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountCode)));

            }
        }
        public object AccountEndAccount
        {
            get => accountEndAccount;
            set
            {
                accountEndAccount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountEndAccount)));
            }
        }
        public DataRowView ListViewSelectedItem 
        {
            get => listViewSelectedItem;
            set
            {
                listViewSelectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListViewSelectedItem)));
            }
        }

        private bool disposedValue = false;

        public event PropertyChangedEventHandler PropertyChanged;
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
            try
            {
                ListView listView = (parameter as ListView);
                if (listView.SelectedItem is null)
                    return;
                DataRowView selsectedRow = listView.SelectedItem as DataRowView;
                int accountID = (int)selsectedRow["ID"];
                DataTable table = new DataTable();
                accountsTableModel.GetAccountsFromAccountTable_ByID(table, accountID);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow accountRow in AccountTable.Rows)
                    {
                        if ((int)accountRow["ID"] == accountID)
                        {
                            if (accountRow.RowState == DataRowState.Unchanged)
                                accountRow.SetModified();
                        }
                    }
                    AccountName = selsectedRow["Name"] as string;
                    AccountCode = (int)selsectedRow["Code"];
                    AccountParent = new Tuple<string, int>((string)table.Rows[0][3], (int)table.Rows[0][1]);
                    AccountEndAccount = new Tuple<string, int>((table.Rows[0][5] as string), (int)table.Rows[0][6]);
                }
                else //  if it is in the acccountTable not in the database
                {
                    // TODO (is done) this if the selsected item it recently added 
                    // and not saved in the database
                    if(AccountTable.Count > 0)
                    {
                        foreach(AccountRow accountRow in AccountTable.Rows)
                        {
                            if ((int)accountRow["ID"] == accountID)
                            {
                                if (accountRow.RowState == DataRowState.Unchanged)
                                    accountRow.SetModified();

                                AccountName = accountRow["Name"] as string;

                                AccountCode = (int)accountRow["Code"];

                                Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

                                int id = (int)accountRow["ParentID"];

                                accountsTableModel.GetAccountsFromAccountTable_ByID(ref account, id);

                                AccountParent = new Tuple<string, int>(account.Item1, id);

                                id = (int)accountRow["EndAccountID"];

                                accountsTableModel.GetEndAccount_ByID(ref account, id);

                                AccountEndAccount = new Tuple<string, int>(account.Item1, id);
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    if (accountRow.RowState == DataRowState.Modified)
                    {
                        accountRow.Name = AccountName;
                        accountRow.Code = AccountCode;
                        accountRow.ParentID = (AccountParent as Tuple<string, int>).Item2;
                        accountRow.EndAccountID = (AccountEndAccount as Tuple<string, int>).Item2;
                        MessageBox.Show("تم تعديل الحساب لحفظ التغيرات يرجاء الضغط على زر الحفظ");

                    }
                }
            }
           catch(Exception ex)
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
                        AccountCode = 0;
                        AccountParent = null;
                        AccountEndAccount = null;
                        AccountTable.Clear();
                        return;
                    }
                    DataRow accountRow = ((DataRow)accountTable.Rows[0]);

                    AccountName = accountRow[0] as string;
                    int ParentID = (int)accountRow[1];
                   
                    string parentName = accountRow[3] as string;
                    AccountParent = new Tuple<string, int> (parentName, ParentID);

                    int AccountID = (int)accountRow[4];
                    string endAccountName = accountRow[5] as string;
                    int endAccountID = (int)accountRow[6];
                    AccountEndAccount = new Tuple<string, int>(endAccountName, endAccountID);
                    // If this line moved up its value will change once the AccountEndAccount is set 
                    // becuase it will trigger the GetAccountCode
                    AccountCode = (int)accountRow[2];
                    // add the selected account to the account table
                    AccountTable.Clear();
                    AccountRow newAccountRow = AccountTable.NewAccountRow();
                    newAccountRow.ID = AccountID;
                    newAccountRow.Name = AccountName;
                    newAccountRow.Code = AccountCode;
                    // TODO: object not set to an instant exception occurs sometimes
                    
                    newAccountRow.ParentID = ParentID;

                    newAccountRow.EndAccountID = endAccountID;

                    AccountTable.AddAccountsRow(newAccountRow);

                    AccountTable.AcceptChanges();
                    
                    isTableLoadedFromTreeView = true;
                    isTableLoadedFromSearch = false;
                }
            }
            catch(Exception ex)
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
        private void GetAccountCode(object parameter)
        {
            try
            {
                ComboBox comboBox = parameter as ComboBox;

                if (!(comboBox.SelectedItem is null))
                {
                    int parentID = (comboBox.SelectedItem as Tuple<string, int>).Item2;

                    AccountTable table = new AccountTable();

                    // get the account children
                    accountsTableModel.GetAccountsFromAccountTable(table, parentID);

                    if (table.Count == 0)
                    {
                        Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

                        accountsTableModel.GetAccountsFromAccountTable_ByID(ref account, parentID);

                        int parentCode = account.Item2;

                        AccountCode = Convert.ToInt32($"{parentCode}{1}");
                    }
                    else
                    {
                        int id = parentID;

                        AccountCode = FindMaxChildCode(table , id);
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
        private void SearchAccounts(object parameter)
        {
            try
            {
                ComboBox comboBox = parameter as ComboBox;
                comboBox.IsDropDownOpen = true;
                string name = comboBox.Text;
                List<Tuple<string, int>> allaccountsList = new List<Tuple<string, int>>();
                accountsTableModel.GetAccountsFromAccountTable(allaccountsList, name);
                comboBox.ItemsSource = allaccountsList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        Random random = new Random(1);
        private void InsertNewAccounts()
        {
            int previuosCode = AccountCode;
            try
            {
                // get the account children
                AccountTable table = new AccountTable();
                int parentID = 0; int endAccountID = 0;
                if (AccountParent is Tuple<string, int>)
                    parentID = ((Tuple<string, int>)AccountParent).Item2;
                else
                {
                     parentID = int.Parse((((string)AccountParent).Split('(', ',', ')')).ToArray()[2]);
                }
                if (AccountEndAccount is Tuple<string, int>)
                    endAccountID = (AccountEndAccount as Tuple<string, int>).Item2;
                else
                {
                    endAccountID = int.Parse((((string)AccountEndAccount).Split('(', ',', ')')).ToArray()[2]);
                }
                // account code from database
                accountsTableModel.GetAccountsFromAccountTable(table, parentID);
                AccountCode = FindMaxChildCode(table, parentID);
                // account code from the current account table that is bieing inserted into
                // if count is greater than 0
                if (AccountTable.Count > 0)
                {
                    int code = FindMaxChildCode(AccountTable, parentID);
                    // take the max
                    if (code >= AccountCode)
                        AccountCode = code;
                    //
                }
                AccountRow newAccountRow = AccountTable.NewAccountRow();
                newAccountRow.ID = random.Next(); 
                newAccountRow.Name = AccountName;
                newAccountRow.Code = AccountCode;
                newAccountRow.ParentID = parentID;
                newAccountRow.EndAccountID = endAccountID;

                AccountTable.AddAccountsRow(newAccountRow);

                //AccountTable.AcceptChanges();

                MessageBox.Show("تم ادراج الحساب لحفظ التغيرات يرجاء الضغط على زر الحفظ");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("يوجد حقل فارغ");
                AccountCode = previuosCode;
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"يوجد حساب مشابة {ex.ToString()}");

                AccountCode = previuosCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AccountCode = previuosCode;
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
                // change the state of all rows
                // the first row is modiflied allways
                // the reset of rows are added
                /*if (isTableLoadedFromTreeView == true)
                {

                    AccountTable.Rows[0].SetModified();
                    for (int i = 1; i < AccountTable.Count; i++)
                    {
                        AccountTable.Rows[i].SetAdded();
                    }
                }
                else if (isTableLoadedFromSearch == true)
                {
                    accountsTableModel.UpdateAccountTable(AccountTable);
                    MessageBox.Show("تم الحفظ");
                    AccountTable.Clear();
                    return;
                }
                else
                {
                    for (int i = 0; i < AccountTable.Count; i++)
                    {
                        AccountTable.Rows[i].SetAdded();
                    }
                }*/
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
