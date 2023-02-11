using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrameWorkLib
{
    public class ViewModelBase : BindableBase, IViewModelBase
    {
        private ViewModes viewMode;
        public ViewModes ViewMode 
        {
            get { return viewMode; } 
            set
            {
                viewMode = value;
                RaiseViewModeChanged();
            }
        }
        private bool isAnyProprtyChanged = false;
        public bool IsAnyProprtyChanged 
        { 
            get => isAnyProprtyChanged; 
            set => isAnyProprtyChanged = value; 
        }
        private bool isFirstTxtBxFocused;
        public bool IsFirstTxtBxFocused 
        { 
            get { return isFirstTxtBxFocused; } 
            set
            {
                SetProperty(ref isFirstTxtBxFocused, value);
            }
        }
        private bool isFirstContrlFocused;
        public bool IsFirstContrlFocused
        {
            get { return isFirstContrlFocused; }
            set
            {
                SetProperty(ref isFirstContrlFocused, value);
            }
        }
        private bool isNew = false;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                ((DelegateCommand)NewCommand).RaiseCanExecuteChanged();
            }
        }

        private bool isSaving = false;
        public bool IsSaving 
        {
            get { return isSaving; } 
            set
            {
                isSaving = value;
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private bool isEditting = false;
        public bool IsEditting
        {
            get => isEditting;
            set
            {
                isEditting = value;
                ((DelegateCommand)ModCommand).RaiseCanExecuteChanged();
            }
        }
        private bool isDeleting = false;
        public bool IsDeleting
        {
            get => isDeleting;
            set
            {
                isDeleting = value;
                ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
        private bool isUpdate = true;
        public bool IsUpdate
        {
            get { return isUpdate; }
            set
            {
                isUpdate = value;
            }
        }
        private bool enableEditing = false;
        public bool EnableEditing
        {
            get => enableEditing;
            set
            {
                SetProperty(ref enableEditing, value);
            }
        }
        private bool isLoading;
        public bool IsLoading 
        {
            get { return isLoading; } 
            set
            {
                SetProperty(ref isLoading, value);
            }
        }
        public ICommand ModCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand MaxCommand { get; set; }
        public ICommand MinCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand UnloadCommand { get; set; }
        public ViewModelBase()
        {
            LoadCommand = new DelegateCommand(LoadView);
            UnloadCommand = new DelegateCommand(UnloadView);
            ModCommand = new DelegateCommand(ModView, () => IsEditting);
            SaveCommand = new DelegateCommand(SaveView, () => IsSaving);
            DeleteCommand = new DelegateCommand(DeleteView, () => IsDeleting);
            NewCommand = new DelegateCommand(NewView, () => IsNew);
            PreviousCommand = new DelegateCommand(PreviousView);
            NextCommand = new DelegateCommand(NextView);
            MinCommand = new DelegateCommand(MinView);
            MaxCommand = new DelegateCommand(MaxView);
        }

        protected virtual void MaxView()
        {
            throw new NotImplementedException();
        }

        protected virtual void MinView()
        {
            throw new NotImplementedException();
        }

        protected virtual void NextView()
        {
            throw new NotImplementedException();
        }

        protected virtual void PreviousView()
        {
            throw new NotImplementedException();
        }

        protected virtual void UnloadView()
        {
            throw new NotImplementedException();
        }

        protected virtual void NewView()
        {
            throw new NotImplementedException();
        }

        protected virtual void DeleteView()
        {
            throw new NotImplementedException();
        }

        protected virtual void SaveView()
        {
            throw new NotImplementedException();

        }

        protected virtual void ModView()
        {
            throw new NotImplementedException();
        }

        protected virtual void LoadView()
        {
            throw new NotImplementedException();
        }
        public event EventHandler ViewModeChanged;
        protected void RaiseViewModeChanged()
        {
            ViewModeChanged?.Invoke(this, new EventArgs());
        }

        public virtual ErrorType CheckingFields()
        {
            throw new NotImplementedException();
        }
        public virtual void ToggleCommandBtns()
        {
            if(this.ViewMode == ViewModes.Editing)
            {

                this.IsFirstTxtBxFocused = true;
                this.IsFirstTxtBxFocused = false;

                IsUpdate = true;
                EnableEditing = true;

                IsEditting = false;
                IsDeleting = false;
                IsNew = false;
                IsSaving = true;

            }
            else
            {

                IsUpdate = false;
                EnableEditing = false;

                IsEditting = true;
                IsDeleting = true;
                IsNew = true;
                IsSaving = false;


                IsFirstContrlFocused = true;
                IsFirstContrlFocused = false;

                //Keyboard.ClearFocus();
            }
        }
        public virtual void InitializingFields()
        {
            throw new NotImplementedException();
        }
    }
}
