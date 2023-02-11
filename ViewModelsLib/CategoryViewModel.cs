using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLib;
using FrameWorkLib;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DataEntitiesLib;
using System.Data;
using System.Windows;

namespace ViewModelsLib
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly object lockToken = new object();
        private CategoryTableModel categoryModel;

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                SetProperty(ref categoryName, value);
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

        private bool iSLoading;
        public bool ISLoading
        {
            get => iSLoading;
            set
            {
                SetProperty(ref iSLoading, value);
            }
        }
        private CategoryTable categoryTable;
        public CategoryTable CategoryTable
        {
            get => categoryTable;

            set
            {
                lock (lockToken)
                {
                    SetProperty(ref categoryTable, value);
                }
            }
        }
        private bool isSaving;
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
        public ICommand LoadedCategoryViewCommand { get; set; }
        public ICommand SelectionChangedListViewCommand { get; set; }
        public ICommand ModCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand InsertCommand { get; set; }
        public CategoryViewModel()
        {
            categoryModel = new CategoryTableModel();

            LoadedCategoryViewCommand = new DelegateCommand(LoadedCategoryView);
            SelectionChangedListViewCommand = new DelegateCommand(SelectionChangedListView);
            ModCommand = new DelegateCommand(Modify, () => IsEditing);
            SaveCommand = new DelegateCommand(Save, () => IsSaving);
            InsertCommand = new DelegateCommand(Insert);
        }
        private void Insert()
        {
            try
            {
                IsSaving = true;

                CategoryRow categoryRow = this.CategoryTable.NewCategoryRow();

                categoryRow["Name"] = this.CategoryName; 

                this.CategoryTable.AddCategoryRow(categoryRow);

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
            try
            {

                categoryModel.UpdateCategoryTable(this.CategoryTable);
                this.CategoryTable.AcceptChanges();

                MessageBox.Show("تم الحفظ");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsSaving = false;
                IsEditing = false;
            }
        }
        private void Modify()
        {
            try
            {
                IsSaving = true;
                if (this.CategoryTable.Count == 0)
                    return;

                CategoryRow categoryRow = (CategoryRow)CategoryTable.Rows.Find(this.SelectedID);

                categoryRow.Name = this.CategoryName;

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
        private void SelectionChangedListView(object parameter)
        {
            try
            {
                if (parameter is null)
                    return;
               System.Windows.Controls.ListView listView = parameter as System.Windows.Controls.ListView;

                if (listView.SelectedItem is null)
                {
                    this.CategoryName = string.Empty;
                    return;
                }

                DataRowView selectedRow = listView.SelectedItem as DataRowView;

                if (!(selectedRow is null))
                {
                    IsEditing = true;
                    this.CategoryName = selectedRow["Name"] as string;
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
        private async void LoadedCategoryView()
        {
            try
            {
                ISLoading = true;
                IsSaving = false;
                isEditing = true;
                await Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {

                        this.CategoryTable = categoryModel.GetCategories();
                    });

                });
                await Task.Delay(1000);
                ISLoading = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ISLoading = false;
            }
        }
    }
}
