using BusinessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib
{
    public class StoreModel
    {
        private StoreBL storeBL;

        public StoreModel()
        {
            storeBL = new StoreBL();
        }
    }
}
