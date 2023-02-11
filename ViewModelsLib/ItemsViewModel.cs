using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
namespace ViewModelsLib
{
   public class ItemsViewModel
    {
        private List<string> _itemsName;

        private readonly EndAccountsBL BusinessClass;
        public ItemsViewModel(EndAccountsBL businessClass)
        {
            BusinessClass = businessClass;
        }
        public List<string> ItemsName 
        {
            get { return _itemsName; }
            set { _itemsName = value; }
        }
 
    }
}
