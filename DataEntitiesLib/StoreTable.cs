using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesLib
{
    public class StoreTable : DataTable
    {
        private DataColumn columnID;

        private DataColumn columnName;

        private string[] columnsName;
        public StoreTable() // costum columns
        {
            this.TableName = "StoreTable";
            this.columnsName = new string[] { "ID", "Name" };
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        public string[] ColumnsName { get => columnsName; }
        public DataColumn ColumnID { get => columnID; }
        public DataColumn ColumnName { get => columnName; }
        public int Count { get { return this.Rows.Count; } }
        public DataRowCollection StoreRows { get { return this.Rows; } }
        public StoreRow this[int index]
        {
            get { return ((StoreRow)(this.Rows[index])); }
        }
        public void AddStoreRow(StoreRow accountRow)
        {
            this.Rows.Add(accountRow);
        }
        public StoreRow NewStoreRow()
        {
            return (StoreRow)this.NewRow();
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
                    dataColumn.AllowDBNull = false;
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
            return typeof(StoreRow);
        }
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new StoreRow(builder);
        }
    }
    public class StoreRow : DataRow
    {
        private StoreTable tableAccounts;
        internal StoreRow(DataRowBuilder dataRowBuilder)
            : base(dataRowBuilder)
        {
            this.tableAccounts = (StoreTable)this.Table;
        }
        public DataColumnCollection Columns
        {
            get { return this.tableAccounts.Columns; }
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
    }
}

