using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
namespace AccountApp2023.CustUserCont
{
    /// <summary>
    /// Interaction logic for SearchUC.xaml
    /// </summary>
    public partial class SearchUC : UserControl
    {
        public SearchUC()
        {
          InitializeComponent();
        }
        // The traditional event wrapper.
        public event TextChangedEventHandler MyTextChanged;
       
        private void search_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyTextChanged?.Invoke(sender, e);
        }
        public event SelectionChangedEventHandler MySelectionChanged;

        private void listBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MySelectionChanged?.Invoke(sender, e);
        }
       
    }
}
