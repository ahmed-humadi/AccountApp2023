using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FrameWorkLib;
namespace AccountApp2023.Views
{
    public interface IView
    {
        bool IsAnyProprtyChanged { get; set; }
        ViewModes ViewMode { get; set; }
        void Save();
    }
}
