﻿using DataAccessLib;
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
    public class CategoryBL
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

        public Tuple<string, int> GetGroup(int id)
        {
            DataAccess.ExecuteQueryCommand($"Select Name From Category Where ID = '{id}'", CommandType.Text);
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
            DataAccess.ExecuteQueryCommand($"Select ID From Category Where Name = '{name}'", CommandType.Text);
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
        public List<string> GetGroups(string name)
        {
            DataAccess.ExecuteQueryCommand($"Select TOP 10 Name From Category Where Name LIKE '%{name}%'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<string> list = new List<string>();


            while (dataReader.Read())
            {
                list.Add((string)dataReader[0]);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        
        public List<Tuple<int, string>> GetGroups1(string name)
        {
            DataAccess.ExecuteQueryCommand($"Select ID, Name From Category Where Name LIKE '%{name}%'", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<Tuple<int, string>> list = new List<Tuple<int, string>>();

            Tuple<int, string> tuple;

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
        public List<string> GetGroups()
        {
            DataAccess.ExecuteQueryCommand($"Select Name From Category", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<string> list = new List<string>();

            while (dataReader.Read())
            {
                list.Add((string)dataReader[0]);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        public List<Tuple<int, string>> GetGroups1()
        {
            DataAccess.ExecuteQueryCommand($"Select Top 100 ID, Name From Category", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<Tuple<int, string>> list = new List<Tuple<int, string>>();
            Tuple<int, string> tuple;
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
        public CategoryTable GetCategories()
        {

            CategoryTable categoryTable = new CategoryTable();
            DataAccess.ExecuteQueryCommand($"Select * From Category", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            while (dataReader.Read())
            {
                CategoryRow categoryRow = categoryTable.NewCategoryRow();
                categoryRow["ID"] = (int)dataReader[0];
                categoryRow["Name"] = (string)dataReader[1];
                categoryTable.AddCategoryRow(categoryRow);

            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            categoryTable.AcceptChanges();
            return categoryTable;
        }
        public void UpdateCategoryTable(CategoryTable categoryTable)
        {
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in categoryTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[categoryTable.Columns.Count - 1];

                    sqlParameters[0] = new SqlParameter($"@{categoryTable.Columns["Name"].ColumnName}", dataRow[categoryTable.Columns["Name"].ColumnName]);

                    DataAccess.ExecuteNonQueryCommand("spInsertCategory", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[categoryTable.Columns.Count];

                    for (int i = 0; i < categoryTable.Columns.Count; i++)
                    {
                        DataColumn dataColumn = categoryTable.Columns[i];
                        sqlParameters[i] = new SqlParameter($"@{dataColumn.ColumnName}", dataRow[dataColumn.ColumnName]);
                    }
                    DataAccess.ExecuteNonQueryCommand("spUpdateCategory", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
            }
            DataAccess.CloseSqlConnection();
        }
    }
}
