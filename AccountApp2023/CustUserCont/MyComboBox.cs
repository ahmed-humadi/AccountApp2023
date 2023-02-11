using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace AccountApp2023.CustUserCont
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AccountApp2023.CustUserCont"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AccountApp2023.CustUserCont;assembly=AccountApp2023.CustUserCont"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MyComboBox/>
    ///
    /// </summary>
    public class MyComboBox : ComboBox
    { 
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(MyComboBox), new PropertyMetadata(false));
        public int SelectedItemID
        {
            get { return (int)GetValue(SelectedItemIDProperty); }
            set { SetValue(SelectedItemIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemIDProperty =
            DependencyProperty.Register("SelectedItemID", typeof(int), typeof(MyComboBox), new PropertyMetadata(0));

        public string SelectedItemBarCode
        {
            get { return (string)GetValue(SelectedItemBarCodeProperty); }
            set { SetValue(SelectedItemBarCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemBarCodeProperty =
            DependencyProperty.Register("SelectedItemBarCode", typeof(string), typeof(MyComboBox), new PropertyMetadata(string.Empty));

        #region private Fields
        private TextBox text;
        private Button searchButton;
        private ComboBoxItem currentItem;
        private int _currentItemIdex;
        #endregion
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            text = GetTemplateChild("PART_EditableTextBox") as TextBox;

            searchButton = GetTemplateChild("PART_SearchButton") as Button;

            if (!(text is null))
            {
                text.TextChanged += Text_TextChanged;
            }
            this.DropDownOpened += MyComboBox_DropDownOpened;

            if(!(searchButton is null))
            {
                searchButton.Click += SearchButton_Click;
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseSearchButtonClick();
        }
        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).IsKeyboardFocusWithin)
            {
                RaiseTextCahanged();
            }
            else
            {
                // do nothing since the user does not chamge the text it is chnaged by selectinn changed
            }
        }

        public event EventHandler TextChanged;
        public void RaiseTextCahanged()
        {
            TextChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler SearchButtonClick;
        protected void RaiseSearchButtonClick()
        {
            SearchButtonClick?.Invoke(this, new EventArgs());
            base.IsDropDownOpen = true;
        }

        // new keyword use bc the base class uses the same event
        // but when trying to use it it does not work so new event where defined
        public new event EventHandler SelectionChanged;
        protected void RaiseSelectionChangedClick()
        {
            SelectionChanged?.Invoke(this, new EventArgs());
        }
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (!(base.SelectedItem is null))
            {
                text.TextChanged -= Text_TextChanged;
                if (base.SelectedItem is Tuple<int, string, string> selectedItem)
                {
                    text.Text = new String(((selectedItem).Item3).ToArray());
                    // set the item id
                    SelectedItemBarCode = selectedItem.Item2;
                    SelectedItemID = selectedItem.Item1;
                }
                else if (base.SelectedItem is Tuple<int, string> selectedItem1)
                {
                    text.Text = new String(((selectedItem1).Item2).ToArray());
                    // set the item id
                    SelectedItemID = selectedItem1.Item1;
                }
                else if (base.SelectedItem is Tuple<int, int, string> selectedItem2)
                {
                    text.Text = new String(((selectedItem2).Item3).ToArray());
                    // set the item id
                    SelectedItemBarCode = selectedItem2.Item2.ToString();
                    SelectedItemID = selectedItem2.Item1;

                }
                else if (base.SelectedItem is string selectedItem3)
                {
                    text.Text = selectedItem3;
                }
                text.TextChanged += Text_TextChanged;
                base.SelectedItem = null;
                RaiseSelectionChangedClick();
                e.Handled = true;

                if (!(text is null))
                {
                    int strlength = text.Text.Length;
                    text.SelectionStart = strlength;

                }
            }
           
           base.IsDropDownOpen = false;
        }
        private void MyComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!(text is null))
            {
                int strlength = text.Text.Length;

                text.SelectionStart = strlength;
            }
        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.ItemsSource != null)
            {
                _currentItemIdex = 0;

                currentItem = (ComboBoxItem)base.ItemContainerGenerator.ContainerFromIndex(_currentItemIdex);

                if (currentItem is null)
                    return;

                currentItem.BorderBrush = new SolidColorBrush(Colors.LightGray);

                currentItem.BorderThickness = new Thickness(1);

                currentItem.BringIntoView();

            }
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            
            if(base.IsDropDownOpen)
            {
                if(e.Key == Key.Up || e.Key == Key.Down)
                {
                    if(_currentItemIdex  >= 0)
                    {
                        if (e.Key == Key.Up)
                        {
                            if ((_currentItemIdex - 1) < 0)
                                return;
                            currentItem = (ComboBoxItem)base.ItemContainerGenerator.ContainerFromIndex(_currentItemIdex);
                            currentItem.BorderBrush = new SolidColorBrush(Colors.Transparent);
                            currentItem.BorderThickness = new Thickness(1);

                            _currentItemIdex -= 1;

                            currentItem = (ComboBoxItem)base.ItemContainerGenerator.ContainerFromIndex(_currentItemIdex);
                            currentItem.BorderBrush = new SolidColorBrush(Colors.LightGray);
                            currentItem.BorderThickness = new Thickness(1);
                            if (_currentItemIdex < 0)
                            {
                                _currentItemIdex = 0;
                            }
                            currentItem.BringIntoView();
                        }
                        else if(e.Key == Key.Down)
                        {
                            if ((_currentItemIdex + 2) > this.Items.Count)
                                return;
                            currentItem = (ComboBoxItem)base.ItemContainerGenerator.ContainerFromIndex(_currentItemIdex);
                            currentItem.BorderBrush = new SolidColorBrush(Colors.Transparent);
                            currentItem.BorderThickness = new Thickness(1);

                            _currentItemIdex += 1;

                            currentItem = (ComboBoxItem)base.ItemContainerGenerator.ContainerFromIndex(_currentItemIdex);
                            currentItem.BorderBrush = new SolidColorBrush(Colors.LightGray);
                            currentItem.BorderThickness = new Thickness(1);

                            currentItem.BringIntoView();
                        }
                        e.Handled = true;
                        return;
                    }
                }
                else if (e.Key == Key.Enter)
                {
                        base.SelectedItem = base.Items[_currentItemIdex];

                        base.IsDropDownOpen = false;
                }
            }
        }
    }
}
