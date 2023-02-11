using DataAccessLib;
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
    public class CostCenterBL
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
        public List<CostCenterModel> GetCostCenters()
        {
            DataAccess.ExecuteQueryCommand($"Select Top 100 ID, Name From CostCenter", CommandType.Text);
            DataAccess.OpenSqlConnection();
            IDataReader dataReader = DataAccess.DataReader();

            List<CostCenterModel> list = new List<CostCenterModel>();
            while (dataReader.Read())
            {
                CostCenterModel costCenterModel = new CostCenterModel()
                {
                    ID = (int)dataReader[0],
                    Name = (string)dataReader[1],

                };
                list.Add(costCenterModel);
            }
            DataAccess.CloseDataReader();
            DataAccess.CloseSqlConnection();

            return list;
        }
        public void InsertNewCostCenter(CostCenterModel costCenter)
        {
            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@Name", costCenter.Name);

            DataAccess.ExecuteNonQueryCommand("spInsertCostCenter", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        public void UpdateCostCenter(CostCenterModel costCenter)
        {
            int sqlparameters = 2;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];

            sqlParameters[0] = new SqlParameter("@ID", costCenter.ID);
            sqlParameters[1] = new SqlParameter("@Name", costCenter.Name);

            DataAccess.ExecuteNonQueryCommand("spUpdateCostCenter", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCostCenter(int id)
        {

            int sqlparameters = 1;

            DataAccess.OpenSqlConnection();

            SqlParameter[] sqlParameters = new SqlParameter[sqlparameters];
            sqlParameters[0] = new SqlParameter("@ID", id);

            DataAccess.ExecuteNonQueryCommand("spDeleteCostCenter", CommandType.StoredProcedure, sqlParameters);

            DataAccess.ExecuteNonQuery();

            DataAccess.CloseSqlConnection();
        }

    }
}
