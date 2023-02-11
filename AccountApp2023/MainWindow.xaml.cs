using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AccountApp2023.CustUserCont;
using AccountingAppMainWind.Views;
using AccountApp2023.Views;
using ViewModelsLib;
using System.Data;
using ViewModelsLib.ViewModelEventArgs;
using System.ComponentModel;
using FrameWorkLib;
namespace AccountApp2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ViewModes viewMode;
        public ViewModes ViewMode
        {
            get { return viewMode; }
            set
            {
                viewMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewMode"));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // when this window is loaded go the data base and get the bill types
            // that were created by the user and add them to this window menu
            try
            {
                mainWindowViewModel.LoadViewModelCommand.Execute(null);

                DataTable dataTable = mainWindowViewModel.BillTypeTable;

                foreach (DataRow row in dataTable.Rows)
                {

                    string Billname = row["Name"] as string;
                    int BillTypeID = (int)row["ID"];
                    int BillTypeNumber = (int)row["Number"];
                    MenuItem menuItem = new MenuItem();
                    menuItem.Click += MenuItem_Click;
                    menuItem.Name = $"SideMenu_MenuItem_{BillTypeID}_{BillTypeNumber}_Display";
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    TextBlock icon = new TextBlock();
                    icon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    icon.Text = "\xE1DA ";
                    icon.TextAlignment = TextAlignment.Center;
                    TextBlock nameTxt = new TextBlock();
                    nameTxt.TextAlignment = TextAlignment.Center;
                    nameTxt.Text = Billname;
                    stackPanel.Children.Add(icon);
                    stackPanel.Children.Add(nameTxt);
                    menuItem.Header = stackPanel;
                    SideMenuItem1.Items.Add(menuItem);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            //if (MessageBox.Show("هل تريد أغلاق النافذة", "أغلاق", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (sender is Button)
                {
                    TabItem tab = (((sender as Button).Parent as StackPanel).Parent as TabItmesHeaderUC).Parent as TabItem;
                    int index = int.Parse((tab).Name.Split('_')[1]);

                    // be fore we set the content to null we check if there is any changes the user made
                   
                    IView view = (tab.Content as IView);
                    if (view != null)
                    {
                        bool isAnyChanges = view.IsAnyProprtyChanged;

                        if (isAnyChanges == true)
                        {
                            if (MessageBox.Show("هل تريد حفظ التغيرات", "أغلاق", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                view.Save();
                                return;
                            }
                            else
                            {
                                view.IsAnyProprtyChanged = false;
                            }
                        }
                    }
                    ((TabItmesHeaderUC)tab.Header).TabItem_CloseBtn.Click -= TabItem_CloseBtn_Click;
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
        private void ShowDayTableView(TabItem tabItem)
        {
            DayTableView1 dayTableView1 = new DayTableView1();
            dayTableView1.DayTableViewModel1.ViewModeChanged += ViewModel_ViewModeChanged;
            tabItem.Content = dayTableView1;
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
        private void Float_Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = TabControl.Items[0] as TabItem;

            Window window = new Window();

            window.Content = tabItem.Content;

            window.Show();
        }
        private void TabControl_Drop(object sender, DragEventArgs e)
        {

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IView view = (TabControl.SelectedContent as IView);
            if(!(view is null))
                this.ViewMode = view.ViewMode;

            /*if (TabControl.Items.Count > 0)
            {
                int prevIdx = TabControl.Items.IndexOf(1);

                TabControl.SelectedIndex = prevIdx;
            }*/

        }
    }
}
