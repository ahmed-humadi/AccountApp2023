using DataAccessLib;
using DataEntitiesLib;
using ModelLib1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLib
{
    public class CurrencyBL
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
        public int GetMaxCurrencyNumber_1()
        {
            int id = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(Number) + 1, 1) FROM Currency", CommandType.Text);
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
        public int GetMaxCurrencyNumber()
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MAX(dbo.Currency.Number), 1) FROM dbo.Currency"
                                            , CommandType.Text);
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
        public int GetMinCurrencyNumber()
        {
            int number = 0;
            DataAccess.ExecuteQueryCommand("SELECT ISNULL(MIN(dbo.Currency.Number), 1) FROM dbo.Currency", CommandType.Text);
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
        public CurrencyTable GetCurrencyTable(int value, string ValueName)
        {

            string queryString = string.Empty;
            if (ValueName.Equals("Number"))
                queryString = $"" +
                            "SELECT        dbo.Currency.* " +
                            "FROM          dbo.Currency " +
                            $"WHERE        (dbo.Currency.Number = '{value}')";
            else if (ValueName.Equals("ID"))
                queryString = $"" +
                                "SELECT        dbo.Currency.* " +
                                "FROM          dbo.Currency " +
                                $"WHERE        (dbo.Currency.ID = '{value}')";

            else
                throw new ArgumentOutOfRangeException();

            CurrencyTable table = new CurrencyTable();
            try
            {
                DataAccess.ExecuteQueryCommand(queryString, CommandType.Text);
                DataAccess.OpenSqlConnection();
                IDataReader dataReader = DataAccess.DataReader();

                while (dataReader.Read())
                {
                    CurrencyRow itemRow = table.NewCurrencyRow();
                    itemRow["ID"] = (int)dataReader[0];
                    itemRow["Name"] = (string)dataReader[1];
                    itemRow["PartName"] = (string)dataReader[2];
                    itemRow["CurrencyValue"] = (double)dataReader[3];
                    itemRow["Number"] = (int)dataReader[4];

                    table.AddCurrencyRow(itemRow);
                }
                table.AcceptChanges();
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            catch (Exception ex)
            {
                DataAccess.CloseDataReader();
                DataAccess.CloseSqlConnection();
            }
            return table;
        }
        public CurrencyModel GetCurrency(int value, string ValueName)
        {
            string queryString = string.Empty;
            if (ValueName.Equals("Number"))
                queryString = $"" +
                            "SELECT        dbo.Currency.* " +
                            "FROM          dbo.Currency " +
                            $"WHERE        (dbo.Currency.Number = '{value}')";
            else if (ValueName.Equals("ID"))
                queryString = $"" +
                                "SELECT        dbo.Currency.* " +
                                "FROM          dbo.Currency " +
                                $"WHERE        (dbo.Currency.ID = '{value}')";

            else
                throw new ArgumentOutOfRangeException();

            CurrencyModel currencyModel = new CurrencyModel();

            DataAccess.ExecuteQueryCommand(queryString, CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                currencyModel.ID = (int)dataReader[0];
                currencyModel.Name = (string)dataReader[1];
                currencyModel.PartName = (string)dataReader[2];
                currencyModel.CurrencyValue = (double)dataReader[3];
                currencyModel.Number = (int)dataReader[4];
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            return currencyModel;
        }
        public void InsertNewCurrency(CurrencyModel currency)
        {
            int sqlparameters = 4;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@Name", currency.Name);
            sqlParameters[1] = new SqlParameter("@PartName", currency.PartName);
            sqlParameters[2] = new SqlParameter("@CurrencyValue", currency.CurrencyValue);
            sqlParameters[3] = new SqlParameter("@Number", currency.Number);
            
            DataAccess.ExecuteNonQueryCommand("spInsertCurrency", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        public void UpdateCurrency(CurrencyModel currency)
        {
            int sqlparameters = 4;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@ID", currency.ID);
            sqlParameters[1] = new SqlParameter("@Name", currency.Name);
            sqlParameters[2] = new SqlParameter("@PartName", currency.PartName);
            sqlParameters[3] = new SqlParameter("@CurrencyValue", currency.CurrencyValue);

            DataAccess.ExecuteNonQueryCommand("spUpdateCurrency", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCurrency(int id)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", id);

            DataAccess.ExecuteNonQueryCommand("spDeleteCurrency", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
    }
}
