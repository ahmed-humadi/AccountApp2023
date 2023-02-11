using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLib;
using DataEntitiesLib;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BusinessLib

{
    public class EndAccountsBL: IDisposable
    {
        private DataAccess _dataAccess;
        private DataAccess DataAccess
        {
            get
            { 
                if(_dataAccess is null)
                    return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                return _dataAccess;
            }
        }
        public void GetAccounts(List<Tuple<string, int>> accountsList)
        {
            accountsList.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From EndAccount", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            try
            {
                while (dataReader.Read())
                {

                    Tuple<string, int> tuple = new Tuple<string, int>(dataReader[1].ToString()
                        , (int)dataReader[0]);

                    accountsList.Add(tuple);
                }
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public void FillEndAccountsTable<T>(EndAccountsTable endAccountsTable)
            where T : DataTable
        {
            endAccountsTable.Clear();
            DataAccess.ExecuteQueryCommand($"Select * From EndAccounts", CommandType.Text);
            DataAccess.OpenSqlConnection();
            endAccountsTable.Load(DataAccess.DataReader());
            endAccountsTable.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
        }
        public void UpdateEndAccountsTable(EndAccountsTable endAccountsTable)
        {
            SqlParameter[] sqlParameters = new SqlParameter[endAccountsTable.Columns.Count];
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in endAccountsTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    for (int i = 0; i < endAccountsTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = endAccountsTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spInsertNewEndAccount", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    for (int i = 0; i < endAccountsTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = endAccountsTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spUpdateEndAccount", CommandType.StoredProcedure, sqlParameters);
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
