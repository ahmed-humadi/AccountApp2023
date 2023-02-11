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
