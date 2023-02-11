using BusinessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModelsLib;


namespace AccountingAppMainWind.Views
{
    /// <summary>
    /// Interaction logic for AccountsWinView.xaml
    /// </summary>
    public partial class AccountsWinView : Window
    {
        public AccountTableViewModel AccountsGuideViewModel { get; set; }

        public EndAccountsViewModel EndAccountsViewModel { get; set; }
        public AccountsWinView()
        {
            InitializeComponent();

            AccountsGuideViewModel = new AccountTableViewModel();
            EndAccountsViewModel = new EndAccountsViewModel();

            this.DataContext = this;
        }
    }
}

