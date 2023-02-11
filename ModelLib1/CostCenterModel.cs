using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib1
{
    public class CostCenterModel
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

