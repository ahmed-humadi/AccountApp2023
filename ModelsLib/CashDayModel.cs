using BusinessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib
{
    public class CashDayModel
    {
        private AccountsBL accountsBL;
        private CashDayBL cashDayBL;
        private DaysBL daysBL;
        public CashDayModel()
        {
            accountsBL = new AccountsBL();
            cashDayBL = new CashDayBL();
            daysBL = new DaysBL();
        }
        public List<Tuple<int, string>> GetAccounts()
        {
            return accountsBL.GetAccounts();
        }
        public List<Tuple<int, string>> GetAccounts(string name)
        {
            return accountsBL.GetAccounts(name);
        }
        public List<Tuple<int, int, string>> GetAccounts1()
        {
            return accountsBL.GetAccounts1();
        }
        public List<Tuple<int, int, string>> GetAccounts1(string accountName)
        {
            return accountsBL.GetAccounts1(accountName);
        }
        public int GetCashDayID(int number, int parentID)
        {
            return cashDayBL.GetCashDayID(number, parentID);
        }
        public int GetMax1CashDayNumber(int parentID)
        {
            return cashDayBL.GetMax1CashDayNumber(parentID);
        }
        public int GetMaxCashDayNumber(int parentID)
        {
            return cashDayBL.GetMaxCashDayNumber(parentID);
        }
        public int GetMinCashDayNumber(int parentID)
        {
            return cashDayBL.GetMinCashDayNumber(parentID);
        }
        public void InsertCashDay(Tuple<int, string, int, int, DateTime> parametersTuple)
        {
            cashDayBL.InsertCashDay(parametersTuple);
        }
        public void InsertDay1(Tuple<DateTime, string, int, string, int> tuple)
        {
            daysBL.InsertDay1(tuple);
        }
        public void InsertDay2(List<Tuple<int, int, float, float, string>> tuples)
        {
            daysBL.InsertDay2(tuples);
        }
        public int GetMax_1DayNumber()
        {
            return daysBL.GetMax_1DayNumber();
        }
        public int GetDay1ID(int number)
        {
            return daysBL.GetDay1ID(number);

        }
        public Tuple<int, int, string, int, string, DateTime> GetCashDay(int number, int parentID)
        {
            return cashDayBL.GetCashDay(number, parentID);
        }
        public List<Tuple<double, double, int, string, int, int, string>> GetDay2(int id)
        {
            return daysBL.GetDay2(id);
        }
        public Tuple<int, DateTime, string, int> GetDay1(int number)
        {
            return daysBL.GetDay1(number);
        }
        public int GetDayNumber(int type)
        {
            return daysBL.GetDay1Number(type);
        }
        public Tuple<int, int> GetDay1NumberAndID(int type)
        {
            return daysBL.GetDay1NumberAndID(type);
        }
        public void UpdateCashDay(Tuple<int, string, int, int, DateTime> parametersTuple, int casdDayID)
        {
            cashDayBL.UpdateCashDay(parametersTuple, casdDayID);
        }
        public void DeleteDay1(int type)
        {
            daysBL.DeleteDay1(type);
        }
    }
}
