using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrameWorkLib
{
    public interface IViewModelBase
    {
        
        event EventHandler ViewModeChanged;
        ViewModes ViewMode { get; set; }
        bool IsAnyProprtyChanged { get; set; }
        bool IsFirstTxtBxFocused { get; set; }
        bool IsSaving { get; set; }
        bool IsEditting { get; set; }
        bool IsDeleting { get; set; }
        bool IsUpdate { get; set; }
        bool EnableEditing { get; set; }
        bool IsLoading { get; set; }
        ICommand ModCommand { get; set; }
        ICommand NewCommand { get; set; }
        ICommand SaveCommand { get; set; }
        ICommand NextCommand { get; set; }
        ICommand PreviousCommand { get; set; }
        ICommand MaxCommand { get; set; }
        ICommand MinCommand { get; set; }
        ICommand LoadCommand { get; set; }
        ICommand UnloadCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ErrorType CheckingFields();
        void ToggleCommandBtns();
        void InitializingFields();
    }
}
