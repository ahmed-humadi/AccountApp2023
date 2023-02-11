using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLib;
using DataEntitiesLib;
using FrameWorkLib;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Data;

namespace ViewModelsLib
{
    public class ItemTableViewModel : ViewModelBase
    {
        private readonly object lockToke = new object();
        private ItemTableModel itemTableModel;
        #region view properties
        private ItemTable itemTable;
        public ItemTable ItemTable
        {
            get => itemTable;
            set
            {
                lock (lockToke)
                {
                    SetProperty(ref itemTable, value);
                }
            }
        }
        public string ItemName
        {
            set
            {
                SetProperty(ref itemName, value);  
               
            }
        }
        public string ItemID
        {
            get => itemID;
            set
            {
                SetProperty(ref itemID, value);

            }
        }
        public string ItemCode
        {
            get => itemCode;
            set
            {
                SetProperty(ref itemCode, value);

            }
        }
        public string ItemBrand
        {
            get => itemBrand;
            set
            {
                SetProperty(ref itemBrand, value);

            }
        }
        public string ItemGroup
        {
            get => itemGroup;
            set
            {
                SetProperty(ref itemGroup, value);
            }
        }
        {
            get => selectedItemGroupID;
            set
            {
            }
        }
        public bool ItemGroupIsDropedOpen
        {
            get => itemGroupIsDroped;
            set
            {
                SetProperty(ref itemGroupIsDroped, value);
            }
        }
        public string ItemEnglish
        {
            get => itemEnglish;
            set
            {
                SetProperty(ref itemEnglish, value);
            }
        }
        {
            get => itemState;
            set
            {
                SetProperty(ref itemState, value);
            }
        }
        public string ItemDate
        {
            get => itemDate;
            set
            {
                SetProperty(ref itemDate, value);
            }
        }
        public string ItemDetails
        {
            get => itemDetails;
            set
            {
                SetProperty(ref itemDetails, value);
            }
        }
        public string ItemUnit
        {
            get => itemUnit;
            set
            {
                SetProperty(ref itemUnit, value);
            }
        }
        public string ItemModDate
        {
            get => itemModDate;
            set
            {
                SetProperty(ref itemModDate, value);
            }
        }
   
        private bool isSaving = false;

        private bool isEditing;

        public bool IsSaving 
        { 
            get => isSaving;
            set
            {
                SetProperty(ref isSaving, value);
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        public bool IsEditing 
        {
            get => isEditing;
            set
            {
                SetProperty(ref isEditing, value);
                ((DelegateCommand)ModCommand).RaiseCanExecuteChanged();
            }
        }
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
            }
        }
        private bool isLoading_Group;

        public bool IsLoading_Group
        {
            get => isLoading_Group;
            set
            {
                SetProperty(ref isLoading_Group, value);
            }
        }
        private bool isVisibleID = false;
        private bool isVisibleCode = false;
        private bool isVisibleName = true;
        private bool isVisibleGroupID = true;
        private bool isVisibleGroupName = true;
        private bool isVisibleNote = true;
        private bool isVisibleBalance = true;
        private bool isVisibleBarCode = true;
        private bool isVisibleUnit = true;
        private bool isVisibleModDay = true;
        private bool isVisibleBrand = true;
        private bool isVisibleState = true;
        private bool isVisibleEnglishName = true;
        public bool IsVisibleID
        {
            get => isVisibleID;
            set
            {
                SetProperty(ref isVisibleID, value);
            }
        }
        public bool IsVisibleCode
        {
            get => isVisibleCode;
            set
            {
                SetProperty(ref isVisibleCode, value);
            }
        }
        public bool IsVisibleName
        {
            get => isVisibleName;
            set
            {
                SetProperty(ref isVisibleName, value);
            }
        }
        public bool IsVisibleGroupID
        {
            get => isVisibleGroupID;
            set
            {
                SetProperty(ref isVisibleGroupID, value);
            }
        }
        public bool IsVisibleGroupName
        {
            get => isVisibleGroupName;
            set
            {
                SetProperty(ref isVisibleGroupName, value);
            }
        }
        public bool IsVisibleNote
        {
            get => isVisibleNote;
            set
            {
                SetProperty(ref isVisibleNote, value);
            }
        }
        public bool IsVisibleBalance
        {
            get => isVisibleBalance;
            set
            {
                SetProperty(ref isVisibleBalance, value);
            }
        }
        public bool IsVisibleBarCode
        {
            get => isVisibleBarCode;
            set
            {
                SetProperty(ref isVisibleBarCode, value);
            }
        }
        public bool IsVisibleUnit
        {
            get => isVisibleUnit;
            set
            {
                SetProperty(ref isVisibleUnit, value);
            }
        }
        public bool IsVisibleModDay
        {
            get => isVisibleModDay;
            set
            {
                SetProperty(ref isVisibleModDay, value);
            }
        }
        public bool IsVisibleBrand
        {
            get => isVisibleBrand;
            set
            {
                SetProperty(ref isVisibleBrand, value);
            }
        }
        public bool IsVisibleState
        {
            get => isVisibleState;
            set
            {
                SetProperty(ref isVisibleState, value);
            }
        }
        public bool IsVisibleEnglishName
        {
            get => isVisibleEnglishName;
            set
            {
                SetProperty(ref isVisibleEnglishName, value);
            }
        }
        #endregion
        #region DelegateCommands
        public ICommand InsertCommand { get; set; }
        public ICommand ModCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ViewLoadedCommand { get; set; }
        public ICommand GroupSelectionChangedCommand { get; set; }
        public ICommand GroupTextChangedCommand { get; set; }
        public ICommand SearchByItemNameCommand { get; set; }
        public ICommand ListViewSelectionChangedCommand { get; set; }

