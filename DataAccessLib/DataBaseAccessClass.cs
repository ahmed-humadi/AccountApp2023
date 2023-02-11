using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Reflection;
using DataEntitiesLib;
using System.Threading;

namespace DataAccessLib
{
    public class DataBaseAccessClass
    {
       
        public DataBaseAccessClass(string Stringconn)
        {
            _connString = Stringconn;
        }
        private readonly string _connString;

        public string ConnString
        {
            get { return _connString; }
        }

        #region Execute Query with Data Reader
        // return list of type T. one column only
        public List<T> ExecuteQueryDaReader<T>(string sqlStatment, string columnName)
        {
            List<T> list = new List<T>();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using(SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            list.Add((T)sqlDataReader[columnName]);
                        }
                    }
                }
            }
            return list;
        }
        // Fill list of type T. one column only
        public void ExecuteQueryFillTwoList<T1,T2>(string sqlStatment, List<Tuple<T1,T2>> list)
        {
            list.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            list.Add(new Tuple<T1, T2>((T1)sqlDataReader[0],(T2)sqlDataReader[1]));
                        }
                    }
                }
            }
        }
        public void ExecuteQueryFillOneColumnList<T>(string sqlStatment,List<T> list)
        {
            list.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            list.Add((T)sqlDataReader[0]);
                        }
                    }
                }
            }
        }
        public Task ExecuteQueryDataReaderAsync<T>(string sqlStatment, List<T> list, CancellationToken cancellactionToken)
        {
            return Task.Run(new Action(delegate ()
            {
                list.Clear();
                using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
                {
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = sqlStatment;
                        sqlCommand.Prepare();
                        if (sqlConnection.State == ConnectionState.Closed)
                            sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                cancellactionToken.ThrowIfCancellationRequested();
                                list.Add((T)sqlDataReader[0]);
                            }
                        }
                    }
                }

            }));
        }
        // return list of T class
        public List<T> ExecuteQueryDataReaderList<T>(string sqlStatment)
            where T : class , new()
        {
            List<T> list = new List<T>();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            T obj = new T();
                            foreach (PropertyInfo property in typeof(T).GetProperties())
                            {
                                property.SetValue(obj, sqlDataReader[property.Name]);
                            }
                            list.Add(obj);
                        }
                    }
                }
            }
            return list;
        }
        // fill list of T class
        public void ExecuteQueryFillList<T>(string sqlStatment, List<T> list)
            where T : class, new()
        {
            list.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            T obj = new T();
                            foreach (PropertyInfo property in typeof(T).GetProperties())
                            {
                                property.SetValue(obj, sqlDataReader[property.Name]);
                            }
                            list.Add(obj);
                        }
                    }
                }
            }
        }
        // return table strong typed
        public T ExecuteQueryDataReaderTable<T>(string sqlStatment)
            where T : DataTable, new()
        {
            T table = new T();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlDataReader);
                    }
                }
            }
            return table;
        }
        // return table strong typed
        public void ExecuteQueryFillTable<T>(string sqlStatment, T table)
            where T : DataTable
        {
            table.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlStatment;
                    sqlCommand.Prepare();
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlDataReader);
                        table.AcceptChanges();
                    }
                }
            }
        }
        #endregion
        #region Execute Non Query
        public (int numberOfAccountsMod, int numberOfAccountsInserted) ExecuteNonQueryUpdateTable<T>
            (string insertSqlStatment, string updateSqlStatment, CommandType commandType,T table)
            where T : DataTable
        {
            int numberOfAccountsMod = 0; int numberOfAccountsInserted = 0;
            using (SqlConnection sqlConnection = new SqlConnection(MyConnectionString.GetConnectionString(ConnString)))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    foreach (DataRow dataRow in table.Rows)
                    {
                        if (dataRow.RowState == DataRowState.Added)
                        {
                            sqlCommand.CommandType = commandType;
                            sqlCommand.CommandText = insertSqlStatment;
                            sqlCommand.Parameters.Clear();
                            foreach (DataColumn dataColumn in table.Columns)
                            {
                                SqlParameter sqlParameter = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                                sqlCommand.Parameters.Add(sqlParameter);
                            }
                            sqlCommand.Prepare();
                            numberOfAccountsInserted = sqlCommand.ExecuteNonQuery();
                        }
                        if (dataRow.RowState == DataRowState.Modified)
                        {
                            sqlCommand.CommandType = commandType;
                            sqlCommand.CommandText = updateSqlStatment;
                            sqlCommand.Parameters.Clear();
                            foreach (DataColumn dataColumn in table.Columns)
                            {
                                SqlParameter sqlParameter = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                                sqlCommand.Parameters.Add(sqlParameter);
                            }
                            sqlCommand.Prepare();
                            numberOfAccountsMod = sqlCommand.ExecuteNonQuery();
                        }
                    }
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                    return (numberOfAccountsMod, numberOfAccountsInserted);
                }
            }
        }
        #endregion
    }
}
