using DataAccessLib;
using DataEntitiesLib;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class MainAccountsBL
    {
        private DataAccess _dataAccess;
        private DataAccess DataAccess
        {
            get
            {
                if (_dataAccess is null)
                    return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                return _dataAccess;
            }
        }
        public void FillEndAccountsList_ByName<T>(ObservableCollection<T> endAccountsList, string endAccountName)
            where T : class, new()
        {
            if (!string.IsNullOrWhiteSpace(endAccountName))
            {
                endAccountsList.Clear();
                string commandString = $@"Select * 
                                        From EndAccounts
                                            Where EndAccountName Like  '{endAccountName}%'";
                DataAccess.ExecuteQueryCommand(commandString, CommandType.Text);
                DataAccess.OpenSqlConnection();
                IDataReader dataReader = DataAccess.DataReader();
                while (dataReader.Read())
                {
                    T obj = new T();
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        property.SetValue(obj, dataReader[property.Name]);
                    }
                    endAccountsList.Add(obj);
                }
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
        }
        public void FillMainAccountsTable(AccountTable accountsTable)
        {
            accountsTable.Clear();
            DataAccess.ExecuteQueryCommand("Select * From Accounts Where ParentID = '00000000-0000-0000-0000-000000000000'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            accountsTable.Load(DataAccess.DataReader());
            accountsTable.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        public void UpdateMainAccountsTable(AccountTable mainAccountsTable)
        {
            SqlParameter[] sqlParameters = new SqlParameter[mainAccountsTable.Columns.Count];
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in mainAccountsTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    for (int i = 0; i < mainAccountsTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = mainAccountsTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spInsertAccount", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    for (int i = 0; i < mainAccountsTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = mainAccountsTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spUpdateAccountsTable", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
            }
            DataAccess.CloseSqlConnection();
        }
        public void Dispose()
        {
            if (!(_dataAccess is null))
                DataAccess.Dispose();
            _dataAccess = null;
        }
    }
}
