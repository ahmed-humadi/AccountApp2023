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
        private DaysBL dayBL;
        private AccountsBL accountsBL;
        public DayTableModel()
        {
            dayBL = new DaysBL();
            accountsBL = new AccountsBL();
        }
        public List<Tuple<int, int, string>> GetAccounts1()
        {
            return accountsBL.GetAccounts1();
        }
        public List<Tuple<int, int, string>> GetAccounts1(string accountName)
        {
            return accountsBL.GetAccounts1(accountName);
        }
        public List<Tuple<string, int, int>> GetAccounts(string accountName)
        {
            return dayBL.GetAccounts(accountName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <returns> Tuple with item1 : account Name, Item2 : account ID</returns>
        public List<Tuple<string, int, int>> GetAccounts(int accountCode)
        {
            return dayBL.GetAccount(accountCode);
        }
        /// <summary>
        /// return max day id + 1
        /// </summary>
        /// <returns></returns>
        public int GetMax_1DayNumber()
        {
            return dayBL.GetMax_1DayNumber();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxDayNumber()
        {
            return dayBL.GetMaxDayNumber();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMinDayNumber()
        {
            return dayBL.GetMinDayNumber();
        }
        /// <summary>
        /// gat day2
        /// </summary>
        /// <param name="id">day1 id</param>
        /// <returns></returns>
        public List<Tuple<double, double, int, string, int, int, string>> GetDay2(int id)
        {
           return dayBL.GetDay2(id);
        }
        /// <summary>
        /// gat day1
        /// </summary>
        /// <param name="number">day1 id</param>
        /// <returns></returns>
        public Tuple<int, DateTime, string, int> GetDay1(int number)
        {
            return dayBL.GetDay1(number);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertDay1(Tuple<DateTime, string, int, string, int> parametersTuple)
        {
            dayBL.InsertDay1(parametersTuple);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        public void InsertDay2(List<Tuple<int, int, float, float, string>> parametersTuplelist)
        {
            dayBL.InsertDay2(parametersTuplelist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTuple"></param>
        /// <param name="day1ID"></param>
        public void UpdateDay1(Tuple<DateTime, string, int, string, int> parametersTuple, int day1ID)
        {
            dayBL.UpdateDay1(parametersTuple, day1ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        public void DeleteDay2(int parentID)
        {

            dayBL.DeleteDay2(parentID);
        }
        public int GetDay1ID(int number)
        {
            return dayBL.GetDay1ID(number);
        }
    }
}
