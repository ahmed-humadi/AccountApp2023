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
    public class CashDayBL
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
        /// <summary>
        /// get day1 
        /// </summary>
        /// <param name="id">day1 id</param>
        /// <returns></returns>
        public Tuple<int, int, string, int, string, DateTime> GetCashDay(int number, int parentID)
        {
            DataAccess.ExecuteQueryCommand("" +
                "SELECT        dbo.CashDay.ID, dbo.Account.ID AS AccountID, dbo.Account.Name AS AccountName, dbo.Account.Code AS AccountCode, dbo.CashDay.Note, dbo.CashDay.Date " +
                "FROM          dbo.Account INNER JOIN dbo.CashDay ON dbo.Account.ID = dbo.CashDay.AccountID " +
                $"WHERE        (dbo.CashDay.ParentID = '{parentID}') AND (dbo.CashDay.Number = '{number}')", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            Tuple<int, int, string, int, string, DateTime> tuple = null;
            while (dataReader.Read())
            {
                int id = (int)dataReader[0];
                int accountID = (int)dataReader[1];
                string accountName = (string)dataReader[2];
                int accountCode = (int)dataReader[3];
                string note = string.Empty;
                if (!dataReader.IsDBNull(4))
                    note = (string)dataReader[4];
                DateTime date = (DateTime)dataReader[5];
                tuple = new Tuple<int, int, string, int, string, DateTime>
                    (
                        id, accountID, accountName, accountCode, note,date
                    );
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return tuple;
        }
        public int GetCashDayID(int number, int parentID)
        {
            int ID = 0;
            DataAccess.ExecuteQueryCommand("SELECT dbo.CashDay.ID FROM dbo.CashDay " +
                                            $"WHERE dbo.CashDay.ParentID = '{parentID}' AND dbo.CashDay.Number = '{number}'", CommandType.Text);
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
        public int GetMax1CashDayNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(dbo.CashDay.Number) + 1, 1) FROM dbo.CashDay " +
                                            $"WHERE dbo.CashDay.ParentID = '{parentID}'", CommandType.Text);
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
        public int GetMaxCashDayNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(dbo.CashDay.Number), 1) FROM dbo.CashDay " +
                                            $"WHERE dbo.CashDay.ParentID = '{parentID}'", CommandType.Text);
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
        public int GetMinCashDayNumber(int parentID)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MIN(dbo.CashDay.Number), 1) FROM dbo.CashDay " +
                                            $"WHERE dbo.CashDay.ParentID = '{parentID}'", CommandType.Text);
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
        public void InsertCashDay(Tuple<int, string, int, int, DateTime> parametersTuple)
        {
            int sqlparameters = 5;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@AccountID", (int)parametersTuple.Item1);
            sqlParameters[1] = new SqlParameter("@Note", (string)parametersTuple.Item2);
            sqlParameters[2] = new SqlParameter("@ParentID", (int)parametersTuple.Item3);
            sqlParameters[3] = new SqlParameter("@Number", (int)parametersTuple.Item4);
            sqlParameters[4] = new SqlParameter("@Date", (DateTime)parametersTuple.Item5);
            DataAccess.ExecuteNonQueryCommand("spInsertCashDay", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        public void UpdateCashDay(Tuple<int, string, int, int, DateTime> parametersTuple, int casdDayID)
        {

            int sqlparameters = 6;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", casdDayID);
            sqlParameters[1] = new SqlParameter("@AccountID", (int)parametersTuple.Item1);
            sqlParameters[2] = new SqlParameter("@Note", (string)parametersTuple.Item2);
            sqlParameters[3] = new SqlParameter("@ParentID", (int)parametersTuple.Item3);
            sqlParameters[4] = new SqlParameter("@Number", (int)parametersTuple.Item4);
            sqlParameters[5] = new SqlParameter("@Date", (DateTime)parametersTuple.Item5);
            DataAccess.ExecuteNonQueryCommand("spUpdateCashDay", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
    }
}
