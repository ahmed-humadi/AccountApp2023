using DataAccessLib;
using DataEntitiesLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class StoreBL
    {
        private DataAccess _dataAccess;
        private readonly object balanceLock = new object();
        public DataAccess DataAccess
        {
            get
            {
                lock (balanceLock)
                {
                    if (_dataAccess is null)
                        return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                    return _dataAccess;
                }
            }
            set
            {
                lock (balanceLock)
                {
                    _dataAccess = value;
                }
            }
        }
        public List<Tuple<int, string>> GetStores1(string name)
        {
            DataAccess.ExecuteQueryCommand($"Select TOP 100 ID, Name From Store Where Name LIKE '%{name}%'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<Tuple<int, string>> list = new List<Tuple<int, string>>();

            Tuple<int, string> tuple = null;

            while (dataReader.Read())
            {
                tuple = new Tuple<int, string>((int)dataReader[0],
                    (string)dataReader[1]);
                list.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        public List<Tuple<int, string>> GetStores1()
        {
            DataAccess.ExecuteQueryCommand($"Select Top 100 ID, Name From Store", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<Tuple<int, string>> list = new List<Tuple<int, string>>();
            Tuple<int, string> tuple = null;
            while (dataReader.Read())
            {
                tuple = new Tuple<int, string>((int)dataReader[0],
                    (string)dataReader[1]);
                list.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        /// <summary>
        /// get all categories
        /// </summary>
        /// <returns></returns>
        public StoreTable GetStores()
        {

            StoreTable storeTable = new StoreTable();
            DataAccess.ExecuteQueryCommand($"Select * From Store", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                StoreRow categoryRow = storeTable.NewStoreRow();
                categoryRow["ID"] = (int)dataReader[0];
                categoryRow["Name"] = (string)dataReader[1];
                storeTable.AddStoreRow(categoryRow);

            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            storeTable.AcceptChanges();
            return storeTable;
        }
        public void UpdateStoreTable(StoreTable storeTable)
        {
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in storeTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[storeTable.Columns.Count - 1];

                    sqlParameters[0] = new SqlParameter($"@{storeTable.Columns["Name"].ColumnName}", dataRow[storeTable.Columns["Name"].ColumnName]);

                    DataAccess.ExecuteNonQueryCommand("spInsertStore", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[storeTable.Columns.Count];

                    for (int i = 0; i < storeTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = storeTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spUpdateStore", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
            }
            DataAccess.CloseSqlConnection();
        }
    }
}
