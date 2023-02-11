using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataEntitiesLib
{
    public class EndAccountsTable : DataTable, IDisposable
    {
        private DataColumn columnEndAccountID;
        private DataColumn columnEndAccountName;
        private DataColumn columnEndAccountCode;
        private DataColumn columnEndAccountModDate;
        private string[] columnsName;
        public string[] ColumnsName { get => columnsName; set => columnsName = value; }
        public DataColumn ColumnEndAccountID { get => columnEndAccountID; }
        public DataColumn ColumnEndAccountName { get => columnEndAccountName; }
        public DataColumn ColumnEndAccountCode { get => columnEndAccountCode; }
        public DataColumn ColumnEndAccountModDate { get => columnEndAccountModDate; }
        public DataRowCollection EndAccountRows
        {
            get
            {
                return this.Rows;
            }
        }
        public EndAccountsTable() // alll columns
        {
            this.TableName = "EndAccountsTable";
            this.columnsName = new string[] { "ID", "EndAccountName", "EndAccountCode", "EndAccountModDate" };
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        public EndAccountsRow this[int index]
        {
            get
            {
                return (EndAccountsRow)this.Rows[index];
            }
        }
        public void InitClass()
        {
            foreach (string columnName in columnsName)
            {
                DataColumn dataColumn = new DataColumn(columnName);
                this.Columns.Add(dataColumn);
                if (dataColumn.ColumnName == "ID")
                {
                    
                    dataColumn.DataType = typeof(Guid);
                    dataColumn.MaxLength = -1;
                    dataColumn.AllowDBNull = false;
                    dataColumn.Unique = true;
                    this.columnEndAccountID = dataColumn;
                }
                if (dataColumn.ColumnName == "EndAccountName")
                {
                    this.PrimaryKey = new DataColumn[] { dataColumn };
                    dataColumn.DataType = typeof(string);
                    dataColumn.MaxLength = 400;
                    dataColumn.AllowDBNull = false;
                    dataColumn.Unique = true;
                    this.columnEndAccountName = dataColumn;
                }
                if (dataColumn.ColumnName == "EndAccountCode")
                {
                    dataColumn.DataType = typeof(int);
                    dataColumn.MaxLength = -1;
                    dataColumn.AllowDBNull = false;
                    dataColumn.Unique = true;
                    this.columnEndAccountCode = dataColumn;

                }
                if (dataColumn.ColumnName == "EndAccountModDate")
                {
                    dataColumn.DataType = typeof(string);
                    dataColumn.MaxLength = 50;
                    dataColumn.AllowDBNull = false;
                    dataColumn.Unique = false;
                    this.columnEndAccountModDate = dataColumn;
                }
            }
        }
        public void AddEndAccountRow(EndAccountsRow endAccountsRow)
        {
            this.Rows.Add(endAccountsRow);
        }
        public EndAccountsRow NewEndAccountRow()
        {
            return (EndAccountsRow)this.NewRow();
        }
        protected override Type GetRowType()
        {
            return typeof(EndAccountsRow);
        }
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new EndAccountsRow(builder);
        }
    }
    public class EndAccountsRow : DataRow
    {
        private EndAccountsTable endAccountsTable;
        internal EndAccountsRow(DataRowBuilder dataRowBuilder)
            : base(dataRowBuilder)
        {
            endAccountsTable = (EndAccountsTable)this.Table;
        }
        public Guid ID
        {
            get
            {
                return (Guid)this[this.endAccountsTable.ColumnEndAccountID];
            }
            set
            {
                this[this.endAccountsTable.ColumnEndAccountID] = value;
            }
        }
        public string EndAccountName
        {
            get
            {
                return (string)this[this.endAccountsTable.ColumnEndAccountName];
            }
            set
            {
                this[this.endAccountsTable.ColumnEndAccountName] = value;
            }
        }
        public int EndAccountCode
        {
            get
            {
                return (int)this[this.endAccountsTable.ColumnEndAccountCode];
            }
            set
            {
                this[this.endAccountsTable.ColumnEndAccountCode] = value;
            }
        }
        public string EndAccountModDate
        {
            get
            {
                return (string)this[this.endAccountsTable.ColumnEndAccountModDate];
            }
            set
            {
                this[this.endAccountsTable.ColumnEndAccountModDate] = value;
            }
        }
    }
}
