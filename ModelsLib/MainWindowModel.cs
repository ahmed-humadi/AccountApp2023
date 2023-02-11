using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
namespace ModelsLib
{
    public class MainWindowModel
    {
        private BillTypeBL billTypeBL;
        public MainWindowModel()
        {
            billTypeBL = new BillTypeBL();
        }

        public DataTable GetBillTypes()
        {
            return billTypeBL.GetBillTypes();
        }
    }
}
