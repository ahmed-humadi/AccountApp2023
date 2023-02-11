using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkLib;
using DataEntitiesLib;
using System.Windows.Input;
using System.Windows;
using ModelsLib;
using System.Windows.Controls;

namespace ViewModelsLib
{
    public class AccountTableViewModel1 : BindableBase
    {
        private readonly object lockToken = new object();

        private AccountTableModel accountsModel;

        private AccountTable accountTable;
        public AccountTable AccountTable
        {
            get
            {
                lock(lockToken)
                {
                    if (accountTable is null)
                        return accountTable = new AccountTable();
                    return accountTable;
                }
            }
            set
            {
                SetProperty(ref accountTable, value);
            }
        }

        #region Create New or Modify account Properties

        private bool isAccountNameTxtBxFocused;

        public bool IsAccountNameTxtBxFocused
        {
            get { return isAccountNameTxtBxFocused; }
            set
            {
                SetProperty(ref isAccountNameTxtBxFocused, value);
            }
        }

        private bool isDropOpenEnd;
        public bool IsDropOpenEnd
        {
            get => isDropOpenEnd;
            set
            {
                SetProperty(ref isDropOpenEnd, value);
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
        private string accountCode = string.Empty;
        public string AccountCode
        {
            get => accountCode;
            set
            {
                SetProperty(ref accountCode, value);
            }
        }
        private string accountParentName = string.Empty;
        public string AccountParentName
        {
            get => accountParentName;
            set
            {
                SetProperty(ref accountParentName, value);
            }
        }
        private string accountEndAccountName = string.Empty;
        public string AccountEndAccountName
        {
            get => accountEndAccountName;
            set
            {
                SetProperty(ref accountEndAccountName, value);
            }
        }

        private string accountDate = string.Empty;
        public string AccountDate
        {
            get => accountDate;
            set
            {
                SetProperty(ref accountDate, value);
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
        private int selectedAccountParentID;
        public int SelectedAccountParentID
        {
            get => selectedAccountParentID;
            set => selectedAccountParentID = value;
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
        private List<string> listEndAccountCmBx;
        public List<string> ListEndAccountCmBx
        {
            get => listEndAccountCmBx;
            set
            {
                SetProperty(ref listEndAccountCmBx, value);
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
        private string searchText;
        public string SearchText
        {
           get => searchText;
           set
           {
                SetProperty(ref searchText, value);
           }
        }
        // if the created account is main account this is false
        private bool accountPaernCmBx = true;
        public bool AccountPaernCmBx
        {
            get => accountPaernCmBx;
            set
            {
                SetProperty(ref accountPaernCmBx, value);
            }
        }
        // if the created account is main account this is true
        private bool accountCodeTxtBx = false;
        public bool AccountCodeTxtBx
        {
            get => accountCodeTxtBx;
            set
            {
                SetProperty(ref accountCodeTxtBx, value);
            }
        }
        #endregion

        #region Commands
        public ICommand SaveAccountCommand { get; set; }
        public ICommand ModifyAccountCommand { get; set; }
        public ICommand AccountsViewLoadedCommand { get; set; }
        public ICommand AccountParentTextChangedCommand { get; set; }
        public ICommand AccountParentselectionChangedCommand { get; set; }
        public ICommand EnterKeyPressedChangedEndCommand { get; set; }
        public ICommand CreateNewAccountCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ExpandTreeViewCommand { get; set; }
        public ICommand CreateNewMainAccountCommand { get; set; }
        public ICommand AccountParentCmBxSearchBtnCommand { get; set; }

        public ICommand AccountEndCmBxSearchBtnCommand { get; set; }

        public ICommand AccountEndTextChangedCommand { get; set; }

        #endregion
        public AccountTableViewModel1()
        {
            accountsModel = new AccountTableModel();
            SaveAccountCommand = new DelegateCommand(SaveAccount);
            ModifyAccountCommand = new DelegateCommand(ModifyAccount);
            AccountsViewLoadedCommand = new DelegateCommand(AccountsViewLoaded);
            AccountParentTextChangedCommand = new DelegateCommand(AccountParentTextChanged);
            AccountParentselectionChangedCommand = new DelegateCommand(AccountParentselectionChanged);
            //EnterKeyPressedChangedEndCommand = new DelegateCommand(EnterKeyPressedChangedEnd);
            // this command to clear the data
            CreateNewAccountCommand = new DelegateCommand(CreateNewAccount);
            SearchCommand = new DelegateCommand(SearchAccount);
            ExpandTreeViewCommand = new DelegateCommand(ExpandTreeView);
            CreateNewMainAccountCommand = new DelegateCommand(CreateNewMainAccount);
            AccountParentCmBxSearchBtnCommand = new DelegateCommand(AccountParentCmBxSearchBtn);
            AccountEndCmBxSearchBtnCommand = new DelegateCommand(AccountEndCmBxSearchBtn);
            AccountEndTextChangedCommand = new DelegateCommand(AccountEndTextChanged);

        }

        private void AccountEndTextChanged()
        {
            //throw new NotImplementedException();
        }

        private void AccountEndCmBxSearchBtn()
        {
            try
            {
                List<Tuple<string, int>> tuples = new List<Tuple<string, int>>();
                accountsModel.GetEndAccounts(tuples);
                List<string> list = new List<string>();
                foreach (Tuple<string, int> tuple in tuples)
                {
                    list.Add(tuple.Item1);
                }
                ListEndAccountCmBx = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AccountParentCmBxSearchBtn()
        {
            try
            {
                this.IsDropOpenParentAccountCmBx = true;
                List<Tuple<string, int>> allaccountsList = new List<Tuple<string, int>>();
                accountsModel.GetAccountsFromAccountTable(allaccountsList);
                List<string> list = new List<string>();
                foreach (Tuple<string, int> tuple in allaccountsList)
                {
                    list.Add(tuple.Item1);
                }
                this.ListParentAccountCmBx = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateNewMainAccount()
        {
            this.AccountPaernCmBx = false;
            this.AccountCodeTxtBx = true;
            try
            {
                this.AccountName = string.Empty;
                this.AccountCode = string.Empty;
                this.AccountParentName = string.Empty;
                this.AccountEndAccountName = string.Empty;
                this.AccountDate = string.Empty;

                // clear the accountcmbx list
                if (!(this.ListParentAccountCmBx is null))
                    this.ListParentAccountCmBx.Clear();
                if (!(this.ListEndAccountCmBx is null))
                    this.ListEndAccountCmBx.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SearchAccount()
        {
            try
            {
                if(String.IsNullOrEmpty(this.SearchText))
                {
                
                    AccountsViewLoaded();
                    
                    return;
                }
                // is it accountCode
                int accountCode;
                if(int.TryParse(this.SearchText, out accountCode))
                {
                    // do work 
                 
                    // return
                    return;
                }
                // is it accountdate
                DateTime accountDate;
                if (DateTime.TryParse(this.SearchText, out accountDate))
                {
                    // do work 

                    // return
                    return;
                }
                // tex
                // is it accountName
                this.AccountTable.Clear();
                System.Data.DataTable dataTable = new System.Data.DataTable();
                accountsModel.GetAccounts(AccountTable, this.SearchText);
                return;
                // is it accountParent


                // is it accountEndAccount

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateNewAccount()
        {
            try
            {
                IsNewAccount = true;
                this.AccountPaernCmBx = true; 
                this.AccountName = string.Empty;
                this.AccountCode = string.Empty;
                this.AccountParentName = string.Empty;
                this.AccountEndAccountName = string.Empty;
                this.AccountDate = string.Empty;

                // clear the accountcmbx list
                if (!(this.ListParentAccountCmBx is null))
                    this.ListParentAccountCmBx.Clear();
                if (!(this.ListEndAccountCmBx is null))
                    this.ListEndAccountCmBx.Clear();

                this.IsAccountNameTxtBxFocused = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AccountParentTextChanged()
        {
            try
            {
                // if the user clear the textbox allow the user to make search again
                if (String.IsNullOrEmpty(this.AccountParentName))
                {
                    this.SelectedAccountParent = null; 
                    this.ListParentAccountCmBx = null;
                    this.AccountCode = null;
                    return;
                }
                // if there is a selection return
                // becuase text changes when selection is made
                if (!String.IsNullOrEmpty(this.SelectedAccountParent))
                    return;

                this.IsDropOpenParentAccountCmBx = true;
                string name = this.AccountParentName;
                List<Tuple<string, int>> allaccountsList = new List<Tuple<string, int>>();
                accountsModel.GetAccountsFromAccountTable(allaccountsList, name);
                List<string> list = new List<string>();
                foreach (Tuple<string, int> tuple in allaccountsList)
                {
                    list.Add(tuple.Item1);
                }
                this.ListParentAccountCmBx = list;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void AccountParentselectionChanged()
        {
            try
            {
                if (!(String.IsNullOrEmpty(selectedAccountParent)))
                {
                    int parentID = accountsModel.GetAccount(this.selectedAccountParent).Item2;

                    this.SelectedAccountParentID = parentID;
                    AccountTable table = new AccountTable();

                    // get the account children
                    accountsModel.GetAccountsFromAccountTable(table, parentID);

                    if (table.Count == 0)
                    {
                        Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

                        accountsModel.GetAccountsFromAccountTable_ByID(ref account, parentID);

                        int parentCode = account.Item2;

                        AccountCode = Convert.ToInt32($"{parentCode}{1}").ToString();
                    }
                    else
                    {
                        int id = parentID;

                        AccountCode = FindMaxChildCode(table, id).ToString();
                    }
                }
                this.ListParentAccountCmBx = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void AccountsViewLoaded()
        {
            try
            {
                this.AccountTable.Clear();
                accountsModel.GetAccounts(this.AccountTable);

                foreach(AccountRow accountRow in this.AccountTable.AccountRows)
                {
                    int parentId = (int)accountRow["ParentID"];
                    int endAccountid = (int)accountRow["EndAccountID"];
                    Tuple<string, int> tuple = new Tuple<string, int>(string.Empty, 0);
                    accountsModel.GetAccountsFromAccountTable_ByID(ref tuple, parentId);
                    if(string.IsNullOrEmpty(tuple.Item1))
                        accountRow["ParentName"] = "حساب رئيسي";
                    else
                        accountRow["ParentName"] = tuple.Item1;

                    accountsModel.GetEndAccount_ByID(ref tuple, endAccountid);
                    accountRow["EndAccountName"] = tuple.Item1; 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsNewAccount = true;

        private int selectedAccountIndex;
        private void ModifyAccount(object parameter)
        {
            try
            {
                Button button = parameter as Button;
                System.Data.DataRowView selectedRow = ((button.Parent as StackPanel).TemplatedParent as ContentPresenter).Content as System.Data.DataRowView;

                this.selectedAccountIndex = (int)selectedRow["ID"];
                this.AccountName = selectedRow["Name"].ToString();
                this.AccountCode = selectedRow["Code"].ToString();
                this.AccountParentName = selectedRow["ParentName"].ToString();
                this.AccountEndAccountName = selectedRow["EndAccountName"].ToString();
                this.AccountDate = selectedRow["date"].ToString();

                if (this.AccountParentName == "حساب رئيسي")
                {
                    this.AccountPaernCmBx = false;
                    this.AccountCodeTxtBx = true;
                }
                else
                {
                    this.accountCodeTxtBx = true;
                    this.AccountPaernCmBx = true;
                }
                    // clear the accountcmbx list
                    if (!(this.ListParentAccountCmBx is null))
                    this.ListParentAccountCmBx.Clear();
                if (!(this.ListEndAccountCmBx is null))
                    this.ListEndAccountCmBx.Clear();

                this.IsNewAccount = false;
                this.IsAccountNameTxtBxFocused = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveAccount()
        {
            try
            {
                if (IsNewAccount)
                {
                    // when the save clear the account table 
                    this.AccountTable.Clear();
                    // create new row
                    AccountRow newAccountRow = this.AccountTable.NewAccountRow();
                    newAccountRow["Name"] = this.AccountName;
                    newAccountRow["Code"] = int.Parse(this.AccountCode);

                    if (this.AccountPaernCmBx == false)
                        newAccountRow["ParentID"] = 0;
                    else
                    {
                       Tuple<string, int> account = accountsModel.GetAccount(this.AccountParentName);
                        if (account == null)
                        {
                            MessageBox.Show("الحساب الرئسي غير صحيح");
                            return;
                        }

                        newAccountRow["ParentID"] = accountsModel.GetAccount(this.AccountParentName).Item2;
                    }

                    newAccountRow["EndAccountID"] = accountsModel.GetEndAccount(this.AccountEndAccountName).Item2;
                    newAccountRow["Date"] = DateTime.Parse(this.AccountDate);
                    this.AccountTable.AddAccountsRow(newAccountRow);
                    //after save
                    accountsModel.UpdateAccountTable(this.AccountTable);
                }
                else
                {
                    // by clear the table and create new row and set as modified 
                    // we not need to loop through the whole table in UpdateAccountTable function

                    this.AccountTable.Clear();
                    AccountRow newAccountRow = this.AccountTable.NewAccountRow();
                    newAccountRow["ID"] = selectedAccountIndex;
                    newAccountRow["Name"] = this.AccountName;
                    newAccountRow["Code"] = int.Parse(this.AccountCode);
                    if (this.AccountParentName == "حساب رئيسي")
                        newAccountRow["ParentID"] = 0;
                    else
                        newAccountRow["ParentID"] = accountsModel.GetAccount(this.AccountParentName).Item2;
                    
                    newAccountRow["EndAccountID"] = accountsModel.GetEndAccount(this.AccountEndAccountName).Item2;
                    newAccountRow["Date"] = DateTime.Parse(this.AccountDate);
                    this.AccountTable.AddAccountsRow(newAccountRow);
                    this.accountTable.AcceptChanges();
                    newAccountRow.SetModified();
                    //after save
                    accountsModel.UpdateAccountTable(this.AccountTable);
                }
                // after load the view againt to see the changes
                AccountsViewLoaded();

                MessageBox.Show("تم الحفظ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int FindMaxChildCode(AccountTable table, int parentID)
        {
            // get the account code
            Tuple<string, int> account = new Tuple<string, int>(string.Empty, 0);

            int id = parentID;

            accountsModel.GetAccountsFromAccountTable_ByID(ref account, id);

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

            if (max == 0)
                return Convert.ToInt32($"{parentCode}{1}");

            List<char> c = max.ToString().ToCharArray().ToList();

            List<char> p_code = parentCode.ToString().ToCharArray().ToList();

            c.RemoveRange(0, p_code.Count);

            int childCode = Convert.ToInt32(new string(c.ToArray()));  //split last digit from number

            childCode = childCode + 1;

            return Convert.ToInt32($"{parentCode}{childCode}");
        }
        private void ExpandTreeView(object parameter)
        {
            try
            {
                TreeViewItem item = (TreeViewItem)((parameter as RoutedEventArgs).OriginalSource);
                item.Items.Clear();
                List<Tuple<string, int, int>> accountsName = new List<Tuple<string, int, int>>();
                accountsModel.GetAccountsFromAccountTable(accountsName, int.Parse(item.Tag.ToString()));
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