        public ICommand ExpandTreeViewCommand { get; set; }
        public ICommand CollapseTreeViewCommand { get; set; }
        public ICommand TreeViewSelectionChangedCommand { get; set; }

        #endregion
        public ItemTableViewModel()
        {
            try
            {
                itemTableModel = new ItemTableModel();
                InsertCommand = new DelegateCommand(Insert);
                ModCommand = new DelegateCommand(Modify, () => IsEditing);
                SaveCommand = new DelegateCommand(Save, () => IsSaving);
                ViewLoadedCommand = new DelegateCommand(ViewLoaded);
                GroupSelectionChangedCommand = new DelegateCommand(GroupSelectionChanged);
                GroupTextChangedCommand = new DelegateCommand(GroupTextChanged);
                SearchByItemNameCommand = new DelegateCommand(SearchByItemName);
                ListViewSelectionChangedCommand = new DelegateCommand(ListViewSelectionChanged);
                ExpandTreeViewCommand = new DelegateCommand(ExpandTreeView);
                TreeViewSelectionChangedCommand = new DelegateCommand(TreeViewSelectionChanged);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void ExpandTreeView(object parameter)
        {
            //try
            //{
        
            //    TreeViewItem item = (TreeViewItem)((parameter as RoutedEventArgs).OriginalSource);
            //    item.Items.Clear();
            //    List<Tuple<int, string>> tuples = null;

            //    if (IsLoading)
            //        return;
            //    IsLoading = true;
            //    await Task.Run(() =>
            //    {
            //        System.Windows.Application.Current.Dispatcher.Invoke(() =>
            //        {

            //            if (int.Parse(item.Tag.ToString()) == 0) // for all categories
            //                tuples = this.itemTableModel.GetGroups();
            //            else
            //                tuples = this.itemTableModel.GetItemsForTreeView(int.Parse(item.Tag.ToString()));
            //            foreach (Tuple<int, string> ele in tuples)
            //            {
            //                TreeViewItem newItem = new TreeViewItem();
            //                newItem.Tag = ele.Item1;
            //                TextBlock textBlock = new TextBlock();
            //                textBlock.Text = $"{ele.Item2}";
            //                newItem.Header = textBlock;
            //                newItem.Items.Add("");
            //                item.Items.Add(newItem);
            //            }

            //        });
            //    });

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    IsLoading = false;
            //}
        }

        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewSelectionChanged()
        {
            try
            {
                if (this.ListViewSelectedItem is null)
                    return;

                this.IsEditing = true;

                DataRowView selectedItem = this.ListViewSelectedItem as DataRowView;

                this.ItemName       = selectedItem["Name"] as string;
                this.ItemCode       = (selectedItem["Code"]).ToString();
                this.ItemBrand      = selectedItem["Brand"] as string;
                this.ItemGroup      = selectedItem["GroupName"] as string;
                this.ItemBrand      = selectedItem["EnglishName"] as string;
                this.ItemState      = selectedItem["State"] as string;
                this.ItemModDate    =  (selectedItem["ModDate"]).ToString();
                this.ItemUnit       = selectedItem["Unit"] as string;
                this.ItemBalance    = (selectedItem["Balance"]).ToString();
                this.ItemDetails       = selectedItem["Note"] as string;
                this.ItemBarCode = selectedItem["BarCode"] as string;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        {
            try
            {
        {
        }
        private async void GroupSelectionChanged()
        {
            try
            {
                // if there is any loading return
                if (IsLoading || IsLoading_Group)
                    return;
                this.IsLoading = true;

                string SelectedGroupName = this.SelectedItemGroupName;

                if (String.IsNullOrEmpty(SelectedGroupName))
                    return;

                await Task.Run(() =>
                { 
                    this.SelectedItemGroupID = itemTableModel.GetGroup(SelectedGroupName).Item1.ToString();
                });
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        // trigged when view is loaded
        private async void ViewLoaded()
        {
            try
            {
                if (IsLoading) // wait until the task finish loading
                    return;
                IsLoading = true;

                IsEditing = false;

                selectedItemGroupName = null;
               
                await Task.Run(() =>
                {

                        }
                        this.ItemTable.AcceptChanges();
                    });
                });
            }catch(Exception ex)
            {

                MessageBox.Show(ex.Message);

            }finally
            {
                IsLoading = false;
            }
        }
        {
            try
            {
                this.IsLoading = true;

                if (this.ItemTable.Count == 0)
                    return;

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        itemTableModel.UpdateItemTable(this.ItemTable);
                    });
                });

                this.IsLoading = false;
                this.IsSaving = false;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.IsLoading = false;
               
            }
        }

        private void Modify()
        {
            try
            {
                this.IsSaving = true;

                if (this.ListViewSelectedItem is null)
                    return;

                DataRowView selectedItem = this.ListViewSelectedItem as DataRowView;

                ItemRow modifiedRow = this.ItemTable.Rows.Find((int)selectedItem["ID"]) as ItemRow;

                if (String.IsNullOrEmpty(this.ItemName))
                { MessageBox.Show("يجب ادخال اسم الصنف", "", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                if (String.IsNullOrEmpty(this.ItemCode))
                { MessageBox.Show("يجب ادخال رقم الصنف", "", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                if (String.IsNullOrEmpty(this.ItemGroup))
                { MessageBox.Show("يجب ادخال مجموعة الصنف", "", MessageBoxButton.OK, MessageBoxImage.Error); return; }



                modifiedRow.Name = this.ItemName;
                modifiedRow.Code = int.Parse(this.ItemCode);
                modifiedRow.Note = this.ItemDetails;
                modifiedRow.GroupID = itemTableModel.GetGroup(this.ItemGroup).Item1;

                modifiedRow.GroupName = this.ItemGroup;

                if (!String.IsNullOrEmpty(this.ItemBalance))
                    modifiedRow.Balance = double.Parse(this.ItemBalance);



                modifiedRow.BarCode = this.ItemBarCode;
                modifiedRow.Unit = this.ItemUnit;
                modifiedRow.ModeDate = this.ItemModDate;
                modifiedRow.Brand = this.ItemBrand;
                modifiedRow.State = this.ItemState;
                modifiedRow.EnglishName = this.ItemEnglish;

            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }

        private void Insert()
        {
            try
            {

                ItemRow itemRow = this.ItemTable.NewItemRow();

                itemRow.Code = int.Parse(this.ItemCode);
                itemRow.Note = this.ItemDetails;
                itemRow.GroupID = itemTableModel.GetGroup(this.ItemGroup).Item1;

                itemRow.GroupName = this.ItemGroup;

                if (!String.IsNullOrEmpty(this.ItemBalance))
                    itemRow.Balance = double.Parse(this.ItemBalance);


                itemRow.BarCode = this.ItemBarCode;
                itemRow.Unit = this.ItemUnit;
                itemRow.ModeDate = this.ItemModDate;
                itemRow.Brand = this.ItemBrand;
                itemRow.State = this.ItemState;
                itemRow.EnglishName = this.ItemEnglish;

                this.ItemTable.AddItemRow(itemRow);
                this.IsSaving = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }
    }
    public class GridViewColumnsVisibility : BindableBase
    {
       
    }
}
