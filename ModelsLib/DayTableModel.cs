using BusinessLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib
{
    public class DayTableModel
    {
        private DaysBL businessLogic;
        public DayTableModel()
        {
            businessLogic = new DaysBL();
        }
        public List<Tuple<string, int, int>> GetAccounts(string accountName)
        {
            return businessLogic.GetAccounts(accountName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <returns> Tuple with item1 : account Name, Item2 : account ID</returns>
        public List<Tuple<string, int, int>> GetAccounts(int accountCode)
        {
            return businessLogic.GetAccount(accountCode);
        }
        /// <summary>
        /// return max day id + 1
        /// </summary>
        /// <returns></returns>
        public int GetMax_1DayID()
        {
            return businessLogic.GetMax_1DayID();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxDayID()
        {
            return businessLogic.GetMaxDayID();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMinDayID()
        {
            return businessLogic.GetMinDayID();
        }
        /// <summary>
        /// gat day2
        /// </summary>
        /// <param name="id">day1 id</param>
        /// <returns></returns>
        public List<Tuple<double, double, int, string, int, int, string>> GetDay2(int id)
        {
           return businessLogic.GetDay2(id);
        }
        /// <summary>
        /// gat day1
        /// </summary>
        /// <param name="id">day1 id</param>
        /// <returns></returns>
        public Tuple<int, DateTime, string, int> GetDay1(int id)
        {
            return businessLogic.GetDay1(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertDay1(Tuple<DateTime, string, int, string> parametersTuple)
        {
            businessLogic.InsertDay1(parametersTuple);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertDay2(List<Tuple<int, int, float, float, string>> parametersTuplelist)
        {
            businessLogic.InsertDay2(parametersTuplelist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        /// <param name="day1ID"></param>
        public void UpdateDay1(Tuple<DateTime, string, int, string> parametersTuple, int day1ID)
        {
            businessLogic.UpdateDay1(parametersTuple, day1ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        public void DeleteDay2(int parentID)
        {

            businessLogic.DeleteDay2(parentID);
        }
    }
}
