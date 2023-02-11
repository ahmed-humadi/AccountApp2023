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

namespace AccountApp2023.CustUserCont
{
    [TemplatePart(Name = "PART_AddButton", Type = typeof(Button))]
    public class MyListView : ListView
    {

        private Button AddButton = null;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            // get the add button
            AddButton = GetTemplateChild("PART_AddButton") as Button;
            if (AddButton is null)
                return;
            AddButton.Click += AddButton_Click;
        }
        public event EventHandler AddButton_Clicked;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(AddButton is null))
                OnAddButton_Clicked();
        }
        protected void OnAddButton_Clicked()
        {
            AddButton_Clicked?.Invoke(this, new EventArgs());
        }
        public int RowIndex
        {
            get { return (int)GetValue(RowIndexProperty); }
            set { SetValue(RowIndexProperty, value); }
        }
        // Using a DependencyProperty as the backing store for RowIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowIndexProperty =
            DependencyProperty.Register("RowIndex", typeof(int), typeof(MyListView), new PropertyMetadata(0));

        public bool EnableButton
        {
            get { return (bool)GetValue(EnableButtonProperty); }
            set { SetValue(EnableButtonProperty, value); }
        }
        // Using a DependencyProperty as the backing store for RowIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableButtonProperty =
            DependencyProperty.Register("EnableButton", typeof(bool), typeof(MyListView), new PropertyMetadata(true));

        public bool EnableSelection
        {
            get { return (bool)GetValue(EnableSelectionProperty); }
            set { SetValue(EnableSelectionProperty, value); }
        }
        // Using a DependencyProperty as the backing store for RowIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableSelectionProperty =
            DependencyProperty.Register("EnableSelection", typeof(bool), typeof(MyListView), new PropertyMetadata(true));

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if(EnableSelection)
                base.OnSelectionChanged(e);
        }
    }
}
