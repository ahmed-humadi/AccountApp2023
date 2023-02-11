using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesLib
{
    public class AccountTable : DataTable
    {
        private DataColumn columnID;

        private DataColumn columnName;

        private DataColumn columnParentID;

        private DataColumn columnCode;

        private DataColumn columnEndAccountID;

        private DataColumn columnDate;

        private DataColumn columnParentName;

        private DataColumn columnEndAccountName;

        private string[] columnsName;
        public AccountTable() // costum columns
        {
            this.TableName = "AccountTable";
            this.columnsName = new string[] { "ID", "Name", "ParentID", "Code", "EndAccountID", "Date", "ParentName", "EndAccountName" };
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        public AccountTable(params string[] columnsNames) // costum columns
        {
            this.TableName = "AccountTable";
            this.columnsName = columnsNames;
            this.BeginInit();
            this.InitClassGeneric();
            this.EndInit();
        }
        public string[] ColumnsName { get => columnsName; }
        public DataColumn ColumnEndAccountID { get => columnEndAccountID; }
        public DataColumn ColumnID { get => columnID; }
        public DataColumn ColumnName { get => columnName; }
        public DataColumn ColumnParentID { get => columnParentID; }
        public DataColumn ColumnCode { get => columnCode; }
        public DataColumn ColumnDate { get => columnDate; }
        public DataColumn ColumnParentName { get => columnParentName; set => columnParentName = value; }
        public DataColumn ColumnEndAccountName { get => columnEndAccountName; set => columnEndAccountName = value; }
        public int Count { get { return this.Rows.Count; } }
        public DataRowCollection AccountRows { get { return this.Rows; } }
        public AccountRow this[int index]
        {
            get { return ((AccountRow)(this.Rows[index])); }
        }
        public void AddAccountsRow(AccountRow accountRow)
        {
            this.Rows.Add(accountRow);
        }
        public AccountRow NewAccountRow()
        {
            return (AccountRow)this.NewRow();
        }
        private void InitClass()
        {
            foreach (string columnName in columnsName)
            {
                DataColumn dataColumn = new DataColumn(columnName);
                this.Columns.Add(dataColumn);
                if (dataColumn.ColumnName == "ID")
                {
                    this.PrimaryKey = new DataColumn[] { dataColumn };
                    // set data type
                    dataColumn.DataType = typeof(int);
                    //
                    dataColumn.AutoIncrement = true;
                    //
                    dataColumn.AutoIncrementStep = 1;
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = true;
                    // add column
                    this.columnID = dataColumn;
                }
                if (dataColumn.ColumnName == "Name")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = 500;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = true;
                    // add column
                    this.columnName = dataColumn;
                }
                if (dataColumn.ColumnName == "ParentID")
                {
                    dataColumn.DataType = typeof(int);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnParentID = dataColumn;
                }
                if (dataColumn.ColumnName == "Code")
                {
                    dataColumn.DataType = typeof(int);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = true;
                    // add column
                    this.columnCode = dataColumn;
                }
                if (dataColumn.ColumnName == "EndAccountID")
                {
                    dataColumn.DataType = typeof(int);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnEndAccountID = dataColumn;
                }
                if (dataColumn.ColumnName == "Date")
                {
                    dataColumn.DataType = typeof(DateTime);
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnDate = dataColumn;
                }
                if (dataColumn.ColumnName == "ParentName")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = 500;
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.ColumnParentName = dataColumn;
                }
                if (dataColumn.ColumnName == "EndAccountName")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = 500;
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.ColumnEndAccountName = dataColumn;
                }
            }
        }
        private void InitClassGeneric()
        {
            foreach (string columnName in columnsName)
            {
                DataColumn dataColumn = new DataColumn(columnName);
                this.Columns.Add(dataColumn);
            }
        }
        protected override Type GetRowType()
        {
            return typeof(AccountRow);
        }
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new AccountRow(builder);
        }
    }
    public class AccountRow : DataRow
    {
        private AccountTable tableAccounts;
        internal AccountRow(DataRowBuilder dataRowBuilder)
            : base(dataRowBuilder)
        {
            this.tableAccounts = (AccountTable)this.Table;
        }
        public DataColumnCollection Columns
        {
            get { return this.tableAccounts.Columns; }
            //set { this.tableAccounts.Columns = value; }
        }
        public int ID
        {
            get { return (int)this[this.tableAccounts.ColumnID]; }
            set { this[this.tableAccounts.ColumnID] = value; }
        }
        public string Name
        {
            get
            {
                return ((string)(this[this.tableAccounts.ColumnName]));
            }
            set
            {
                this[this.tableAccounts.ColumnName] = value;
            }
        }
        public int ParentID
        {
            get
            {
                return ((int)(this[this.tableAccounts.ColumnParentID]));
            }
            set
            {
                this[this.tableAccounts.ColumnParentID] = value;
            }
        }
        public int Code
        {
            get
            {
                return ((int)(this[this.tableAccounts.ColumnCode]));
            }
            set
            {
                this[this.tableAccounts.ColumnCode] = value;
            }
        }

        public int EndAccountID
        {
            get
            {
                return ((int)(this[this.tableAccounts.ColumnEndAccountID]));
            }
            set
            {
                this[this.tableAccounts.ColumnEndAccountID] = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return ((DateTime)(this[this.tableAccounts.ColumnDate]));
            }
            set
            {
                this[this.tableAccounts.ColumnDate] = value;
            }
        }
        public string ParentName
        {
            get
            {
                return ((string)(this[this.tableAccounts.ColumnParentName]));
            }
            set
            {
                this[this.tableAccounts.ColumnParentName] = value;
            }
        }
        public string EndAccountName
        {
            get
            {
                return ((string)(this[this.tableAccounts.ColumnEndAccountName]));
            }
            set
            {
                this[this.tableAccounts.ColumnEndAccountName] = value;
            }
        }
    }
}
