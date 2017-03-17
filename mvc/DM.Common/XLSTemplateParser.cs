//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using System.Collections;
//using FESCO.MIS.Framework.VBExcel;

//namespace DM.Common
//{
//    public class XLSTemplateParser
//    {        
//        private string templateFileName = "";
//        private ExcelManager excel = null;
//        ArrayList XLSReports = new ArrayList();
//        public ExcelManager Excel
//        {
//            get { return this.excel; }
//        }

//        public XLSTemplateParser(string templateFileName)
//        {
//            this.templateFileName = templateFileName;
//        }

//        public void OpenExcel()
//        {
//            this.excel = new ExcelManager();
//            this.excel.LoadExcel(this.templateFileName);
//        }

//        public XLSTemplate NewXLSTemplate(string templateSheetName, int templateHeadStartRow, int templateHeadEndRow, string templateHeadEndCol, int templateFootStartRow,  int templateFootEndRow,string templateFootEndCol)
//        {
//            return new XLSTemplate(templateSheetName, templateHeadStartRow, templateHeadEndRow, templateHeadEndCol, templateFootStartRow, templateFootEndRow, templateFootEndCol);
//        }

//        public XLSTemplate NewXLSTemplate(string templateSheetName, int templateHeadEndRow, string templateHeadEndCol, int templateFootStartRow,  int templateFootEndRow,string templateFootEndCol)
//        {
//            return new XLSTemplate(templateSheetName, 1, templateHeadEndRow, templateHeadEndCol, templateFootStartRow, templateFootEndRow, templateFootEndCol);
//        }

//        public XLSTemplate NewXLSTemplate(string templateSheetName, int templateHeadEndRow, string templateHeadEndCol)
//        {
//            return new XLSTemplate(templateSheetName, 1, templateHeadEndRow, templateHeadEndCol, templateHeadEndRow + 1, templateHeadEndRow + 1, templateHeadEndCol);
//        }

//        public XLSReport BindData(string reportSheetName, XLSTemplate xlsTemplate, DataTable datasource)
//        {
//            return this.BindData(reportSheetName, xlsTemplate, datasource, 1, false);
//        }

//        public XLSReport BindData(string reportSheetName, XLSTemplate xlsTemplate, DataTable datasource, int startPointRowIndex)
//        {
//            return this.BindData(reportSheetName, xlsTemplate, datasource, startPointRowIndex, false);
//        }

//        public XLSReport BindData(string reportSheetName, XLSTemplate xlsTemplate, DataTable datasource, int startPointRowIndex, bool containsDataSourceHeader)
//        {
//            if (reportSheetName == xlsTemplate.TemplateSheetName)
//            {
//                return null;
//            }
//            XLSReport xlsReport = new XLSReport(reportSheetName, xlsTemplate, datasource, startPointRowIndex, containsDataSourceHeader);
//            excel.LoadSheet(xlsReport.ReportSheetName, "A", xlsReport.Template.TemplateHeadEndCol);
//            excel.RangeCopy(xlsReport.Template.TemplateSheetName, "A" + xlsReport.Template.TemplateHeadStartRow, xlsReport.Template.TemplateHeadEndCol + xlsReport.Template.TemplateHeadEndRow.ToString(), xlsReport.ReportSheetName, "A" + xlsReport.ReportHeadStartRow);
//            if (xlsReport.DataSource != null)
//            {
//                int dataSourceStartRow = xlsReport.DataSourceStartRow;
//                if (containsDataSourceHeader == true)
//                {
//                    DataTable dataSourceHeader = this.GetDataSourceHeader(xlsReport.DataSource);
//                    excel.UpdateRegionValueDataSetAt("A", xlsReport.DataSourceStartRow, dataSourceHeader);
//                    dataSourceStartRow = xlsReport.DataSourceStartRow + 1;
//                    excel.SetRangeBorderStyle(string.Format("A{0}", xlsReport.DataSourceStartRow), string.Format("{0}{1}", xlsReport.Template.TemplateHeadEndCol, xlsReport.DataSourceStartRow + 1), xlsReport.DataSourceBoderStyle);
//                }
//                if (xlsReport.DataSource.Rows.Count > 0)
//                {
//                    excel.UpdateRegionValueDataSetAt("A", dataSourceStartRow, xlsReport.DataSource);        
//                    excel.SetRangeBorderStyle("A" + dataSourceStartRow.ToString(), xlsReport.Template.TemplateHeadEndCol + xlsReport.DataSourceEndRow.ToString(), xlsReport.DataSourceBoderStyle);
//                }
//            }
//            excel.RangeCopy(xlsReport.Template.TemplateSheetName, "A" + xlsReport.Template.TemplateFootStartRow.ToString(), xlsReport.Template.TemplateFootEndCol + xlsReport.Template.TemplateFootEndRow.ToString(), xlsReport.ReportSheetName, "A" + xlsReport.ReportFootStartRow.ToString());
//            this.XLSReports.Add(xlsReport);
//            return xlsReport;
//        }

