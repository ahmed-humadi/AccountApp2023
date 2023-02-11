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
using AccountApp2023.CustUserCont;
using System.ComponentModel;
using AccountApp2023.ITabView;
using System.Diagnostics;
using AccountingAppMainWind.Views;
using AccountApp2023.Views;

namespace AccountApp2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void SideMenuItem_Click(object sender, RoutedEventArgs e)
        {
         
            if (TabControl.Items.Count == 1) // since we have one main tab
                CreateTabControlItems(sender, true);
            else
            {
                foreach (TabItem tab in TabControl.Items)
                {
                    MenuItem menuItem = sender as MenuItem;
                    if (menuItem.Name.Split('_').ToList()[3].Equals("OneDisplay"))
                    {
                        return;
                    }
                }
                CreateTabControlItems(sender, true);
            }
        }
        private void CreateTabControlItems(object sender, bool IsSideMenuItem)
        {
        
            if (IsSideMenuItem is true)
            {
                int numberOfTabItems = TabControl.Items.OfType<TabItem>().Count();
                MenuItem menuItem = sender as MenuItem;
                // Create TabItem Header
                TabItmesHeaderUC tabItmesHeaderUC = new TabItmesHeaderUC();
                tabItmesHeaderUC.TabItem_IcontxtBx.Text = " ";// (menuItem.Header as StackPanel).Children.OfType<TextBlock>().First().Text;
                tabItmesHeaderUC.TabItem_NametxtBx.Text = (menuItem.Header as StackPanel).Children.OfType<TextBlock>().Last().Text;
                tabItmesHeaderUC.TabItem_CloseBtn.Name = $"TabItem_closeBtn_{numberOfTabItems}";
                tabItmesHeaderUC.TabItem_CloseBtn.Click += TabItem_CloseBtn_Click;
                //
                TabItem tabItem = new TabItem() { Name = $"tabItem_{numberOfTabItems}", Header = tabItmesHeaderUC, IsSelected = true };
                TabControl.Items.Add(tabItem);
                if (menuItem.Name.Equals("SideMenu_MenuItem_3_Display"))
                {
                    ShowDayTableView(tabItem);
                }
                else if (menuItem.Name.Equals("SideMenu_MenuItem_41_Display"))
                {
                    ShowCatogryTableView(tabItem);
                }
                else if (menuItem.Name.Equals("SideMenu_MenuItem_42_Display"))
                {
                    ShowStoreTableView(tabItem);
                }
                else if (menuItem.Name.Equals("SideMenu_MenuItem_43_Display"))
                {
                    ShowItemTableView(tabItem);
                }
            }
        }

        private void ShowItemTableView(TabItem tabItem)
        {
            ItemTableView itemTableView = new ItemTableView();
            tabItem.Width = itemTableView.Width;
            tabItem.Content = itemTableView;
        }

        private void ShowStoreTableView(TabItem tabItem)
        {
            tabItem.Content = new StoreView();
        }

        private void TabItem_CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد أغلاق النافذة", "أغلاق", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (sender is Button)
                {
                    TabItem tab = (((sender as Button).Parent as StackPanel).Parent as TabItmesHeaderUC).Parent as TabItem;
                    int index = int.Parse((tab).Name.Split('_')[1]);
                    ((TabItmesHeaderUC)tab.Header).TabItem_CloseBtn.Click -= TabItem_CloseBtn_Click;
                    //switch ((tab.Header as TabItmesHeaderUC).TabItem_NametxtBx.Text)
                    //{
                    //    case "دليل الحسابات":
                    //        (tab.Content as AccountsView).Dispose();
                    //        break;
                    //}
                    tab.Content = null;
                    TabControl.Items.Remove(tab);
                    tab = null;
                   
                    // rearrange
                    foreach (TabItem tabItem in TabControl.Items)
                    {
                        if (!tabItem.Name.Equals("tabItem_0")) // main tab
                        {
                            int tabItemIndex = int.Parse(tabItem.Name.Split('_')[1]);
                            if (tabItemIndex > index)
                            {
                                ((TabItmesHeaderUC)tabItem.Header).TabItem_CloseBtn.Name = $"TabItem_closeBtn_{index}";
                                tabItem.Name = $"tabItem_{index}";
                                index++;
                            }
                        }
                    }
                }
            }
        }
        System.Threading.CancellationTokenSource cancellationToken = new System.Threading.CancellationTokenSource();
        private void ShowDayTableView(TabItem tabItem)
        {
            
            tabItem.Content = new DayTableView();
        }
        private void ShowCatogryTableView(TabItem tabItem)
        {
            tabItem.Content = new CategoryView();
        }
        private void SideMenuItem_Click1(object sender, RoutedEventArgs e)
        {
            AccountsWinView accountsWinView = new AccountsWinView();
            accountsWinView.Show();
        }
    }
}
