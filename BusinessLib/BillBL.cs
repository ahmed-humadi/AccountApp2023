using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class BillBL
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

        public int GetBill1ID(int number, int parentID)
        {
            int ID = 0;
            DataAccess.ExecuteQueryCommand("SELECT dbo.Bill1.ID FROM dbo.Bill1 " +
                                            $"WHERE dbo.Bill1.ParentID = '{parentID}' AND dbo.Bill1.Number = '{number}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                ID = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return ID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMax1BillNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(dbo.Bill1.Number) + 1, 1) FROM dbo.Bill1 " +
                                            $"WHERE dbo.Bill1.ParentID = '{parentID}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                number = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return number;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxBillNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(dbo.Bill1.Number), 1) FROM dbo.Bill1 " +
                                            $"WHERE dbo.Bill1.ParentID = '{parentID}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                number = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return number;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMinBillNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MIN(dbo.Bill1.Number), 1) FROM dbo.Bill1 " +
                                            $"WHERE dbo.Bill1.ParentID = '{parentID}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                number = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return number;
        }
        /// <summary>
        /// view_2 in database get day2 by day1 id
        /// </summary>
        /// <param name="id"> day1 id </param>
        /// <returns></returns>
        public List<Tuple<int, double, double, string, int, string, string, Tuple<double>>> GetBill2(int Bill1id)
        {
            DataAccess.ExecuteQueryCommand("" +
                "SELECT        dbo.Bill2.ID, dbo.Bill2.Quantity, dbo.Bill2.Price, dbo.Bill2.Note, dbo.Item.ID AS ItemID, dbo.Item.Name, dbo.Item.BarCode, dbo.Bill2.Quantity * dbo.Bill2.Price AS Total " +
                "FROM            dbo.Bill2 INNER JOIN dbo.Item ON dbo.Bill2.ItemID = dbo.Item.ID " +
               $"WHERE(dbo.Bill2.ParentID  = {Bill1id})", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            List<Tuple<int, double, double, string, int, string, string, Tuple<double>>> list = new List<Tuple<int, double, double, string, int, string, string, Tuple<double>>>();
            while (dataReader.Read())
            {
                int id = (int)dataReader[0];
                double qua = (double)dataReader[1];
                double price = (double)dataReader[2];
                string note = string.Empty;
                if (!dataReader.IsDBNull(3))
                    note = (string)dataReader[3];
                int itemID = (int)dataReader[4];
                string itemName = (string)dataReader[5];
                string itemBarCode = (string)dataReader[6];
                double total = (double)dataReader[7];
                Tuple<int, double, double, string, int, string, string, Tuple<double>> tuple = new Tuple<int, double, double, string, int, string, string, Tuple<double>>
                    (
                        id,qua,price,note,itemID,itemName,itemBarCode,new Tuple<double>(total)
                    );
                list.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        /// <summary>
        /// get day1 
        /// </summary>
        /// <param name="id">day1 id</param>
        /// <returns></returns>
        public Tuple<int, string, string, double, string,int, string, Tuple <int, string> > GetBill1(int number, int parentID)
        {
            DataAccess.ExecuteQueryCommand("" +
                "SELECT        dbo.Bill1.ID, dbo.Bill1.Note, dbo.Bill1.date, dbo.Bill1.Discount, dbo.Bill1.PaymentType, dbo.Account.ID AS AccountID, dbo.Account.Name AS AccountName, dbo.Store.ID AS StoreID, dbo.Store.Name AS StoreName " +
                "FROM          dbo.Bill1 INNER JOIN dbo.Account ON dbo.Bill1.CustomerID = dbo.Account.ID INNER JOIN dbo.Store ON dbo.Bill1.StoreID = dbo.Store.ID " +
                $"WHERE       (dbo.Bill1.Number = '{number}') AND(dbo.Bill1.ParentID = '{parentID}')" , CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            Tuple<int, string, string, double, string, int, string, Tuple<int, string>> tuple = null;
            while (dataReader.Read())
            {
                int id = (int)dataReader[0];
                string note = string.Empty;
                if (!dataReader.IsDBNull(1))
                    note = (string)dataReader[1];
                string date = (string)dataReader[2];
                double discount = (double)dataReader[3];
                string paymentType = (string)dataReader[4];
                int accountID = (int)dataReader[5];
                string accountName = (string)dataReader[6];
                int storeID = (int)dataReader[7];
                string storeName = (string)dataReader[8];
                tuple = new Tuple<int, string, string, double, string, int, string, Tuple<int, string>>
                    (
                        id,note,date,discount,paymentType,accountID,accountName, new Tuple<int, string>(storeID,storeName)
                    );
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return tuple;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertBill1(Tuple<int, string, int, int,string, int, double, Tuple<string>> parametersTuple)
        {
            int sqlparameters = 8;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@ParentID", (int)parametersTuple.Item1);
            sqlParameters[1] = new SqlParameter("@Note", (string)parametersTuple.Item2);
            sqlParameters[2] = new SqlParameter("@CustomerID", (int)parametersTuple.Item3);
            sqlParameters[3] = new SqlParameter("@StoreID", (int)parametersTuple.Item4);
            sqlParameters[4] = new SqlParameter("@date", (string)parametersTuple.Item5);
            sqlParameters[5] = new SqlParameter("@Number", (int)parametersTuple.Item6);
            sqlParameters[6] = new SqlParameter("@Discount", (double)parametersTuple.Item7);
            sqlParameters[7] = new SqlParameter("@PaymentType", (string)parametersTuple.Rest.Item1);
            DataAccess.ExecuteNonQueryCommand("spInsertBill1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertBill2(List<Tuple<int, int, decimal, decimal, string>> parametersList)
        {
            int sqlparameters = 5;

            DataAccess.OpenSqlConnection();

            foreach (Tuple<int, int, decimal, decimal, string> tuple in parametersList)
            {
                SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

                sqlParameters[0] = new SqlParameter("@ParentID", (int)tuple.Item1);
                sqlParameters[1] = new SqlParameter("@ItemID", (int)tuple.Item2);
                sqlParameters[2] = new SqlParameter("@Quantity", (decimal)tuple.Item3);
                sqlParameters[3] = new SqlParameter("@Price", (decimal)tuple.Item4);
                sqlParameters[4] = new SqlParameter("@Note", (string)tuple.Item5);

                DataAccess.ExecuteNonQueryCommand("spInsertBill2", CommandType.StoredProcedure, sqlParameters);

                DataAccess.ExecuteNonQuery();
            }
            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        public void DeleteBill2(int parentID)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ParentID", parentID);

            DataAccess.ExecuteNonQueryCommand("spDeleteBill2", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteBill1(int id)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", id);

            DataAccess.ExecuteNonQueryCommand("spDeleteBill1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        /// <param name="bill1ID"></param>
        public void UpdateBill1(Tuple<int, string, int, int,string, int, double, Tuple<string>> parametersTuple, int bill1ID)
        {
            
            int sqlparameters = 9;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", bill1ID);
            sqlParameters[1] = new SqlParameter("@ParentID", (int)parametersTuple.Item1);
            sqlParameters[2] = new SqlParameter("@Note", (string)parametersTuple.Item2);
            sqlParameters[3] = new SqlParameter("@CustomerID", (int)parametersTuple.Item3);
            sqlParameters[4] = new SqlParameter("@StoreID", (int)parametersTuple.Item4);
            sqlParameters[5] = new SqlParameter("@date", (string)parametersTuple.Item5);
            sqlParameters[6] = new SqlParameter("@Number", (int)parametersTuple.Item6);
            sqlParameters[7] = new SqlParameter("@Discount", (double)parametersTuple.Item7);
            sqlParameters[8] = new SqlParameter("@PaymentType", (string)parametersTuple.Rest.Item1);
            DataAccess.ExecuteNonQueryCommand("spUpdateBill1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
    }
}
