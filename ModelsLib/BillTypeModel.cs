using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
namespace ModelsLib
{
    public class BillTypeModel
    {
        private BillTypeBL billTypeBL;

        public BillTypeModel()
        {
            billTypeBL = new BillTypeBL();

        }
        public void UpdateBillTypeTable(DataTable dataTable )
        {
            this.billTypeBL.UpdateBillTypeTable(dataTable);
        }
        public DataTable GetBillTypeTable()
        {
            return this.billTypeBL.GetBillTypes();
        }
    }
}