//        public void ReplaceRegion(XLSTemplate xlsTemplate, DataTable datasource, int startRow, string startCol)
//        {
//            this.ReplaceRegion(xlsTemplate, datasource, startRow, startCol, false);
//        }

//        public void ReplaceRegion(XLSTemplate xlsTemplate, DataTable datasource, int startRow, string startCol, bool containsDataSourceHeader)
//        {
//            excel.LoadSheet(xlsTemplate.TemplateSheetName, "A", xlsTemplate.TemplateHeadEndCol); ;
//            if (datasource != null)
//            {
//                int dataSourceStartRow = startRow;
//                if (containsDataSourceHeader == true)
//                {
//                    DataTable dataSourceHeader = this.GetDataSourceHeader(datasource);
//                    excel.UpdateRegionValueDataSetAt("A", startRow, dataSourceHeader);
//                    dataSourceStartRow = startRow + 1;
//                    excel.SetRangeBorderStyle(string.Format("A{0}", startRow), string.Format("{0}{1}", xlsTemplate.TemplateHeadEndCol, startRow + 1), XLSTemplate.DefaultBorderStyle);
//                }
//                if (datasource.Rows.Count > 0)
//                {
//                    excel.UpdateRegionValueDataSetAt(startCol, dataSourceStartRow, datasource);     
//                    excel.SetRangeBorderStyle("A" + dataSourceStartRow.ToString(), xlsTemplate.TemplateHeadEndCol + string.Format("{0}", dataSourceStartRow + datasource.Rows.Count - 1), XLSTemplate.DefaultBorderStyle);
//                }
//            }
//        }

//        public void ReplaceTags(XLSTemplate xlsTemplate, Hashtable tags)
//        {
//            excel.LoadSheet(xlsTemplate.TemplateSheetName, "A", xlsTemplate.TemplateHeadEndCol);
//            if (tags != null && tags.Count > 0)
//            {
//                IDictionaryEnumerator ie = tags.GetEnumerator();
//                while (ie.MoveNext())
//                {
//                    string sKey = ie.Key.ToString();
//                    string sValue = ie.Value.ToString();
//                    excel.RangeReplace("A" + xlsTemplate.TemplateHeadStartRow.ToString(), xlsTemplate.TemplateHeadEndCol + xlsTemplate.TemplateHeadEndRow.ToString(), XLSTemplate.TagBegin + sKey + XLSTemplate.TagEnd, sValue);
//                    excel.RangeReplace("A" + xlsTemplate.TemplateFootStartRow.ToString(), xlsTemplate.TemplateHeadEndCol + xlsTemplate.TemplateFootEndRow.ToString(), XLSTemplate.TagBegin + sKey + XLSTemplate.TagEnd, sValue);
//                }
//            }
//        }

