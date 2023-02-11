using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp2023.ITabView
{
    public enum Modes
    {
        بحث,
        تعديل
    }
    public interface ITabView
    {
        Modes Modes { get; set; }
    }
}
