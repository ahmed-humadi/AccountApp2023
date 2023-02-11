using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
using DataEntitiesLib;

namespace ModelsLib
{
    public class ItemTableModel
    {
        private ItemBL itemBL;
        public ItemTableModel()
        {
            itemBL = new ItemBL();
        }
        public ItemTable GetItems()
        {
           return itemBL.GetItems();
        }
        public Tuple<string , int> GetGroup(int id)
        {
            return itemBL.GetGroup(id);
        }
        public void UpdateItemTable(ItemTable itemTable)
        {
            itemBL.UpdateItemTable(itemTable);
        }
        public Tuple<int, string> GetGroup(string name)
        {
            return itemBL.GetGroup(name);
        }
    }
}