//        public void ReplaceTags(XLSReport xlsReport, Hashtable tags)
//        {
//            if (tags != null && tags.Count > 0)
//            {
//                IDictionaryEnumerator ie = tags.GetEnumerator();
//                while (ie.MoveNext())
//                {
//                    string sKey = ie.Key.ToString();
//                    string sValue = ie.Value.ToString();
//                    excel.RangeReplace("A" + xlsReport.ReportHeadStartRow.ToString(), xlsReport.Template.TemplateHeadEndCol + xlsReport.ReportHeadEndRow.ToString(), XLSTemplate.TagBegin + sKey + XLSTemplate.TagEnd, sValue);
//                    excel.RangeReplace("A" + xlsReport.ReportFootStartRow.ToString(), xlsReport.Template.TemplateFootEndCol + xlsReport.ReportFootEndRow.ToString(), XLSTemplate.TagBegin + sKey + XLSTemplate.TagEnd, sValue);
//                }
//            }
//        }

//        public void ExportTo(string exportFileName)
//        {
//            ArrayList removedWorkSheets = new ArrayList();
//            for (int i = 0; i < XLSReports.Count; i++)
//            {
//                XLSReport xlsReport = (XLSReport)XLSReports[i];
//                if (!removedWorkSheets.Contains(xlsReport.Template.TemplateSheetName) && xlsReport.ReportSheetName != xlsReport.Template.TemplateSheetName)
//                {
//                    this.excel.RemoveWorkSheet(xlsReport.Template.TemplateSheetName);
//                    removedWorkSheets.Add(xlsReport.Template.TemplateSheetName);
//                }
//            }
//            this.excel.SaveExcel(exportFileName, "");
//        }

//        public void Print(string sheetName)
//        {
//            this.excel.LoadSheet(sheetName, "A", "L");
//            this.excel.WorksheetPrint();
//        }

//        public void CloseExcel()
//        {
//            this.excel.CloseApp_CloseAlert();
//            this.excel.CloseMe();
//        }

//        private DataTable GetDataSourceHeader(DataTable datasource)
//        {
//            DataTable dataSourceHeader = new DataTable();
//            for (int i = 0; i < datasource.Columns.Count; i++)
//            {
//                dataSourceHeader.Columns.Add(datasource.Columns[i].ColumnName, System.Type.GetType("System.String"));
//            }
//            DataRow dataRow = dataSourceHeader.NewRow();
//            for (int i = 0; i < dataSourceHeader.Columns.Count; i++)
//            {
//                dataRow[i] = dataSourceHeader.Columns[i].ColumnName;
//            }
//            dataSourceHeader.Rows.Add(dataRow);
//            return dataSourceHeader;
//        }

//        public static string ConvertColumnIndex2Name(int columnIndex)
//        {
//            string[] sCols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
//            int iTmp = columnIndex;
//            string columnName = "";
//            while (iTmp > 0)
//            {
//                int remainder = iTmp % 26;
//                int quotient = iTmp / 26;
//                if (remainder == 0)
//                {
//                    quotient = quotient - 1;
//                    remainder = 26;
//                }
//                columnName = sCols[remainder - 1] + columnName;
//                iTmp = quotient;
//            }
//            return columnName;
//        }

//        public static int ConvertColumnName2Index(string columnName)
//        {
//            string[] sCols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
//            Hashtable colsMapping = new Hashtable();
//            for (int i = 0; i < sCols.Length; i++)
//            {
//                colsMapping.Add(sCols[i], i.ToString());
//            }
//            int columnIndex = 0;
//            for (int i = 0; i < columnName.Length; i++)
//            {
//                string itemName = columnName.Substring(columnName.Length - i - 1, 1);
//                int itemIndex = int.Parse(colsMapping[itemName].ToString()) + 1;
//                columnIndex += itemIndex * (int)Math.Pow(26, i);
//            }
//            return columnIndex;
//        }
//    }

//    public class XLSTemplate
//    {
//        public static readonly int DefaultBorderStyle = 1;
//        public static readonly string TagBegin = "<!--";
//        public static readonly string TagEnd = "-->";

