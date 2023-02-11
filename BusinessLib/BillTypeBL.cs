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
    public class BillTypeBL
    {
        private DataAccess _dataAccess;
        public DataAccess DataAccess
        {
            get
            {
                if (_dataAccess is null)
                    return _dataAccess = new DataAccess(MyConnectionString.GetConnectionString("SqlAccountDataBase"));
                return _dataAccess;
            }
            set { _dataAccess = value; }
        }
        public DataTable GetBillTypes()
        {
            DataTable dataTable = new DataTable();

            DataAccess.ExecuteQueryCommand($"Select * From dbo.TypeBill", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            dataTable.Load(dataReader);

            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();
            dataTable.AcceptChanges();
            return dataTable;
        }

        public void UpdateBillTypeTable(DataTable dataTable)
        {
            DataAccess.OpenSqlConnection();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[2];

                    sqlParameters[0] = new SqlParameter($"@{"Name"}", dataRow["Name"]);
                    sqlParameters[1] = new SqlParameter($"@{"Number"}", dataRow["Number"]);
                    DataAccess.ExecuteNonQueryCommand("spInsertBillType", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[3];

                    sqlParameters[0] = new SqlParameter($"@{"ID"}", dataRow["ID"]);
                    sqlParameters[1] = new SqlParameter($"@{"Name"}", dataRow["Name"]);
                    sqlParameters[2] = new SqlParameter($"@{"Number"}", dataRow["Number"]);
                    DataAccess.ExecuteNonQueryCommand("spUpdateBillType", CommandType.StoredProcedure, sqlParameters);
                    DataAccess.ExecuteNonQuery();
                }
            }
            DataAccess.CloseSqlConnection();
        }
    }
}
