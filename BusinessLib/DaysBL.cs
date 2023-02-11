using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class DaysBL
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
        public Tuple<int, int> GetDay1NumberAndID(int type)
        {
            DataAccess.ExecuteQueryCommand("SELECT dbo.Day1.Number, dbo.Day1.ID FROM dbo.Day1 " +
                                            $"WHERE dbo.Day1.Type = '{type}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            Tuple<int, int> tuple = null;
            while (dataReader.Read())
            {
                tuple = new Tuple<int, int>
                (
                    (int)dataReader[0],
                    (int)dataReader[1]
                );
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return tuple;
        }
        public int GetDay1Number(int type)
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT dbo.Day1.Number FROM dbo.Day1 " +
                                            $"WHERE dbo.Day1.Type = '{type}'", CommandType.Text);
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
        public int GetDay1ID(int number)
        {
            int ID = 0;
            DataAccess.ExecuteQueryCommand("SELECT dbo.Day1.ID FROM dbo.Day1 " +
                                            $"WHERE dbo.Day1.Number = '{number}'", CommandType.Text);
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
        /// <param name="accountCode"></param>
        /// <returns></returns>
        public List<Tuple<string, int, int>> GetAccount(int accountCode)
        {
            List<Tuple<string, int, int>> accountsList = new List<Tuple<string, int, int>>();

            DataAccess.ExecuteQueryCommand($"Select Name, Code,ID From Account Where Code Like '{accountCode}%'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                Tuple<string, int,int> tuple = new Tuple<string, int,int>(
                    (string)dataReader[0], (int)dataReader[1], (int)dataReader[2]);
                accountsList.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return accountsList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public List<Tuple<string, int, int>> GetAccounts(string accountName)
        {

            List<Tuple<string, int, int>> accountsList = new List<Tuple<string, int, int>>();

            DataAccess.ExecuteQueryCommand($"Select Name, Code, ID From Account Where Name Like '{accountName}%'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                Tuple<string, int, int> tuple = new Tuple<string, int, int>(
                    (string)dataReader[0], (int)dataReader[1], (int)dataReader[2]);
                accountsList.Add(tuple);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return accountsList;
        }
        /// <summary>
        /// retern the max Day1 ID + 1
        /// </summary>
        /// <returns></returns>
        public int GetMax_1DayNumber()
        {
            int id = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(Number) + 1, 1) FROM Day1", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                id = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxDayNumber()
        {
            int id = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(Number), 1) FROM Day1", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                id = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMinDayNumber()
        {
            int id = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MIN(Number), 1) FROM Day1", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                id = (int)dataReader[0];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return id;
        }
        /// <summary>
        /// view_2 in database get day2 by day1 id
        /// </summary>
        /// <param name="id"> day1 id </param>
        /// <returns></returns>
        public List<Tuple<double, double, int, string, int, int, string>> GetDay2(int id)
        { 
            DataAccess.ExecuteQueryCommand("" +
               "SELECT        dbo.Day2.Debit, dbo.Day2.Credit, dbo.Day1.ID, dbo.Account.Name, dbo.Account.Code, dbo.Account.ID AS Expr1, dbo.Day2.Note "+
               "FROM          dbo.Day1 INNER JOIN dbo.Day2 ON dbo.Day1.ID = dbo.Day2.ParentID INNER JOIN dbo.Account ON dbo.Day2.AccountID = dbo.Account.ID "+
               $"WHERE(dbo.Day1.ID = {id})", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();
            List<Tuple<double, double, int, string, int, int, string>> list = new List<Tuple<double, double, int, string, int, int, string>>();
            while (dataReader.Read())
            {
                Tuple<double, double, int, string, int, int, string> tuple = new Tuple<double, double, int, string, int, int, string>(
                    (double)(dataReader[0]),
                    (double)(dataReader[1]),
                    (int)dataReader[2],
                    (string)dataReader[3],
                    (int)dataReader[4],
                    (int)dataReader[5],
                    (string)dataReader[6]
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
        /// <param name="number">day1 id</param>
        /// <returns></returns>
        public Tuple<int, DateTime, string, int> GetDay1(int number)
        {
            DateTime dateTime = DateTime.Now;
            string note = string.Empty;
            int type = 0;
            int id = 0;
            DataAccess.ExecuteQueryCommand($"SELECT * FROM Day1 WHERE Number = '{number}'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                id = (int)dataReader[0];
                dateTime = (DateTime)dataReader[1];
                note = (string)dataReader[2];
                type = (int)dataReader[3];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return new Tuple<int, DateTime, string, int>(id, dateTime, note, type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertDay1(Tuple<DateTime,string,int,string, int> parametersTuple)
        {
            int sqlparameters = 5;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@Date"    , (DateTime)parametersTuple.Item1.Date);
            sqlParameters[1] = new SqlParameter("@Note"    , (string)parametersTuple.Item2);
            sqlParameters[2] = new SqlParameter("@Type"    , (int)parametersTuple.Item3);
            sqlParameters[3] = new SqlParameter("@Note_Day", (string)parametersTuple.Item4);
            sqlParameters[4] = new SqlParameter("@Number", (int)parametersTuple.Item5);
            DataAccess.ExecuteNonQueryCommand("spInsertDay1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="parametersTuple"></param>
        public void InsertDay2(List<Tuple<int, int, float, float, string>> parametersList)
        {
            int sqlparameters = 5;

            DataAccess.OpenSqlConnection();
            foreach (Tuple<int, int, float, float, string> tuple in parametersList)
            {
            
                    SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

                    sqlParameters[0] = new SqlParameter("@ParentID", (int)tuple.Item1);
                    sqlParameters[1] = new SqlParameter("@AccountID", (int)tuple.Item2);
                    sqlParameters[2] = new SqlParameter("@Debit", (float)tuple.Item3);
                    sqlParameters[3] = new SqlParameter("@Credit", (float)tuple.Item4);
                    sqlParameters[4] = new SqlParameter("@Note", (string)tuple.Item5);

                    DataAccess.ExecuteNonQueryCommand("spInsertDay2", CommandType.StoredProcedure, sqlParameters);

                    DataAccess.ExecuteNonQuery();
            }
            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        public void DeleteDay2(int parentID)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ParentID", parentID);
          
            DataAccess.ExecuteNonQueryCommand("spDeleteDay2", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// this function is used by the bill viewmodel to modify the associated day with that bill
        /// </summary>
        /// <param name="type"></param>
        public void DeleteDay1(int type)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@Type", type);

            DataAccess.ExecuteNonQueryCommand("spDeleteDay1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        /// <param name="day1ID"></param>
        public void UpdateDay1(Tuple<DateTime, string, int, string, int> parametersTuple, int day1ID)
        {
            int sqlparameters = 6;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", day1ID);
            sqlParameters[1] = new SqlParameter("@Date", (DateTime)parametersTuple.Item1.Date);
            sqlParameters[2] = new SqlParameter("@Note", (string)parametersTuple.Item2);
            sqlParameters[3] = new SqlParameter("@Type", (int)parametersTuple.Item3);
            sqlParameters[4] = new SqlParameter("@Note_Day", (string)parametersTuple.Item4);
            sqlParameters[5] = new SqlParameter("@Number", (int)parametersTuple.Item5);

            DataAccess.ExecuteNonQueryCommand("spUpdateDay1", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
    }
}
