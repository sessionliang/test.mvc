using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DM.Common
{
    public class NPOIDataTable
    {
        private DataTable _DataTable;

        private List<NPOIDataColumn> _Columns;

        public NPOIDataTable()
        {
            _DataTable = new DataTable();
            _Columns = new List<NPOIDataColumn>();
        }

        /// <summary>
        /// 新增NPOIDataTable数据列,维护Excel导入和导出时操作的列集合。
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(NPOIDataColumn column)
        {
            _Columns.Add(column);
        }

        /// <summary>
        /// 读出Excel到NPOIDataTable
        /// </summary>
        /// <param name="excelSheet">ExcelSheet对象维护ExcelFile数据流</param>
        /// <param name="rowOffset">读入起始行</param>
        /// <returns></returns>
        public bool ReadExcel(ExcelSheet excelSheet, int rowOffset = 1)
        {
            //构建表结构
            _DataTable.Columns.Clear();
            foreach (NPOIDataColumn col in _Columns)
            {
                _DataTable.Columns.Add(col.ColumnName, typeof(string));
            }

            //填充表数据
            for (int row = rowOffset; row < excelSheet.RowCount; row++)
            {
                DataRow dataRow = _DataTable.NewRow();
                foreach (NPOIDataColumn col in _Columns)
                {
                    dataRow[col.ColumnName] = excelSheet[row, col.ColumnIndex];
                }
                _DataTable.Rows.Add(dataRow);
            }

            return true;
        }

        /// <summary>
        /// 将NPOIDataTable数据写入Excel
        /// </summary>
        /// <param name="excelSheet">ExcelSheet对象维护ExcelFile数据流</param>
        /// <param name="rowOffset">写入起始行</param>
        /// <returns></returns>
        public bool WriteExcel(ExcelSheet excelSheet, int rowOffset = 1)
        {
            if (_DataTable.Rows.Count == 0 || _DataTable.Columns.Count == 0)
            {
                throw new Exception("NPOIDataTable实例无数据行或数据列未定义。");
            }

            //循环写入 - DataTable to Excel
            for (int row = 0; row < _DataTable.Rows.Count; row++)
            {
                for (int col = 0; col < _DataTable.Columns.Count; col++)
                {
                    excelSheet[row + rowOffset, col] = Convert.ToString(_DataTable.Rows[row][col]);
                }
            }


            return excelSheet.Write();
        }

        public DataTable Data
        {
            get
            {
                return _DataTable;
            }
        }

        public List<NPOIDataColumn> Columns
        {
            get
            {
                return _Columns;
            }
        }
    }

    public class NPOIDataColumn
    {
        /// <summary>
        /// 构造1
        /// </summary>
        public void NPOIDataTable()
        {

        }

        /// <summary>
        /// 构造2
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnIndex"></param>
        /// 
        public void NPOIDataTable(string columnName, int columnIndex)
        {
            ColumnName = ColumnName;
            ColumnIndex = columnIndex;
        }

        public string ColumnName { get; set; }

        public int ColumnIndex { get; set; }

    }

}
