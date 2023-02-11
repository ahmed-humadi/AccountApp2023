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
        private string itemName;
        public string ItemName
        {
            get => itemName;
            set
            {
                 SetProperty(ref itemName, value);
               
            }
        }
        private string itemID;
        public string ItemID
        {
            get => itemID;
            set
            {
                SetProperty(ref itemID, value);

            }
        }
        private string itemCode;
        public string ItemCode
        {
            get => itemCode;
            set
            {
                SetProperty(ref itemCode, value);

            }
        }
        private string itemBrand;
        public string ItemBrand
        {
            get => itemBrand;
            set
            {
                SetProperty(ref itemBrand, value);

            }
        }
        private string itemGroup;
        public string ItemGroup
        {
            get => itemGroup;
            set
            {
                SetProperty(ref itemGroup, value);
            }
        }
        private int selectedItemGroupID;
        public int SelectedItemGroupID
        {
            get => selectedItemGroupID;
            set
            {
                SetProperty(ref selectedItemGroupID, value);
            }
        }
        private bool itemGroupIsDroped;
        public bool ItemGroupIsDropedOpen
        {
            get => itemGroupIsDroped;
            set
            {
                SetProperty(ref itemGroupIsDroped, value);
            }
        }
        private string itemEnglish;
        public string ItemEnglish
        {
            get => itemEnglish;
            set
            {
                SetProperty(ref itemEnglish, value);
            }
        }
        private string itemState;
        public string ItemSate
        {
            get => itemState;
            set
            {
                SetProperty(ref itemState, value);
            }
        }
        private string itemDate;
        public string ItemDate
        {
            get => itemDate;
            set
            {
                SetProperty(ref itemDate, value);
            }
        }
        private string itemDetails;
        public string ItemDetails
        {
            get => itemDetails;
            set
            {
                SetProperty(ref itemDetails, value);
            }
        }
        private string itemUnit;
        public string ItemUnit
        {
            get => itemUnit;
            set
            {
                SetProperty(ref itemUnit, value);
            }
        }
        private string itemModDate;
        public string ItemModDate
        {
            get => itemModDate;
            set
            {
                SetProperty(ref itemModDate, value);
            }
        }
        private bool isSaving;

        private bool isEditing;

        private bool isLoading;
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
        #endregion
        #region DelegateCommands
        public ICommand InsertCommand { get; set; }
        public ICommand ModCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ViewLoadedCommand { get; set; }
        public ICommand GroupSelectionChangedCommand { get; set; }
        public ICommand GroupTextChangedCommand { get; set; }

        #endregion
        public ItemTableViewModel()
        {
            itemTableModel = new ItemTableModel();
            InsertCommand = new DelegateCommand(Insert);
            ModCommand = new DelegateCommand(Modify, () => IsEditing);
            SaveCommand = new DelegateCommand(Save, () => IsSaving);
            ViewLoadedCommand = new DelegateCommand(ViewLoaded);
            GroupSelectionChangedCommand = new DelegateCommand(GroupSelectionChanged);
            GroupTextChangedCommand = new DelegateCommand(GroupTextChanged);

        }

        private void GroupTextChanged()
        {
            try
            {
                string st = this.ItemGroup;
                this.ItemGroupIsDropedOpen = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void GroupSelectionChanged()
        {
            try
            {
                if(String.IsNullOrEmpty(this.ItemGroup))
                {
                   // this.SelectedItemGroupID = itemTableModel.GetGroup(this.ItemGroup).Item1;
                }
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
                IsLoading = true;

                await Task.Run(() =>
                {
                    //System.Windows.Application.Current
                    itemTableModel.GetItems();
                });

            }catch(Exception ex)
            {

                MessageBox.Show(ex.Message);

            }finally
            {
                IsLoading = false;
            }
        }

        private void Save()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void Modify()
        {
            try
            {

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

                itemRow.Name = this.itemName;
                itemRow.Code = int.Parse(this.ItemCode);
                itemRow.Note = this.ItemDetails;
                itemRow.GroupID = itemTableModel.GetGroup(this.ItemGroup).Item1;

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}
