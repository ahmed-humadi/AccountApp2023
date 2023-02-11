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
    public class ItemTableViewModel1 : BindableBase
    {
        private ItemTableModel itemTableMode;
        private bool isNewItem;
        private bool isAnyPropertyChanged = false;
        public event EventHandler Animation;
        #region Proprties

        private bool isItemNameTxtBxFocused;

        public bool IsItemNameTxtBxFocused
        {
            get { return isItemNameTxtBxFocused; }
            set
            {
                SetProperty(ref isItemNameTxtBxFocused, value);
            }
        }

        private string searchToken;
        public string SearchToken
        {
            get => searchToken;
            set
            {
                SetProperty(ref searchToken, value);
            }
        }
        private ItemTable itemTable;
        public ItemTable ItemTable
        {
            get => itemTable;
            set
            {
                SetProperty(ref itemTable, value);
            }
        }
        private string itemName;
        public string ItemName
        {
            get => itemName;
            set
            {
                SetProperty(ref itemName, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }
        private int itemID;
        public int ItemID
        {
            get => itemID;
            set
            {
                itemID = value;

            }
        }
        private string itemCode;
        public string ItemCode
        {
            get => itemCode;
            set
            {
                SetProperty(ref itemCode, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }
        private string itemBrand;
        public string ItemBrand
        {
            get => itemBrand;
            set
            {
                SetProperty(ref itemBrand, value);
                isAnyPropertyChanged = true;
            }
        }
        private string itemGroup;
        public string ItemGroup
        {
            get => itemGroup;
            set
            {
                SetProperty(ref itemGroup, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }

        private string itemStore;
        public string ItemStore
        {
            get => itemStore;
            set
            {
                SetProperty(ref itemStore, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }

        private string itemEnglish;
        public string ItemEnglish
        {
            get => itemEnglish;
            set
            {
                SetProperty(ref itemEnglish, value);
                isAnyPropertyChanged = true;
            }
        }
        private string itemModDate;
        public string ItemModDate
        {
            get => itemModDate;
            set
            {
                SetProperty(ref itemModDate, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }
        private string itemBalance;
        public string ItemBalance
        {
            get => itemBalance;
            set
            {
                SetProperty(ref itemBalance, value);
                IsSaving = true;
                isAnyPropertyChanged = true;
            }
        }
        private string itemState;
        public string ItemState
        {
            get => itemState;
            set
            {
                SetProperty(ref itemState, value);
                isAnyPropertyChanged = true;
            }
        }
        private string itemDetails;
        public string ItemDetails
        {
            get => itemDetails;
            set
            {
                SetProperty(ref itemDetails, value);
                isAnyPropertyChanged = true;
            }
        }
        private string itemUnit;
        public string ItemUnit
        {
            get => itemUnit;
            set
            {
                SetProperty(ref itemUnit, value);
                isAnyPropertyChanged = true;
            }
        }
        private string itemBarCode;
        public string ItemBarCode
        {
            get => itemBarCode;
            set
            {
                SetProperty(ref itemBarCode, value);
                isAnyPropertyChanged = true;
            }
        }
        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
            }
        }
        private bool isSaving = false;
        public bool IsSaving
        {
            get => isSaving;
            set
            {
                isSaving = value;
                ((DelegateCommand)SaveItemCommand).RaiseCanExecuteChanged();
            }
        }

        private int selectedRowIndex;
        public int SelectedRowIndex
        {
            get => selectedRowIndex;
            set
            {
                SetProperty(ref selectedRowIndex, value);
            }
        }

        private int selectedItemGroupID;
        public int SelectedItemGroupID
        {
            get => selectedItemGroupID;
            set
            {
                selectedItemGroupID = value;
            }
        }

        private int selectedItemStoreID;
        public int SelectedItemStoreID
        {
            get => selectedItemStoreID;
            set
            {
                selectedItemStoreID = value;
            }
        }
        private List<Tuple<int, string>> groupsList;
        public List<Tuple<int, string>> GroupsList
        {
            get => groupsList;
            set
            {
                SetProperty(ref groupsList, value);
            }
        }

        private List<Tuple<int, string>> storesList;
        public List<Tuple<int, string>> StoresList
        {
            get => storesList;
            set
            {
                SetProperty(ref storesList, value);
            }
        }
        private bool isLoading_Group = false;
        public bool IsLoading_Group
        {
            get => isLoading_Group;
            set
            {
                SetProperty(ref isLoading_Group, value);
            }
        }

        private bool isLoading_Store = false;
        public bool IsLoading_Store
        {
            get => isLoading_Store;
            set
            {
                SetProperty(ref isLoading_Store, value);
            }
        }
        private bool itemGroupIsDropedOpen = false;
        public bool ItemGroupIsDropedOpen
        {
            get => itemGroupIsDropedOpen;
            set
            {
                SetProperty(ref itemGroupIsDropedOpen, value);
            }
        }

        private bool itemStoreIsDropedOpen = false;
        public bool ItemStoreIsDropedOpen
        {
            get => itemStoreIsDropedOpen;
            set
            {
                SetProperty(ref itemStoreIsDropedOpen, value);
            }
        }
        #region// columns visibility
        private bool isVisibleCode = true;
        public bool IsVisibleCode
        {
            get => isVisibleCode;
            set
            {
                SetProperty(ref isVisibleCode, value);
            }
        }
        private bool isVisibleName = true;
        public bool IsVisibleName
        {
            get => isVisibleName;
            set
            {
                SetProperty(ref isVisibleName, value);
            }
        }
        private bool isVisibleGroupName = true;
        public bool IsVisibleGroupName
        {
            get => isVisibleGroupName;
            set
            {
                SetProperty(ref isVisibleGroupName, value);
            }
        }
        private bool isVisibleNote = true;
        public bool IsVisibleNote
        {
            get => isVisibleNote;
            set
            {
                SetProperty(ref isVisibleNote, value);
            }
        }
        private bool isVisibleBalance = true;
        public bool IsVisibleBalance
        {
            get => isVisibleBalance;
            set
            {
                SetProperty(ref isVisibleBalance, value);
            }
        }
        private bool isVisibleBarCode = true;
        public bool IsVisibleBarCode
        {
            get => isVisibleBarCode;
            set
            {
                SetProperty(ref isVisibleBarCode, value);
            }
        }
        private bool isVisibleUnit = true;
        public bool IsVisibleUnit
        {
            get => isVisibleUnit;
            set
            {
                SetProperty(ref isVisibleUnit, value);
            }
        }
        private bool isVisibleModDay = true;
        public bool IsVisibleModDay
        {
            get => isVisibleModDay;
            set
            {
                SetProperty(ref isVisibleModDay, value);
            }
        }
        private bool isVisibleBrand = true;
        public bool IsVisibleBrand
        {
            get => isVisibleBrand;
            set
            {
                SetProperty(ref isVisibleBrand, value);
            }
        }
        private bool isVisibleState = true;
        public bool IsVisibleState
        {
            get => isVisibleState;
            set
            {
                SetProperty(ref isVisibleState, value);
            }
        }
        private bool isVisibleEnglishName = true;
        public bool IsVisibleEnglishName
        {
            get => isVisibleEnglishName;
            set
            {
                SetProperty(ref isVisibleEnglishName, value);
            }
        }
        #endregion
        #endregion
        #region Commands
        public ICommand SaveItemCommand { get; set; }
        public ICommand GroupSelectionChangedCommand { get; set; }
        public ICommand GroupTextChangedCommand { get; set; }
        public ICommand GroupSearchCommand { get; set; }
        public ICommand StoreSearchCommand { get; set; }
        public ICommand CreateNewItemCommand { get; set; }
        public ICommand ModifyItemCommand { get; set; }
        public ICommand ItemTableViewLoadCommand { get; set; }
        public ICommand CloseFormCommand { get; set; }
        public ICommand ItemSearchCommand { get; set; }
        #endregion
        public ItemTableViewModel1()
        {
            itemTableMode = new ItemTableModel();
            SaveItemCommand = new DelegateCommand(SaveItem, () => CanExcute());
            GroupSearchCommand = new DelegateCommand(GroupSearch);
            StoreSearchCommand = new DelegateCommand(StoreSearch);
            GroupTextChangedCommand = new DelegateCommand(GroupTextChanged);
            GroupSelectionChangedCommand = new DelegateCommand(GroupSelectionChanged);
            CreateNewItemCommand = new DelegateCommand(CreateNewItem);
            ModifyItemCommand = new DelegateCommand(ModifyItem);
            ItemTableViewLoadCommand = new DelegateCommand(ItemTableViewLoad);
            CloseFormCommand = new DelegateCommand(CloseForm);
            ItemSearchCommand = new DelegateCommand(ItemSearch);

        }

        private async void StoreSearch()
        {
            try
            {
                //this.IsLoading_Group = true;

                //this.ItemGroupIsDropedOpen = true;
                //await Task.Run(() =>
                //{
                  //  System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    //{
                        this.StoresList = itemTableMode.GetStores1();
                  //  });
                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // ToDo create this proprty
                //this.IsLoading_Store = false;
            }
        }

        private async void ItemSearch()
        {
            try
            {
                string searchToken = this.searchToken;
                if (IsLoading)
                    return;
                this.IsLoading = true;

                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (String.IsNullOrEmpty(searchToken) || String.IsNullOrWhiteSpace(searchToken))
                        {
                            ItemTableViewLoad();
                        }
                        if (int.TryParse(searchToken, out int itemCode))
                        {
                            this.ItemTable = this.itemTableMode.GetItems(itemCode);
                            return;
                        }
                        if (DateTime.TryParse(searchToken, out DateTime itemDate))
                        {
                            this.ItemTable = this.itemTableMode.GetItems(searchToken, 2);
                            return;
                        }

                        this.ItemTable = this.itemTableMode.GetItems(searchToken, 1);
                    });
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        private bool CanExcute()
        {
            if (!String.IsNullOrEmpty(this.ItemName) && !String.IsNullOrWhiteSpace(this.ItemName)
                && !String.IsNullOrEmpty(this.ItemCode) && !String.IsNullOrWhiteSpace(this.ItemCode)
                && !String.IsNullOrEmpty(this.ItemGroup) && !String.IsNullOrWhiteSpace(this.ItemGroup)
                && !String.IsNullOrEmpty(this.ItemModDate) && !String.IsNullOrWhiteSpace(this.ItemModDate))
            {
                return true;
            }
            return false;
        }
        private void CloseForm()
        {
            try
            {
                if(isAnyPropertyChanged)
                {
                    if (MessageBox.Show("هل تريد حفظ التغيرات", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                    {
                        SaveItem();
                    }
                    else
                    {
                        StoryboardManager.PlayStoryboard("HideItemAnimation");
                    }
                }
                else
                {
                    StoryboardManager.PlayStoryboard("HideItemAnimation");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void ItemTableViewLoad()
        {
            try
            {
                this.IsLoading = true;
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.ItemTable = itemTableMode.GetItems();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        private void ModifyItem()
        {
            try
            {
                isNewItem = false;
                ItemRow modItemRow = this.ItemTable.Rows[this.SelectedRowIndex] as ItemRow;
                this.ItemID = (int)modItemRow["ID"];
                this.ItemName = modItemRow["Name"].ToString();
                this.ItemCode = modItemRow["Code"].ToString();
                this.ItemBrand = modItemRow["Brand"].ToString();
                this.ItemGroup = modItemRow["GroupName"].ToString();
                // why i call it selected bcs at the begining it is selected from the combx
                this.SelectedItemGroupID = (int)modItemRow["GroupID"]; 
                this.ItemEnglish = modItemRow["EnglishName"].ToString();
                this.ItemUnit = modItemRow["Unit"].ToString();
                this.ItemBalance = modItemRow["Balance"].ToString();
                this.ItemBarCode = modItemRow["BarCode"].ToString();
                this.ItemDetails = modItemRow["Note"].ToString();
                this.ItemState = modItemRow["State"].ToString();
                this.ItemModDate = modItemRow["ModDate"].ToString();
                this.GroupsList = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                isAnyPropertyChanged = false;
                OnAnimation("ShownItemFormGrid");
            }
        }
        private void CreateNewItem()
        {
            try
            {
                isNewItem = true;
                this.ItemName = null;
                this.ItemCode = null;
                this.ItemBrand = null;
                this.ItemGroup = null;
                this.GroupsList = null;
                this.ItemEnglish = null;
                this.ItemUnit = "حبة";
                this.ItemBalance = null;
                this.ItemBarCode = null;
                this.ItemDetails = null;
                this.ItemState = null;
                this.ItemModDate = DateTime.Today.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                isAnyPropertyChanged = false;
                StoryboardManager.PlayStoryboard("ShowItemAnimation");
                IsItemNameTxtBxFocused = true;
            }
        }        /// <summary>
        /// this just to clear the the list after selection is made
        /// since it did not work from the MycomboBox class
        /// </summary>
        private void GroupSelectionChanged()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void GroupTextChanged()
        {
            try
            {
                this.IsLoading_Group = true;
                this.ItemGroupIsDropedOpen = true;
                string name = this.ItemGroup;
                if(String.IsNullOrEmpty(name))
                {
                    this.ItemGroupIsDropedOpen = false;
                    this.GroupsList = null;
                    return;
                }
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.GroupsList = this.itemTableMode.GetGroups1(name);
                    });
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Group = false;
            }
        }
        private async void GroupSearch()
        {
            try
            {
                this.IsLoading_Group = true;

                this.ItemGroupIsDropedOpen = true;
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.GroupsList = itemTableMode.GetGroups1();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading_Group = false;
            }
        }
        private void SaveItem()
        {
            try
            {
                if (isNewItem)
                {

                    // when the save clear the account table 
                    this.ItemTable.Clear();
                    // create new row
                    ItemRow newItemRow = this.ItemTable.NewItemRow();
                    newItemRow["Code"] = this.ItemCode;
                    newItemRow["Name"] = this.ItemName;
                    newItemRow["GroupID"] = this.SelectedItemGroupID;
                    newItemRow["Note"] = this.ItemDetails;
                    // this is updated from the bills
                    newItemRow["Balance"] = 0;//this.ItemBalance;
                    if (this.ItemBarCode.Contains(" "))
                        this.ItemBarCode = this.ItemBarCode.Replace(" ", "");
                    newItemRow["BarCode"] = this.ItemBarCode;
                    newItemRow["Unit"] = this.ItemUnit;
                    newItemRow["ModDate"] = this.ItemModDate;
                    newItemRow["Brand"] = this.ItemBrand;
                    // this is updated from the bills
                    newItemRow["State"] = "لايوجد";
                    newItemRow["EnglishName"] = this.ItemEnglish;
                    // this is updated from the bills
                    newItemRow["StoreID"] = 0;
                    this.ItemTable.AddItemRow(newItemRow);
                    // save
                    this.itemTableMode.UpdateItemTable(this.ItemTable);

                    // after load the view againt to see the changes
                    ItemTableViewLoad();

                    MessageBox.Show("تم الحفظ");
                    //OnAnimation("HideItemFormGrid");
                    StoryboardManager.PlayStoryboard("HideItemAnimation");
                }
                else // save modifications
                {
                    this.ItemTable.Clear();
                    ItemRow newItemRow = this.ItemTable.NewItemRow();
                    newItemRow["ID"] = this.ItemID;
                    newItemRow["Code"] = this.ItemCode;
                    newItemRow["Name"] = this.ItemName;
                    newItemRow["GroupID"] = this.SelectedItemGroupID;
                    newItemRow["Note"] = this.ItemDetails;
                    newItemRow["Balance"] = this.ItemBalance;
                    if (this.ItemBarCode.Contains(" "))
                        this.ItemBarCode = this.ItemBarCode.Replace(" ", "");
                    newItemRow["BarCode"] = this.ItemBarCode;
                    newItemRow["Unit"] = this.ItemUnit;
                    newItemRow["ModDate"] = this.ItemModDate;
                    newItemRow["Brand"] = this.ItemBrand;
                    newItemRow["State"] = this.ItemState;
                    newItemRow["EnglishName"] = this.ItemEnglish;
                    this.ItemTable.AddItemRow(newItemRow);
                    this.ItemTable.AcceptChanges();
                    newItemRow.SetModified();
                    // save
                    this.itemTableMode.UpdateItemTable(this.ItemTable);

                    // after load the view againt to see the changes
                    ItemTableViewLoad();

                    MessageBox.Show("تم الحفظ");
                    //OnAnimation("HideItemFormGrid");
                    StoryboardManager.PlayStoryboard("HideItemAnimation");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        protected void OnAnimation(string animationName)
        {
            Animation?.Invoke(this, new AnimationEventArgs() {AnimationName = animationName });
        }
    }
    public class AnimationEventArgs : EventArgs
    {
        public string AnimationName { get; set; }
    }
}
