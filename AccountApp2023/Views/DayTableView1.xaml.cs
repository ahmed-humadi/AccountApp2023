using FrameWorkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModelsLib;

namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for DayTableView1.xaml
    /// </summary>
    public partial class DayTableView1 : UserControl, IView
    {
        public DayTableView1()
        {
            InitializeComponent();
        }
        public bool IsAnyProprtyChanged 
        {
            get => DayTableViewModel1.IsAnyProprtyChanged;
            set => DayTableViewModel1.IsAnyProprtyChanged = value;
        }
        public void Save()
        {
            DayTableViewModel1.Save();
        }
        public ViewModes ViewMode { get => DayTableViewModel1.ViewMode; set => DayTableViewModel1.ViewMode = value; }
    }
}
