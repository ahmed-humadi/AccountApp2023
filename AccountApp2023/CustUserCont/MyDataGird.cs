using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountApp2023.CustUserCont
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AccountApp2023.CustUserCont"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AccountApp2023.CustUserCont;assembly=AccountApp2023.CustUserCont"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MyDataGird/>
    ///
    /// </summary>
    public class MyDataGird : DataGrid
    {
        
        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> CellTextChanged;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnCellTextChanged(EventArgs e)
        {
            CellTextChanged?.Invoke(this, e);
        }
        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> CombBxCellTextChanged;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnCombBxCellTextChanged(EventArgs e)
        {
            CombBxCellTextChanged?.Invoke(this, e);
        }
        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> CombBxCellSelectionChanged;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnCombBxCellSelectionChanged(EventArgs e)
        {
            CombBxCellSelectionChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> CombBxDropClosed;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnCombBxDropClosed(EventArgs e)
        {
            CombBxDropClosed?.Invoke(this, e);
        }

        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> DebitTextChanged;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnDebitTextChanged(EventArgs e)
        {
            DebitTextChanged?.Invoke(this, e);
        }
        /// <summary>
        ///     Raised just before cell editing is ended.
        ///     Gives handlers the opportunity to cancel the operation.
        /// </summary>
        public event EventHandler<EventArgs> CreditTextChanged;

        /// <summary>
        ///     Called just before cell editing is ended.
        ///     Gives subclasses the opportunity to cancel the operation.
        /// </summary>
        public virtual void OnCreditTextChanged(EventArgs e)
        {
            CreditTextChanged?.Invoke(this, e);
        }
    }
}
