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
            table.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="selector">1:(name); 2:(date)</param>
        /// <returns></returns>
        public ItemTable GetItems(string searchToken,int selector)
        {
            ItemTable table = new ItemTable();
            string queryString = string.Empty;
            if (selector == 1)
                queryString = $"" +
                "SELECT        dbo.Item.ID, dbo.Item.Code, dbo.Item.Name, dbo.Item.Note, dbo.Item.Balance, " +
                               "dbo.Item.Unit,  dbo.Item.ModDate, dbo.Item.State, dbo.Item.EnglishName, dbo.Item.Brand, dbo.Item.BarCode, " +
                               "dbo.Category.Name AS CategoryName, dbo.Item.GroupID, dbo.Store.Name AS StoreName, dbo.Item.StoreID " +
                "FROM          dbo.Item INNER JOIN dbo.Category ON dbo.Item.GroupID = dbo.Category.ID INNER JOIN dbo.Store ON dbo.Item.StoreID = dbo.Store.ID " +
                              
               $"WHERE         (dbo.Item.Name = '{searchToken}')";
            else if (selector == 2)
                queryString = $"" +
                "SELECT         dbo.Item.ID, dbo.Item.Code, dbo.Item.Name, dbo.Item.Note, dbo.Item.Balance, dbo.Item.Unit, dbo.Item.ModDate, dbo.Item.State, dbo.Item.EnglishName, dbo.Item.Brand, dbo.Item.BarCode, dbo.Category.Name AS CategoryName,dbo.Item.GroupID AS CategoryID " +
                "FROM            dbo.Item INNER JOIN dbo.Category ON dbo.Item.GroupID = dbo.Category.ID " +
                $"WHERE         (dbo.Item.ModDate = '{searchToken}')";
            else
                throw new ArgumentOutOfRangeException(nameof(selector), "عملية البحث غير متوفرة");

            if (String.IsNullOrEmpty(queryString))
                throw new ArgumentNullException();


            DataAccess.ExecuteQueryCommand(queryString, CommandType.Text);

            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ItemRow itemRow = table.NewItemRow();
                itemRow["ID"] = (int)dataReader[0];
                itemRow["Code"] = (int)dataReader[1];

                itemRow["Name"] = (string)dataReader[2];

                if (!dataReader.IsDBNull(3))
                    itemRow["Note"] = (string)dataReader[3];
                if (!dataReader.IsDBNull(4))
                    itemRow["Balance"] = (double)dataReader[4];
                if (!dataReader.IsDBNull(5))
                    itemRow["Unit"] = (string)dataReader[5];
                if (!dataReader.IsDBNull(6))
                    itemRow["ModDate"] = (string)dataReader[6];
                if (!dataReader.IsDBNull(7))
                    itemRow["State"] = (string)dataReader[7];
                if (!dataReader.IsDBNull(8))
                    itemRow["EnglishName"] = (string)dataReader[8];
                if (!dataReader.IsDBNull(9))
                    itemRow["Brand"] = (string)dataReader[9];
                if (!dataReader.IsDBNull(10))
                    itemRow["BarCode"] = (string)dataReader[10];

                itemRow["GroupName"] = (string)dataReader[11];

                itemRow["GroupID"] = (int)dataReader[12];

                itemRow["StoreName"] = (string)dataReader[13];

                itemRow["StoreID"] = (int)dataReader[14];

                table.AddItemRow(itemRow);
            }
            table.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return table;
        }
        public ItemTable GetItems(string name)
        {
            ItemTable table = new ItemTable();
            DataAccess.ExecuteQueryCommand($"" +
                "SELECT         dbo.Item.ID, dbo.Item.Code, dbo.Item.Name, dbo.Item.Note, dbo.Item.Balance, dbo.Item.Unit, dbo.Item.ModDate, dbo.Item.State, dbo.Item.EnglishName, dbo.Item.Brand, dbo.Item.BarCode, dbo.Category.Name AS CategoryName,dbo.Item.GroupID AS CategoryID " +
                "FROM            dbo.Item INNER JOIN dbo.Category ON dbo.Item.GroupID = dbo.Category.ID "+
                $"WHERE         (dbo.Item.Name = '{name}') "
                ,CommandType.Text);

            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ItemRow itemRow = table.NewItemRow();
                itemRow["ID"] = (int)dataReader[0];
                itemRow["Code"] = (int)dataReader[1];

                itemRow["Name"] = (string)dataReader[2];

                if (!dataReader.IsDBNull(3))
                    itemRow["Note"] = (string)dataReader[3];
                if (!dataReader.IsDBNull(4))
                    itemRow["Balance"] = (double)dataReader[4];
                if (!dataReader.IsDBNull(5))
                    itemRow["Unit"] = (string)dataReader[5];
                if (!dataReader.IsDBNull(6))
                    itemRow["ModDate"] = (string)dataReader[6];
                if (!dataReader.IsDBNull(7))
                    itemRow["State"] = (string)dataReader[7];
                if (!dataReader.IsDBNull(8))
                    itemRow["EnglishName"] = (string)dataReader[8];
                if (!dataReader.IsDBNull(9))
                    itemRow["Brand"] = (string)dataReader[9];
                if (!dataReader.IsDBNull(10))
                    itemRow["BarCode"] = (string)dataReader[10];

                itemRow["GroupName"] = (string)dataReader[11];

                itemRow["GroupID"] = (int)dataReader[12];
                table.AddItemRow(itemRow);
            }
            table.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return table;
        }
        public ItemTable GetItems(int code)
        {
            ItemTable table = new ItemTable();
            DataAccess.ExecuteQueryCommand($"" +
                $"SELECT         dbo.Item.ID, dbo.Item.Code, dbo.Item.Name, dbo.Item.Note, dbo.Item.Balance, dbo.Item.Unit, dbo.Item.ModDate, dbo.Item.State, dbo.Item.EnglishName, dbo.Item.Brand, dbo.Item.BarCode, dbo.Category.Name AS CategoryName,dbo.Item.GroupID AS CategoryID " +
                "FROM            dbo.Item INNER JOIN dbo.Category ON dbo.Item.GroupID = dbo.Category.ID "+
                $"WHERE         (dbo.Item.Code = '{code}')"
                , CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ItemRow itemRow = table.NewItemRow();
                itemRow["ID"] = (int)dataReader[0];
                itemRow["Code"] = (int)dataReader[1];

                itemRow["Name"] = (string)dataReader[2];

                if (!dataReader.IsDBNull(3))
                    itemRow["Note"] = (string)dataReader[3];
                if (!dataReader.IsDBNull(4))
                    itemRow["Balance"] = (double)dataReader[4];
                if (!dataReader.IsDBNull(5))
                    itemRow["Unit"] = (string)dataReader[5];
                if (!dataReader.IsDBNull(6))
                    itemRow["ModDate"] = (string)dataReader[6];
                if (!dataReader.IsDBNull(7))
                    itemRow["State"] = (string)dataReader[7];
                if (!dataReader.IsDBNull(8))
                    itemRow["EnglishName"] = (string)dataReader[8];
                if (!dataReader.IsDBNull(9))
                    itemRow["Brand"] = (string)dataReader[9];
                if (!dataReader.IsDBNull(10))
                    itemRow["BarCode"] = (string)dataReader[10];

                itemRow["GroupName"] = (string)dataReader[11];

                itemRow["GroupID"] = (int)dataReader[12];
                table.AddItemRow(itemRow);
            }
            table.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return table;
        }
        public List<Tuple<int, string>> GetItemsForTreeView(int categoriesID)
        {

            DataAccess.ExecuteQueryCommand($"Select ID, Name From Item Where GroupID ='{categoriesID}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<Tuple<int, string>> tuples = new List<Tuple<int, string>>();

            while (dataReader.Read())
            {
                Tuple<int, string> tuple = new Tuple<int, string>(
                     (int)dataReader[0],
                     (string)dataReader[1]
                    );
                tuples.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return tuples;
        }
        public ItemTable GetItem(int id)
        {
            ItemTable table = new ItemTable();
            string queryString = string.Empty;
            queryString = $"" +
            "SELECT        dbo.Item.* " +
            "FROM          dbo.Item " +
           $"WHERE         (dbo.Item.ID = '{id}')";

            DataAccess.ExecuteQueryCommand(queryString, CommandType.Text);

            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ItemRow itemRow = table.NewItemRow();
                itemRow["ID"] = (int)dataReader[0];
                itemRow["Code"] = (int)dataReader[1];

                itemRow["Name"] = (string)dataReader[2];

                itemRow["GroupID"] = (int)dataReader[3];

                if (!dataReader.IsDBNull(4))
                    itemRow["Note"] = (string)dataReader[4];
                if (!dataReader.IsDBNull(5))
                    itemRow["Balance"] = (double)dataReader[5];
                if (!dataReader.IsDBNull(6))
                    itemRow["BarCode"] = (string)dataReader[6];
                if (!dataReader.IsDBNull(7))
                    itemRow["Unit"] = (string)dataReader[7];
                if (!dataReader.IsDBNull(6))
                    itemRow["ModDate"] = (string)dataReader[8];
                if (!dataReader.IsDBNull(9))
                    itemRow["Brand"] = (string)dataReader[9];
                if (!dataReader.IsDBNull(10))
                    itemRow["State"] = (string)dataReader[10];
                if (!dataReader.IsDBNull(11))
                    itemRow["EnglishName"] = (string)dataReader[11];

                itemRow["StoreID"] = (int)dataReader[12];

                table.AddItemRow(itemRow);
            }
            table.AcceptChanges();
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
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
        public void AddItemBalance(int id, double balance)
        {
            DataAccess.OpenSqlConnection();
            SqlParameter[] sqlParameters = new SqlParameter[2]; // not GroupName
            sqlParameters[0] = new SqlParameter($"@id", id);
            sqlParameters[1] = new SqlParameter($"@balance", balance);
            DataAccess.ExecuteNonQueryCommand("spAddItemBalance", CommandType.StoredProcedure, sqlParameters);
            DataAccess.ExecuteNonQuery();
        }
        public void SubstractItemBalance(int id, double balance)
        {
            DataAccess.OpenSqlConnection();
            SqlParameter[] sqlParameters = new SqlParameter[2]; // not GroupName
            sqlParameters[0] = new SqlParameter($"@id", id);
            sqlParameters[1] = new SqlParameter($"@balance", balance);
            DataAccess.ExecuteNonQueryCommand("spSubstractItemBalance", CommandType.StoredProcedure, sqlParameters);
            DataAccess.ExecuteNonQuery();
        }

        /// <summary>
        /// Inventory == Store
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public void AssignItmeInvetory(int id, int inventory)
        {
            DataAccess.OpenSqlConnection();
            SqlParameter[] sqlParameters = new SqlParameter[2]; // not GroupName
            sqlParameters[0] = new SqlParameter($"@id", id);
            sqlParameters[1] = new SqlParameter($"@StoreID", inventory);
            DataAccess.ExecuteNonQueryCommand("spAssignItemStore", CommandType.StoredProcedure, sqlParameters);
            DataAccess.ExecuteNonQuery();
        }
    }
}
