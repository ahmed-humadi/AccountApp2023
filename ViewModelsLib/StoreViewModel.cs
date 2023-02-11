using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DataEntitiesLib;
using FrameWorkLib;
using ModelsLib;

namespace ViewModelsLib
{
    public class StoreViewModel : ViewModelBase
    {
        private readonly object lockToken = new object();

        private StoreModel storeModel;

        private StoreTable storeTable;
        public StoreTable StoreTable
        {
            get => storeTable;
            set
            {
                lock (lockToken)
                {
                    SetProperty(ref storeTable, value);
                }
            }
        }

        private string storeName;
        public string StoreName
        {
            get => storeName;
            set
            {
                SetProperty(ref storeName, value);
            }
        }

        private int selectedID;
        public int SelectedId
        {
            get => selectedID;
            set
            {
                SetProperty(ref selectedID, value);
            }
        }

        private bool isSvaing;
        public bool IsSaving
        {
            get => isSvaing;
            set
            {
                SetProperty(ref isSvaing, value);
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private bool isEditing;
        public bool IsEditing
        {
            get => isSvaing;
            set
            {
                SetProperty(ref isSvaing, value);
                ((DelegateCommand)ModCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand ModCommand { get; set; }
        public ICommand InsertCommand { get; set; }
        public ICommand LoadedCategoryViewCommand { get; set; }
        public ICommand SelectionChangedListViewCommand { get; set; }
        public StoreViewModel()
        {
            storeModel = new StoreModel(); 

            SaveCommand = new DelegateCommand(Save, () => IsSaving);
            ModCommand = new DelegateCommand(Modify, () => IsEditing);
            LoadedCategoryViewCommand = new DelegateCommand(LoadedCategoryView);
            SelectionChangedListViewCommand = new DelegateCommand(SelectionChangedListView);
        }

        private void SelectionChangedListView()
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void LoadedCategoryView()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Save()
        {
            throw new NotImplementedException();

        }
    }
}
