using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace DM.Common
{
    public class ExcelSheet
    {
        private string _ExcelFileName;
        private FileStream _fileStream;
        private IWorkbook _Workbook;
        private ISheet _Sheet;

        /// <summary>
        /// 读取Excel文件Sheet
        /// </summary>
        /// <param name="excelFile">excel文件完整路径</param>
        /// <param name="sheetName">excel sheet名称</param>
        /// <param name="fileAccess">文件访问权限</param>
        public ExcelSheet(string excelFile, string sheetName, FileAccess fileAccess)
        {
            _ExcelFileName = excelFile;
            string extension = Path.GetExtension(excelFile).ToLower();

            if (extension.Equals(".xls"))
            {
                if (fileAccess == FileAccess.Read)
                {
                    try
                    {
                        _fileStream = File.OpenRead(excelFile);
                        _Workbook = new HSSFWorkbook(_fileStream);
                        _Sheet = _Workbook.GetSheet(sheetName);
                        _fileStream.Close();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        _fileStream.Close();
                    }
                }
                else
                {
                    _Workbook = new HSSFWorkbook();
                    _Sheet = _Workbook.CreateSheet(sheetName);
                }
            }
            else
            {
                if (fileAccess == FileAccess.Read)
                {
                    _fileStream = File.OpenRead(excelFile);
                    _Workbook = new XSSFWorkbook(_fileStream);
                    _Sheet = _Workbook.GetSheet(sheetName);
                    _fileStream.Close();
                }
                else
                {
                    _Workbook = new XSSFWorkbook();
                    _Sheet = _Workbook.CreateSheet(sheetName);
                }
            }
        }

        public string this[int rowIndex, int columnIndex]
        {
            get
            {
                IRow row = _Sheet.GetRow(rowIndex);
                if (row == null) return null;

                ICell cell = row.GetCell(columnIndex);
                if (cell == null) return null;

                return Convert.ToString(GetCellValue(cell));
            }

            set
            {
                IRow row = _Sheet.GetRow(rowIndex);
                if (row == null) row = _Sheet.CreateRow(rowIndex);

                ICell cell = row.GetCell(columnIndex);
                if (cell == null) cell = row.CreateCell(columnIndex);

                //SetCellValue(cell, value);
                cell.SetCellValue(value);
            }
        }

        public int RowCount
        {
            get
            {
                return _Sheet.PhysicalNumberOfRows;
            }
        }


        public bool Write()
        {
            try
            {
                _fileStream = File.OpenWrite(_ExcelFileName);
                _Workbook.Write(_fileStream);   //向打开的这个Excel文件中写入表单并保存。  
                _fileStream.Close();
                return true;
            }
            catch (IOException)
            {
                throw new Exception(string.Format("{0}文件被占用。", _ExcelFileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _fileStream.Close();
            }
        }

        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        protected object GetCellValue(ICell cell)
        {
            object value = null;

            switch (cell.CellType)
            {
                //空串
                case CellType.Blank:
                    value = string.Empty;
                    break;


                case CellType.Numeric:
                    // 日期值 
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        value = cell.DateCellValue;
                    }
                    else
                    {
                        // 数值
                        value = cell.NumericCellValue;
                    }
                    break;

                case CellType.Boolean:
                    // 布尔值
                    value = cell.BooleanCellValue;
                    break;

                case CellType.Formula:
                    //公式
                    value = cell.CellFormula;
                    break;

                default:
                    // 字符串
                    value = cell.StringCellValue;
                    break;
            }

            return value;

        }
    }


    #region

    ///// <summary>
    ///// 写入单元格数据
    ///// </summary>
    ///// <param name="cell"></param>
    ///// <param name="obj"></param>
    //protected static void SetCellValue(ICell cell, object obj)
    //{
    //    if (obj.GetType() == typeof(int))
    //    {
    //        cell.SetCellValue((int)obj);
    //    }
    //    else if (obj.GetType() == typeof(double))
    //    {
    //        cell.SetCellValue((double)obj);
    //    }
    //    else if (obj.GetType() == typeof(IRichTextString))
    //    {
    //        cell.SetCellValue((IRichTextString)obj);
    //    }
    //    else if (obj.GetType() == typeof(string))
    //    {
    //        cell.SetCellValue(obj.ToString());
    //    }
    //    else if (obj.GetType() == typeof(DateTime))
    //    {
    //        cell.SetCellValue((DateTime)obj);
    //    }
    //    else if (obj.GetType() == typeof(bool))
    //    {
    //        cell.SetCellValue((bool)obj);
    //    }
    //    else
    //    {
    //        cell.SetCellValue(obj.ToString());
    //    }
    //}
    #endregion

}



