using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLib;
using BusinessLib;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using DataEntitiesLib;
using FrameWorkLib;
using System.Windows.Input;
using System.Data;
using System.Windows.Controls;

namespace ViewModelsLib
{
    public class EndAccountsViewModel : IDisposable , INotifyPropertyChanged
    {
        private EndAccountsTableModel endAccountModel;

        private bool disposedValue = false;
       
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand InsertAccountCommand { get; set; }
        public ICommand ShowAccountCommand { get; set; }
        public ICommand ModifyAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public EndAccountsViewModel()
        {
            endAccountModel = new EndAccountsTableModel();
            InsertAccountCommand = new DelegateCommand(InsertAccount);
            ShowAccountCommand = new DelegateCommand(ShowAccount);
            ModifyAccountCommand = new DelegateCommand(ModifyAccount);
            SaveAccountCommand = new DelegateCommand(SaveEndAccount);
        }
        private void ShowAccount(object parameter)
        {
            try
            {
                ComboBox comboBox = parameter as ComboBox;

                List<Tuple<string, int>> list = new List<Tuple<string, int>>();
                endAccountModel.GetEndAccounts(list);
              
                comboBox.ItemsSource = list;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void InsertAccount()
        {
            //try
            //{
            //    EndAccountsRow newEndAccountRow = EndAccountsTable.NewEndAccountRow();
            //    newEndAccountRow.ID = Guid.NewGuid();
            //    newEndAccountRow.EndAccountName = EndAccount.EndAccountName;
            //    newEndAccountRow.EndAccountCode = EndAccount.EndAccountCode;
            //    newEndAccountRow.EndAccountModDate = EndAccount.EndAccountModDate;
            //    EndAccountsTable.EndAccountRows.Add(newEndAccountRow);
            //    MessageBox.Show("تم اضافة حساب جديد لحفظ الحساب اضغط حفظ او جديد لادراج حساب اخر");
            //}
            //catch (ArgumentNullException)
            //{
            //    MessageBox.Show("يوجد حقل فارغ");
            //}
            //catch (ConstraintException)
            //{
            //    MessageBox.Show("يوجد حساب مشابة");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void ModifyAccount()
        {
            //if (!(SelectedEndAccount is null))
            //{
            //    try
            //    {
            //        (SelectedEndAccount.Row as EndAccountsRow).EndAccountName = EndAccount.EndAccountName;
            //        (SelectedEndAccount.Row as EndAccountsRow).EndAccountCode = EndAccount.EndAccountCode;
            //        (SelectedEndAccount.Row as EndAccountsRow).EndAccountModDate = EndAccount.EndAccountModDate;
            //        MessageBox.Show("تم تعديل الحساب  لحفظ الحساب اضغط حفظ او جديد لادراج حساب اخر");
            //    }
            //    catch (ArgumentNullException)
            //    {
            //        MessageBox.Show("يوجد حقل فارغ");
            //    }
            //    catch (ConstraintException)
            //    {
            //        MessageBox.Show("يوجد حساب مشابة");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
        private void SaveEndAccount()
        {
            //try
            //{
            //    if (MessageBox.Show("هل تريد حفظ التغيرات", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //    {
            //        //BusinessLogic.UpdateEndAccountsTable(EndAccountsTable);
            //        MessageBox.Show("تم حفظ التغيرات", "تأكيد");
            //        EndAccountsTable.AcceptChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
               
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                InsertAccountCommand = null;
                ShowAccountCommand = null;
                ModifyAccountCommand = null;
                SaveAccountCommand = null;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
