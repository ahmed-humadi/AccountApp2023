using BusinessLib;
using DataEntitiesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib
{
    public class StoreTableModel
    {
        private StoreBL storeBL;

        public StoreTableModel()
        {
            storeBL = new StoreBL();
        }
        public StoreTable GetStores()
        {
            return storeBL.GetStores();
        }
        public void UpdateStoreTable(StoreTable categoryTable)
        {
            storeBL.UpdateStoreTable(categoryTable);
        }
    }
}
