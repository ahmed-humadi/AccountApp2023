using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
using DataEntitiesLib;
namespace ModelsLib
{
    public class BillTableModel
    {
        private ItemBL itemBL;
        private BillBL billBL;
        private AccountsBL accountsBL;
        private StoreBL storeBL;
        private CategoryBL categoryBL;
        private DaysBL daysBL;
        public BillTableModel()
        {
            itemBL = new ItemBL();
            billBL = new BillBL();
            accountsBL = new AccountsBL();
            storeBL = new StoreBL();
            categoryBL = new CategoryBL();
            daysBL = new DaysBL();
        }
        public List<Tuple<int , string, string>> GetItems()
        {
            return itemBL.GetItemsAsList();
        }
        public List<Tuple<int, string, string>> GetItems(string name)
        {
            return itemBL.GetItemsAsList(name);
        }
        public List<Tuple<int, string>> GetStores()
        {
            return storeBL.GetStores1();
        }
        public List<Tuple<int, string>> GetStores(string name)
        {
            return storeBL.GetStores1(name);
        }
        public Tuple<int, string, string> GetItem(string barCode)
        {
            return itemBL.GetItem(barCode);
        }
        public List<Tuple<int, string>> GetAccounts()
        {
            return accountsBL.GetAccounts();
        }
        public List<Tuple<int, string>> GetAccounts(string name)
        {
            return accountsBL.GetAccounts(name);
        }
        public int GetMaxBillNumber(int parentID)
        {
            return billBL.GetMaxBillNumber(parentID);
        }
        public int GetMinBillNumber(int parnetID)
        {
            return billBL.GetMinBillNumber(parnetID);
        }
        public int GetBill1ID(int number, int parentID)
        {
            return billBL.GetBill1ID(number, parentID);
        }
        public int GetMax1BillNumber(int parnetID)
        {
            return billBL.GetMax1BillNumber(parnetID);
        }
        public void InsertBill1(Tuple<int, string, int, int, string, int, double, Tuple<string>> parametersTuple)
        {
            billBL.InsertBill1(parametersTuple);
        }
        public void InsertBill2(List<Tuple<int, int, decimal, decimal, string>> parametersList)
        {
            billBL.InsertBill2(parametersList);
        }
        public void DeleteBill2(int parentID)
        {
            billBL.DeleteBill2(parentID);
        }
        public void DeleteBill1(int ID)
        {
            billBL.DeleteBill1(ID);
        }
        public void UpdateBill1(Tuple<int, string, int, int, string, int, double, Tuple<string>> parametersTuple, int day1ID)
        {
            billBL.UpdateBill1(parametersTuple, day1ID);
        }
        public List<Tuple<int, double, double, string, int, string, string, Tuple<double>>> GetBill2(int Bill1id)
        {
            return billBL.GetBill2(Bill1id);
        }
        public Tuple<int, string, string, double, string, int, string, Tuple<int, string>> GetBill1(int number, int parentID)
        {
            return billBL.GetBill1(number, parentID);
        }
        public void AddItemBalance(int id, double balance)
        {
            itemBL.AddItemBalance(id, balance);
        }
        public void AssignItmeInvetory(int id, int Store)
        {
            itemBL.AssignItmeInvetory(id, Store);
        }
        public ItemTable GetItem(int id)
        {
           return itemBL.GetItem(id);
        }
        public void SubstractItemBalance(int id, double balance)
        {
            itemBL.SubstractItemBalance(id, balance);
        }
        public int GetMax_1DayNumber()
        {
            return daysBL.GetMax_1DayNumber();
        }
        public void InsertDay1(Tuple<DateTime, string, int, string, int> parameters)
        {
            daysBL.InsertDay1(parameters);
        }
        public void InsertDay2(List<Tuple<int, int, float, float, string>> parametersList)
        {
            daysBL.InsertDay2(parametersList);
        }
        public void DeleteDay1(int type)
        {
            daysBL.DeleteDay1(type);
        }
        public int GetDay1ID(int number)
        {
            return daysBL.GetDay1ID(number);
        }

        public int GetDayNumber(int type)
        {
            return daysBL.GetDay1Number(type);
        }
    }
}
