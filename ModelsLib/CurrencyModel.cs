using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntitiesLib;
using BusinessLib;
namespace ModelsLib
{
    public class CurrencyModel
    {
        private readonly CurrencyBL currencyBL;
        public CurrencyModel ()
        {
            currencyBL = new CurrencyBL();
        }
        public int GetMaxCurrencyNumber_1()
        {
            return currencyBL.GetMaxCurrencyNumber_1();
        }
        public int GetMaxCurrencyNumber()
        {
            return currencyBL.GetMaxCurrencyNumber();
        }
        public CurrencyTable GetCurrencyTable(int value, string valueName)
        {
            return currencyBL.GetCurrencyTable(value, valueName);
        }
    }
}
