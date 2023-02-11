using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class MyComboBox1 : ComboBox
    {
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(MyComboBox1), new PropertyMetadata(false));
        public int SelectedItemID
        {
            get { return (int)GetValue(SelectedItemIDProperty); }
            set { SetValue(SelectedItemIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemIDProperty =
            DependencyProperty.Register("SelectedItemID", typeof(int), typeof(MyComboBox1), new PropertyMetadata(0));

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

            if (!(searchButton is null))
            {
                searchButton.Click += SearchButton_Click;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseSearchButtonClick();
        }

        private void MyComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!(text is null))
            {
                int strlength = text.Text.Length;

                text.SelectionStart = strlength;
            }

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
        protected void RaiseTextCahanged()
        {
            TextChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler SearchButtonClick;

        protected void RaiseSearchButtonClick()
        {
            SearchButtonClick?.Invoke(this, new EventArgs());
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
            if (!(this.SelectedItem is null))
            {
                text.TextChanged -= Text_TextChanged;

                string selectedItem = this.SelectedItem as string;

                text.Text = new String(((selectedItem)).ToArray());
                text.TextChanged += Text_TextChanged;
                RaiseSelectionChangedClick();
                if (!(text is null))
                {
                    int strlength = text.Text.Length;
                    text.SelectionStart = strlength;

                }

            }
            base.SelectedItem = null;
            base.IsDropDownOpen = false;
            e.Handled = true;

        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            //base.OnItemsChanged(e);
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
            //this.SelectedItem = null;
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            if (base.IsDropDownOpen)
            {
                if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    if (_currentItemIdex >= 0)
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
                        else if (e.Key == Key.Down)
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
                    this.SelectedItem = base.Items[_currentItemIdex];
                    //base.IsDropDownOpen = false;
                }
            }
        }
    }
}

