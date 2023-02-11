using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelLib1
{
    public class CurrencyModel
    {
        private int iD;
        public int ID
        {
            get { return iD; }
            set
            {
                iD = value;
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        private string partName;
        public string PartName
        {
            get { return partName; }
            set
            {
                partName = value;
            }
        }
        private double currencyValue;
        public double CurrencyValue
        {
            get { return currencyValue; }
            set
            {
                currencyValue = value;
            }
        }
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
            }
        }
    }
}
