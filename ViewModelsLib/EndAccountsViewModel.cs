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
    public class EndAccountsViewModel : FrameWorkLib.BindableBase, IDisposable
    {
        private EndAccountsTableModel endAccountModel;

        private bool disposedValue = false;
       
        private List<string> endAccountCmBxList;
        public List<string> EndAccountCmBxList
        {
            get => endAccountCmBxList;
            set
            {
                SetProperty(ref endAccountCmBxList, value);
            }
        }

        private string selectedEndAccount;
        public string SelectedEndAccount
        {
            get => selectedEndAccount;
            set
            {
                SetProperty(ref selectedEndAccount, value);
            }
        }
        internal int selectedEndAccountID;
        internal int SelectedEndAccountID
        {
            get => selectedEndAccountID;
            set
            {
                SetProperty(ref selectedEndAccountID, value);
            }
        }
        public ICommand InsertAccountCommand { get; set; }
        public ICommand ShowAccountCommand { get; set; }
        public ICommand ModifyAccountCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public EndAccountsViewModel()
        {
            endAccountModel = new EndAccountsTableModel();
            InsertAccountCommand = new DelegateCommand(InsertAccount);
            ShowAccountCommand = new DelegateCommand(ShowAccount);
            ModifyAccountCommand = new DelegateCommand(ModifyAccount);
            SaveAccountCommand = new DelegateCommand(SaveEndAccount);
            SelectionChangedCommand = new DelegateCommand(SelectionChanged);
        }

        private void SelectionChanged()
        {
            try
            {
                if (String.IsNullOrEmpty(this.SelectedEndAccount))
                    return;
                this.SelectedEndAccountID = endAccountModel.GetAccount(this.SelectedEndAccount).Item2;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowAccount()
        {
            try
            {

                List<Tuple<string, int>> tuples = new List<Tuple<string, int>>();
                endAccountModel.GetEndAccounts(tuples);
                List<string> list = new List<string>();
                foreach(Tuple<string, int> tuple in tuples)
                {
                    list.Add(tuple.Item1);
                }
                EndAccountCmBxList = list;
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
