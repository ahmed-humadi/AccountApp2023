using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesLib
{
    public class CurrencyTable : DataTable
    {
        private DataColumn columnID;

        private DataColumn columnName;
        private DataColumn columnPartName;
        private DataColumn columnCurrencyValue;
        private DataColumn columnNumber;
        private string[] columnsName;
        public CurrencyTable() // costum columns
        {
            this.TableName = "CurrencyTable";
            this.columnsName = new string[] { "ID", "Name" ,"PartName", "CurrencyValue", "Number"};
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        public string[] ColumnsName { get => columnsName; }
        public DataColumn ColumnID { get => columnID; }
        public DataColumn ColumnName { get => columnName; }
        public DataColumn ColumnPartName { get => columnPartName; }
        public DataColumn ColumnCurrencyValue { get => columnCurrencyValue; }
        public DataColumn ColumnNumber { get => columnNumber; }

        public int Count { get { return this.Rows.Count; } }
        public DataRowCollection AccountRows { get { return this.Rows; } }
        public CurrencyRow this[int index]
        {
            get { return ((CurrencyRow)(this.Rows[index])); }
        }
        public void AddCurrencyRow(CurrencyRow accountRow)
        {
            this.Rows.Add(accountRow);
        }
        public CurrencyRow NewCurrencyRow()
        {
            return (CurrencyRow)this.NewRow();
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
                if (dataColumn.ColumnName == "PartName")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = 500;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = true;
                    // add column
                    this.columnPartName = dataColumn;
                }
                if (dataColumn.ColumnName == "CurrencyValue")
                {
                    dataColumn.DataType = typeof(float);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // add column
                    this.columnCurrencyValue = dataColumn;
                }
                if (dataColumn.ColumnName == "Number")
                {
                    dataColumn.DataType = typeof(int);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set unique
                    dataColumn.Unique = true;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // add column
                    this.columnNumber = dataColumn;
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
            return typeof(CurrencyRow);
        }
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CurrencyRow(builder);
        }
    }
    public class CurrencyRow : DataRow
    {
        private CurrencyTable table;
        internal CurrencyRow(DataRowBuilder dataRowBuilder)
            : base(dataRowBuilder)
        {
            this.table = (CurrencyTable)this.Table;
        }
        public DataColumnCollection Columns
        {
            get { return this.table.Columns; }
        }
        public int ID
        {
            get { return (int)this[this.table.ColumnID]; }
            set { this[this.table.ColumnID] = value; }
        }
        public string Name
        {
            get
            {
                return ((string)(this[this.table.ColumnName]));
            }
            set
            {
                this[this.table.ColumnName] = value;
            }
        }
        public string PartName
        {
            get
            {
                return ((string)(this[this.table.ColumnPartName]));
            }
            set
            {
                this[this.table.ColumnPartName] = value;
            }
        }
        public float CurrencyValue
        {
            get
            {
                return ((float)(this[this.table.ColumnCurrencyValue]));
            }
            set
            {
                this[this.table.ColumnCurrencyValue] = value;
            }
        }
        public int Number
        {
            get
            {
                return ((int)(this[this.table.ColumnNumber]));
            }
            set
            {
                this[this.table.ColumnNumber] = value;
            }
        }
    }
}

