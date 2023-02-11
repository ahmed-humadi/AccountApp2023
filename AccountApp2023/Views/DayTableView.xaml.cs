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
using ViewModelsLib;

namespace AccountApp2023.Views
{
    /// <summary>
    /// Interaction logic for DayTableView.xaml
    /// </summary>
    public partial class DayTableView : UserControl
    {
        public DayTableViewModel dayTableViewModel { get; set; }
        public DayTableView()
        {
            InitializeComponent();
            dayTableViewModel = new DayTableViewModel();

            this.DataContext = this;
        }
        private void dataGrid_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (sender is ComboBox)
            {
                // this is becuase the textchanged event will fire 
                // whenever the text changes in the combobox
                // however the user does not enter any value
                // therefore we want just to fire the event 
                // when the user changed the value not anything eles
                if((sender as ComboBox).IsKeyboardFocusWithin == true /*&& isSelectedValueChanged == false*/)
                {
                    dataGrid.OnCombBxCellTextChanged(new EventArgs());
                }
            }
            else if (sender is TextBox)
            {
                if ((sender as TextBox).IsKeyboardFocusWithin)
                {

                    dataGrid.OnCellTextChanged(new EventArgs());
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).IsKeyboardFocusWithin == true)
            {
                (sender as ComboBox).IsDropDownOpen = true;
                dataGrid.OnCombBxCellSelectionChanged(new EventArgs());
            }
        }
        private void dataGridComboBx_DropDownClosed(object sender, EventArgs e)
        {
            if ((sender as ComboBox).IsKeyboardFocusWithin == true)
            {
                dataGrid.OnCombBxDropClosed(new EventArgs());
            }
        }
        private void Debit_Credit_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if(textBox.Name.Equals("Debit"))
            {
                if (textBox.IsKeyboardFocusWithin == true)
                    dataGrid?.OnDebitTextChanged(new EventArgs());
            }
            else
            {
                if (textBox.IsKeyboardFocusWithin == true)
                    dataGrid?.OnCreditTextChanged(new EventArgs());
            }

        }
      
        
    }
}
