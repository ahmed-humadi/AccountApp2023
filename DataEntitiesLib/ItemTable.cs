using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesLib
{
    public class ItemTable : DataTable
    {
        private DataColumn columnID;

        private DataColumn columnCode;

        private DataColumn columnName;

        private DataColumn columnGroupID;

        private DataColumn columnNote;
       
        private DataColumn columnbalance;
                           
        private DataColumn columnbarCode;
                           
        private DataColumn columnunit;
                           
        private DataColumn columnmodDate;

        private string[] columnsName;
        public ItemTable() // costum columns
        {
            this.TableName = "ItemTable";
            this.columnsName = new string[] { "ID", "Code", "Name", "GroupID", "Note", "Balance", "BarCode", "Unit", "ModDate" };
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        public ItemTable(params string[] columnsNames) // costum columns
        {
            this.TableName = "ItemTable";
            this.columnsName = columnsNames;
            this.BeginInit();
            this.InitClassGeneric();
            this.EndInit();
        }
        public string[] ColumnsName { get => columnsName; set => columnsName = value; }
        public DataColumn ColumnNote { get => columnNote; set => columnNote = value; }
        public DataColumn ColumnID { get => columnID; set => columnID = value; }
        public DataColumn ColumnName { get => columnName; set => columnName = value; }
        public DataColumn ColumnGroupID { get => columnGroupID; set => columnGroupID = value; }
        public DataColumn ColumnCode { get => columnCode; set => columnCode = value; }
        public DataColumn ColumnBalance { get => columnbalance; set => columnbalance = value; }
        public DataColumn ColumnBarCode { get => columnbarCode; set => columnbarCode = value; }
        public DataColumn ColumnUnit { get => columnunit; set => columnunit = value; }
        public DataColumn ColumnModDate { get => columnmodDate; set => columnmodDate = value; }
        public int Count { get { return this.Rows.Count; } }
        public DataRowCollection ItemRows { get { return this.Rows; } }
        public ItemRow this[int index]
        {
            get { return ((ItemRow)(this.Rows[index])); }
        }
        public void AddItemRow(ItemRow Row)
        {
            this.Rows.Add(Row);
        }
        public ItemRow NewItemRow()
        {
            return (ItemRow)this.NewRow();
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
                else if (dataColumn.ColumnName == "Code")
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
                else if (dataColumn.ColumnName == "Name")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = 500;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnName = dataColumn;
                }
                else if(dataColumn.ColumnName == "GroupID")
                {
                    dataColumn.DataType = typeof(int);

                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnGroupID = dataColumn;
                }
                else if(dataColumn.ColumnName == "Note")
                {
                    dataColumn.DataType = typeof(int);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnNote = dataColumn;
                }
                else if (dataColumn.ColumnName == "Balance")
                {
                    dataColumn.DataType = typeof(double);
                    // set allow null
                    dataColumn.AllowDBNull = false;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnbalance = dataColumn;
                }
                else if (dataColumn.ColumnName == "BarCode")
                {
                    dataColumn.DataType = typeof(string);
                    // set data Max length
                    dataColumn.MaxLength = -1;
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnbarCode = dataColumn;
                }
                else if (dataColumn.ColumnName == "Unit")
                {
                    dataColumn.DataType = typeof(int);
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnbalance = dataColumn;
                }
                else if (dataColumn.ColumnName == "Balance")
                {
                    dataColumn.DataType = typeof(DateTime);
                    // set allow null
                    dataColumn.AllowDBNull = true;
                    // set unique
                    dataColumn.Unique = false;
                    // add column
                    this.columnbalance = dataColumn;
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
            return typeof(ItemRow);
        }
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ItemRow(builder);
        }
    }
    public class ItemRow : DataRow
    {
        private ItemTable table;
        internal ItemRow(DataRowBuilder dataRowBuilder)
            : base(dataRowBuilder)
        {
            this.table = (ItemTable)this.Table;
        }
        public DataColumnCollection Columns
        {
            get { return this.table.Columns; }
            //set { this.tableAccounts.Columns = value; }
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
        public int GroupID
        {
            get
            {
                return ((int)(this[this.table.ColumnGroupID]));
            }
            set
            {
                this[this.table.ColumnGroupID] = value;
            }
        }
        public int Code
        {
            get
            {
                return ((int)(this[this.table.ColumnCode]));
            }
            set
            {
                this[this.table.ColumnCode] = value;
            }
        }

        public string Note
        {
            get
            {
                return ((string)(this[this.table.ColumnNote]));
            }
            set
            {
                this[this.table.ColumnNote] = value;
            }
        }
        public double Balance
        {
            get
            {
                return ((double)(this[this.table.ColumnBalance]));
            }
            set
            {
                this[this.table.ColumnBalance] = value;
            }
        }
        public string BarCode
        {
            get
            {
                return ((string)(this[this.table.ColumnBarCode]));
            }
            set
            {
                this[this.table.ColumnBarCode] = value;
            }
        }
        public int Unit
        {
            get
            {
                return ((int)(this[this.table.ColumnUnit]));
            }
            set
            {
                this[this.table.ColumnUnit] = value;
            }
        }
        public DateTime ModeDate
        {
            get
            {
                return ((DateTime)(this[this.table.ColumnModDate]));
            }
            set
            {
                this[this.table.ColumnModDate] = value;
            }
        }
    }
}

