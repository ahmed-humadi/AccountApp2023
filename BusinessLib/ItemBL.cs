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
    public class ItemBL
    {
        private readonly object lockToken = new object();
        private DataAccess _dataAccess;
        public DataAccess DataAccess
        {
            get
            {
                lock (lockToken)
                {
                    if (_dataAccess is null)
                        return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                    return _dataAccess;
                }
            }
            set 
            {
                lock (lockToken)
                {
                    _dataAccess = value;
                }
            }
        }

        public Tuple<string, int> GetGroup(int id)
        {
            DataAccess.ExecuteQueryCommand($"Select Name From Item Where ID '{id}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            Tuple<string, int> tuple = null;

            while (dataReader.Read())
            {
                tuple = new Tuple<string, int>(
                    (string)dataReader[0], id);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return tuple;
        }
        public Tuple<int, string> GetGroup(string name)
        {
            DataAccess.ExecuteQueryCommand($"Select ID From Item Where ID '{name}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            Tuple<int, string> tuple = null;

            while (dataReader.Read())
            {
                tuple = new Tuple<int, string>(
                    (int)dataReader[0], name);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return tuple;
        }
        public ItemTable GetItems()
        {
            ItemTable table = new ItemTable();


            DataAccess.ExecuteQueryCommand($"Select * From Item", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ItemRow itemRow = table.NewItemRow();
                itemRow["ID"]       = (int)dataReader[0];
                itemRow["Code"]     = (int)dataReader[1];

                itemRow["Name"]     = (string)dataReader[2];
                itemRow["GroupID"]  = (int)dataReader[3];
                itemRow["Note"]     = (string)dataReader[4];
                itemRow["Balance"]  = (double)dataReader[5];
                itemRow["BarCode"]  = (string)dataReader[6];
                itemRow["Unit"]     = (int)dataReader[7];
                itemRow["ModDate"]  = (DateTime)dataReader[8];
                table.AddItemRow(itemRow);

            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            table.AcceptChanges();
            return table;
        }
        public void UpdateItemTable(ItemTable itemTable)
        {
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in itemTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[itemTable.Columns.Count - 1];

                    sqlParameters[0] = new SqlParameter($"@{itemTable.Columns["Name"].ColumnName}", dataRow[itemTable.Columns["Name"].ColumnName]);
                    sqlParameters[1] = new SqlParameter($"@{itemTable.Columns["Code"].ColumnName}", dataRow[itemTable.Columns["Code"].ColumnName]);
                    sqlParameters[2] = new SqlParameter($"@{itemTable.Columns["GroupID"].ColumnName}", dataRow[itemTable.Columns["GroupID"].ColumnName]);
                    sqlParameters[3] = new SqlParameter($"@{itemTable.Columns["Note"].ColumnName}", dataRow[itemTable.Columns["Note"].ColumnName]);
                    sqlParameters[4] = new SqlParameter($"@{itemTable.Columns["Balance"].ColumnName}", dataRow[itemTable.Columns["Balance"].ColumnName]);
                    sqlParameters[5] = new SqlParameter($"@{itemTable.Columns["BarCode"].ColumnName}", dataRow[itemTable.Columns["BarCode"].ColumnName]);
                    sqlParameters[6] = new SqlParameter($"@{itemTable.Columns["Unit"].ColumnName}", dataRow[itemTable.Columns["Unit"].ColumnName]);
                    sqlParameters[7] = new SqlParameter($"@{itemTable.Columns["ModDate"].ColumnName}", dataRow[itemTable.Columns["ModDate"].ColumnName]);

                    DataAccess.ExecuteNonQueryCommand("spInsertItem", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                //if (dataRow.RowState == DataRowState.Modified)
                //{
                //    SqlParameter[] sqlParameters = new SqlParameter[itemTable.Columns.Count];

                //    for (int i = 0; i < itemTable.Columns.Count; i++)
                //    {
                //        DataColumn dataColumn = itemTable.Columns[i];
                //        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                //    }
                //    DataAccess.ExecuteNonQueryCommand("spUpdateCategory", CommandType.StoredProcedure, sqlParameters);
                //    DataAccess.ExecuteNonQuery();
                //}
            }
            DataAccess.CloseSqlConnection();
        }
    }
}
