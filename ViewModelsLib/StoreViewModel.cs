using System;
using System.Collections.Generic;
using System.Data;
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

        private StoreTableModel storeModel;

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
        public int SelectedID
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
            get => isEditing;
            set
            {
                SetProperty(ref isEditing, value);
                ((DelegateCommand)ModCommand).RaiseCanExecuteChanged();
            }
        }
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand ModCommand { get; set; }
        public ICommand InsertCommand { get; set; }
        public ICommand LoadedStoreViewCommand { get; set; }
        public ICommand SelectionChangedListViewCommand { get; set; }
        public StoreViewModel()
        {
            storeModel = new StoreTableModel(); 

            SaveCommand = new DelegateCommand(Save, () => IsSaving);
            ModCommand = new DelegateCommand(Modify, () => IsEditing);
            InsertCommand = new DelegateCommand(Inser);
            LoadedStoreViewCommand = new DelegateCommand(LoadedStoreView);
            SelectionChangedListViewCommand = new DelegateCommand(SelectionChangedListView);
        }

        private void Inser()
        {
            try
            {
                IsSaving = true;

                StoreRow categoryRow = this.StoreTable.NewStoreRow();

                categoryRow["Name"] = this.StoreName;

                this.StoreTable.AddStoreRow(categoryRow);

            }
            catch (ConstraintException cos)
            {
                MessageBox.Show($"يوجد عنصر مشابة + {cos.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            finally
            {

            }
        }

        private void SelectionChangedListView(object parameter)
        {
            try
            {
                if (parameter is null)
                    return;
                System.Windows.Controls.ListView listView = parameter as System.Windows.Controls.ListView;

                if (listView.SelectedItem is null)
                {
                    this.StoreName = string.Empty;
                    return;
                }

                DataRowView selectedRow = listView.SelectedItem as DataRowView;

                if (!(selectedRow is null))
                {
                    IsEditing = true;
                    this.StoreName = selectedRow["Name"] as string;
                    this.SelectedID = (int)selectedRow["ID"];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private async void LoadedStoreView()
        {
            try
            {
                IsLoading = true;
                IsSaving = false;
                isEditing = true;
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.StoreTable = storeModel.GetStores();
                    });

                });
                IsLoading = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        private void Modify()
        {
            try
            {
                IsSaving = true;
                if (this.StoreTable.Count == 0)
                    return;

                StoreRow storeRow = (StoreRow)StoreTable.Rows.Find(this.SelectedID);

                storeRow.Name = this.StoreName;

                MessageBox.Show("تم تعديل الحساب لحفظ التغيرات يرجاء الضغط على زر الحفظ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        private async void Save()
        {
            try
            {
                IsLoading = true;
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        storeModel.UpdateStoreTable(this.StoreTable);
                        this.StoreTable.AcceptChanges();
                    });
                });
                IsLoading = false;
                IsSaving = false;
                IsEditing = false;
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }

        }
    }
}
