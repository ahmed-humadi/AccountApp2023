using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BusinessLib;

using DataEntitiesLib;
using System.Data;

namespace ModelsLib
{
    public class AccountTableModel
    {
        private AccountsBL accountsGuideBL;
        public AccountTableModel()
        {
            accountsGuideBL = new AccountsBL();
        }
        /// <summary>
        ///  this method take a list and load it with acount 
        ///  name and id
        ///  this quiry is done base on the parent id 
        ///  and the account view model update the tree view
        /// </summary>
        /// <param name="Accountstuples"></param>
        /// <param name="parentID"></param>
        public void GetAccountsFromAccountTable(List<Tuple<string, int>> Accountstuples,int parentID)
        {
            accountsGuideBL.GetAccounts(Accountstuples, parentID);
        }
        /// <summary>
        /// return list of Tuple 
        /// Tuple items :
        /// item1 : name, item2 : ID, item : code
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentid"></param>
        public void GetAccountsFromAccountTable(List<Tuple<string, int, int>> Accountstuples, int parentID)
        {
            accountsGuideBL.GetAccounts(Accountstuples, parentID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="parentID"></param>
        public void GetAccountsFromAccountTable(AccountTable accountTable, int parentID)
        {
            accountsGuideBL.GetAccounts(accountTable, parentID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Accountstuples"></param>
        public void GetAccountsFromAccountTable(List<Tuple<string, int>> Accountstuples)
        {
            accountsGuideBL.GetAccounts(Accountstuples);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Accountstuples"></param>
        /// <param name="name"></param>
        [Obsolete]
        public void GetAccountsFromAccountTable(List<Tuple<string, int>> Accountstuples, string name)
        {
            accountsGuideBL.GetAccounts(Accountstuples, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        public void GetAccounts(DataTable table, string name)
        {
            accountsGuideBL.GetAccounts(table, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Accountstuples"></param>
        /// <param name="id"></param>
        public void GetAccountsFromAccountTable_ByID( ref Tuple<string, int> Accountstuples, int id)
        {
            accountsGuideBL.GetAccounts_ByID(ref Accountstuples, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="id"></param>
        public void GetAccountsFromAccountTable_ByID(AccountTable accountTable, int id)
        {
            accountsGuideBL.GetAccounts_ByID(accountTable, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="id"></param>
        public void GetAccountsFromAccountTable_ByID(DataTable accountTable, int id)
        {
            accountsGuideBL.GetAccounts_ByID(accountTable, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="id"></param>
        public void GetEndAccount_ByID(ref Tuple<string, int> tuple, int id)
        {
            accountsGuideBL.GetEndAccount_ByID(ref tuple, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accoutTable"></param>
        public void UpdateAccountTable(AccountTable accoutTable)
        {
            accountsGuideBL.UpdateAccountTable(accoutTable);
        }
    }
}
