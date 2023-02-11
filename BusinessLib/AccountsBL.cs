using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DataAccessLib;
using DataEntitiesLib;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BusinessLib
{
    public class AccountsBL: IDisposable
    {
        private DataAccess _dataAccess;
        public DataAccess DataAccess
        {
            get 
            {
                if (_dataAccess is null)
                    return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                return _dataAccess;
            }
            set { _dataAccess = value; }
        }
        [Obsolete]
        public void FillAccountsList_ByName<T>(List<T> list, string tableName,string AccountName)
            where T : class, new()
        {
            list.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From {tableName} Where EndAccountName = '{AccountName}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            while (dataReader.Read())
            {
                T obj = new T();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    property.SetValue(obj, dataReader[property.Name]);
                }
                list.Add(obj);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        [Obsolete]
        public void FillAccountsList_ByParentID<T>(List<T> list, string tableName, string accountName)
            where T : class, new()
        {
            list.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From {tableName} Where EndAccountName = '{accountName}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            while (dataReader.Read())
            {
                T obj = new T();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    property.SetValue(obj, dataReader[property.Name]);
                }
                list.Add(obj);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        [Obsolete]
        public void GetAllAccounts_ByParentID(List<string> list, Guid parentid)
        {
            list.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From Accounts Where ParentID = '{parentid}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            try
            {
                while (dataReader.Read())
                {

                    list.Add(dataReader[1].ToString());
                }
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        [Obsolete]
        public void GetAccounts_ByParentID(List<string> list, int parentid)
        {
            list.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From Account Where ParentID = '{parentid}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            try
            {
                while (dataReader.Read())
                {

                    list.Add(dataReader[1].ToString());
                }
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        // 
        /// <summary>
        /// this will be used if we want to get the name and id of an account
        /// </summary>
        /// <param name="AllAccounts"></param>
        public void GetAccounts(List<Tuple<string, int>> AllAccounts)
        {
            AllAccounts.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From Account", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                Tuple<string, int> tuple = new Tuple<string, int>(dataReader[1].ToString()
                    , (int)dataReader[0]);

                AllAccounts.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="list"></param>
       /// <param name="parentid"></param>
        public void GetAccounts(List<Tuple<string, int>> list, int parentid)
        {
            list.Clear();
            

            DataAccess.ExecuteQueryCommand($"Select * From Account Where ParentID = '{parentid}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                Tuple<string, int> tuple = new Tuple<string, int>(dataReader[1].ToString()
                    , (int)dataReader[0]);

                list.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
        /// <summary>
        /// return list of Tuple 
        /// Tuple items :
        /// item1 : name, item2 : ID, item : code
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentid"></param>
        public void GetAccounts(List<Tuple<string, int, int>> list, int parentid)
        {
            list.Clear();


            DataAccess.ExecuteQueryCommand($"Select * From Account Where ParentID = '{parentid}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                Tuple<string, int, int> tuple = new Tuple<string, int, int>(dataReader[1].ToString()
                    , (int)dataReader[0], (int)dataReader[3]);

                list.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="parentid"></param>
        public void GetAccounts(AccountTable accountTable, int parentid)
        {

            accountTable.Clear();

            DataAccess.ExecuteQueryCommand($"Select * From Account Where ParentID = '{parentid}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                AccountRow accountRow = accountTable.NewAccountRow();
                accountRow[0] = dataReader[0];
                accountRow[1] = dataReader[1];
                accountRow[2] = dataReader[2];
                accountRow[3] = dataReader[3];
                accountRow[4] = dataReader[4];
                accountTable.AddAccountsRow(accountRow);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        [Obsolete]
        public void GetAccounts(List<Tuple<string, int>> list, string name)
        {
            list.Clear();


            DataAccess.ExecuteQueryCommand($"Select * From Account Where Name = '{name}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            try
            {
                while (dataReader.Read())
                {
                    Tuple<string, int> tuple = new Tuple<string, int>(dataReader[1].ToString()
                        , (int)dataReader[0]);

                    list.Add(tuple);
                }
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        public void GetAccounts(DataTable table, string name)
        {
            table.Clear();
            DataAccess.ExecuteQueryCommand($"" +
              $"SELECT TOP 10  Account.Name AS Expr1, Account_1.ID, Account.Code, Account_1.Name, Account.ID AS Expr4, EndAccount.Name AS Expr2, EndAccount.ID AS Expr3 " +
              $"FROM           Account INNER JOIN Account AS Account_1 ON Account.ParentID = Account_1.ID INNER JOIN EndAccount ON Account.EndAccountID = EndAccount.ID " +
              $"Where          (Account.Name LIKE '{name}%')", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            table.Load(dataReader);           

            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="id"></param>
        public void GetAccounts_ByID(ref Tuple<string, int> tuple, int id)
        {
            
            DataAccess.ExecuteQueryCommand($"Select * From Account Where ID = '{id}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
               tuple = new Tuple<string, int>(dataReader[1].ToString()
                    , (int)dataReader[3]);

            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
        /// <summary>
        /// take table of type AccountTable
        /// </summary>
        /// <param name="accountTable">
        /// column1 : account id,               column2 : account name,  
        /// column3 : account parent id,        column4 : account code,   
        /// column5 : account endAccount id,     
        /// </param>
        /// <param name="id">account id</param>
        public void GetAccounts_ByID(AccountTable accountTable, int id)
        {
            accountTable.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From Account Where ID = '{id}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                AccountRow accountRow = accountTable.NewAccountRow();
                accountRow[0] = dataReader[0];
                accountRow[1] = dataReader[1];
                accountRow[2] = dataReader[2];
                accountRow[3] = dataReader[3];
                accountRow[4] = dataReader[4];
                accountTable.AddAccountsRow(accountRow);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

        }
        /// <summary>
        /// DataTable will be use to quiry three table in the same time
        /// Instead of create custom table as (AccountTble)
        /// DataTable will be use
        /// In this method the query command gets the parent name instesd of the parent id
        /// The where statement is with account id 
        /// </summary>
        /// <param name="dataTable">
        /// column1 : account name,               column2 : account parent id,  
        /// column3 : account code,               column4 : account parent name,   
        /// column5 : account endAccount name,    column6 : account endAccount id, 
        /// column7 : account id
        /// </param>
        /// <param name="id">account id</param>
        public void GetAccounts_ByID(DataTable dataTable, int id)
        {
            dataTable.Clear();
            DataAccess.ExecuteQueryCommand($"" +
              $"SELECT  Account.Name AS Expr1, Account_1.ID, Account.Code, Account_1.Name, Account.ID AS Expr4, EndAccount.Name AS Expr2, EndAccount.ID AS Expr3 " +
              $"FROM    Account INNER JOIN Account AS Account_1 ON Account.ParentID = Account_1.ID INNER JOIN EndAccount ON Account.EndAccountID = EndAccount.ID " +
              $"Where   (Account.ID = '{id}')", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            dataTable.Load(dataReader);
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="id"></param>
        public void GetEndAccount_ByID(ref Tuple<string, int> tuple, int id)
        {
          
            DataAccess.ExecuteQueryCommand($"" +
              $"SELECT        dbo.EndAccount.ID, dbo.EndAccount.Name " +
              $"FROM          dbo.EndAccount " +
              $"Where   (dbo.EndAccount.ID = '{id}')", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            while (dataReader.Read())
            {
                tuple = new Tuple<string, int>(dataReader[1].ToString()
                     , (int)dataReader[0]);

            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// This method takes AcccountTable and update the datebase
        /// or insert according to the table rows state
        /// </summary>
        /// <param name="accountsTable"></param>
        public void UpdateAccountTable(AccountTable accountsTable)
        {
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in accountsTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[accountsTable.Columns.Count - 1];

                    sqlParameters[0] = new SqlParameter($"@{accountsTable.Columns[1].ColumnName}", dataRow[accountsTable.Columns[1].ColumnName]);
                    sqlParameters[1] = new SqlParameter($"@{accountsTable.Columns[2].ColumnName}", dataRow[accountsTable.Columns[2].ColumnName]);
                    sqlParameters[2] = new SqlParameter($"@{accountsTable.Columns[3].ColumnName}", dataRow[accountsTable.Columns[3].ColumnName]);
                    sqlParameters[3] = new SqlParameter($"@{accountsTable.Columns[4].ColumnName}", dataRow[accountsTable.Columns[4].ColumnName]);
                    
                    DataAccess.ExecuteNonQueryCommand("spInsertAccount", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[accountsTable.Columns.Count ];

                    for (int i = 0; i < accountsTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = accountsTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spUpdateAccountTable", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
            }
            DataAccess.CloseSqlConnection();
        }
        //
        public void Dispose()
        {
            if (!(_dataAccess is null))
                DataAccess.Dispose();
            DataAccess = null;
            _dataAccess = null;
        }
    }
}
