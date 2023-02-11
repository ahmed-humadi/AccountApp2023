using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BusinessLib;
namespace ModelsLib
{
    public class EndAccountsTableModel
    {
        private EndAccountsBL endAccountsBL;
        public EndAccountsTableModel()
        {
            this.endAccountsBL = new EndAccountsBL();
        }
        public void GetEndAccounts(List<Tuple<string, int>> accountsList)
        {
            endAccountsBL.GetAccounts(accountsList);
        }

    }
}