//        private string templateSheetName = "";
//        private int templateHeadStartRow = -1;
//        private string templateHeadEndCol = "";
//        private int templateHeadEndRow = -1;
//        private int templateFootStartRow = -1;
//        private string templateFootEndCol = "";
//        private int templateFootEndRow = -1;
//        public string TemplateSheetName
//        {
//            get { return this.templateSheetName; }
//        }
//        public int TemplateHeadStartRow
//        {
//            get { return this.templateHeadStartRow; }
//        }
//        public string TemplateHeadEndCol
//        {
//            get { return this.templateHeadEndCol; }
//        }
//        public int TemplateHeadEndRow
//        {
//            get { return this.templateHeadEndRow; }
//        }
//        public int TemplateFootStartRow
//        {
//            get { return this.templateFootStartRow; }
//        }
//        public string TemplateFootEndCol
//        {
//            get { return this.templateFootEndCol; }
//        }
//        public int TemplateFootEndRow
//        {
//            get { return this.templateFootEndRow; }
//        }

//        internal XLSTemplate(string templateSheetName, int templateHeadStartRow, int templateHeadEndRow, 
//                                                string templateHeadEndCol, int templateFootStartRow, int templateFootEndRow, string templateFootEndCol)
//        {
//            this.templateSheetName = templateSheetName;
//            this.templateHeadStartRow = templateHeadStartRow;
//            this.templateHeadEndRow = templateHeadEndRow;
//            this.templateHeadEndCol = templateHeadEndCol;
//            this.templateFootStartRow = templateFootStartRow;
//            this.templateFootEndRow = templateFootEndRow;
//            this.templateFootEndCol = templateFootEndCol;
//        }
//    }

//    public class XLSReport
//    {
//        private string reportSheetName = "";
//        private XLSTemplate template = null;
//        private int reportHeadStartRow = -1;
//        private int reportHeadEndRow = -1;
//        private DataTable dataSource = null;
//        private int dataSourceStartRow = -1;
//        private int dataSourceEndRow = -1;
//        private int dataSourceBorderStyle = XLSTemplate.DefaultBorderStyle;
//        private int reportFootStartRow = -1;
//        private int reportFootEndRow = -1;
//        public string ReportSheetName
//        {
//            get { return this.reportSheetName; }
//        }

//        public XLSTemplate Template
//        {
//            get { return this.template; }
//        }

//        public int ReportHeadStartRow
//        {
//            get { return this.reportHeadStartRow; }
//        }
//        public int ReportHeadEndRow
//        {
//            get { return this.reportHeadEndRow; }
//        }
//        public DataTable DataSource
//        {
//            get { return this.dataSource; }
//        }
//        public int DataSourceStartRow
//        {
//            get { return this.dataSourceStartRow; }
//        }
//        public int DataSourceEndRow
//        {
//            get { return this.dataSourceEndRow; }
//        }
//        public int DataSourceBoderStyle
//        {
//            get { return this.dataSourceBorderStyle; }
//        }
//        public int ReportFootStartRow
//        {
//            get { return this.reportFootStartRow; }
//        }
//        public int ReportFootEndRow
//        {
//            get { return this.reportFootEndRow; }
//        }

//        internal XLSReport(string reportSheetName, XLSTemplate template, DataTable dataSource, int startPointRowIndex, bool containsDataSourceHeader)
//        {
//            this.reportSheetName = reportSheetName;
//            this.template = template;
//            this.dataSource = dataSource;
//            this.reportHeadStartRow = startPointRowIndex;
//            this.reportHeadEndRow = this.reportHeadStartRow + (this.template.TemplateHeadEndRow - this.template.TemplateHeadStartRow);
//            this.dataSourceStartRow = this.reportHeadEndRow + 1;
//            if (dataSource == null || dataSource.Rows.Count == 0)
//            {
//                this.dataSourceEndRow = this.reportHeadEndRow;
//            }
//            else
//            {
//                this.dataSourceEndRow = this.reportHeadEndRow + dataSource.Rows.Count;
//            }
//            if (containsDataSourceHeader == true)
//            {
//                this.dataSourceEndRow = this.dataSourceEndRow + 1;
//            }
//            this.reportFootStartRow = this.dataSourceEndRow + 1;
//            this.reportFootEndRow = this.reportFootStartRow + (this.template.TemplateFootEndRow - this.template.TemplateFootStartRow);
//            this.dataSourceBorderStyle = 1;
//        }
//    }
//}
