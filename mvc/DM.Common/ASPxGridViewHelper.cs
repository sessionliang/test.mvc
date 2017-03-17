using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Web;
using System.Web.UI;
using System.Data;

namespace DM.Common
{
    public static class ASPxGridViewHelper
    {
        public static void SetCookies(ASPxGridView pGridView, string pCookiesID, bool pEnabled)
        {
            pGridView.SettingsCookies.Enabled = pEnabled;
            pGridView.SettingsCookies.CookiesID = pCookiesID;
            pGridView.SettingsCookies.StoreColumnsVisiblePosition = pEnabled;
            pGridView.SettingsCookies.StoreColumnsWidth = pEnabled;
            pGridView.SettingsCookies.StoreGroupingAndSorting = pEnabled;
        }
        /// <summary>
        /// GridView行编辑，控件查看状态
        /// </summary>
        /// <param name="grd"></param>        
        /// <param name="e"></param>
        /// <param name="pFieldName">Combox控件需要传入字段名称</param>
        /// <param name="pFormat"></param>
        public static void SetEditorViewState(DevExpress.Web.ASPxGridView grd, DevExpress.Web.ASPxGridViewEditorEventArgs e, string pFieldName, string pFormat)
        {
            e.Editor.Visible = false;
            ASPxLabel lable = new ASPxLabel();
            object value = string.IsNullOrEmpty(pFieldName) ? e.Value : grd.GetRowValuesByKeyValue(e.KeyValue, pFieldName);
            lable.Text = string.Format("{0:" + pFormat + "}", value);
            e.Editor.Parent.Controls.Add(lable);
        }
        public static void SetEditorViewState(DevExpress.Web.ASPxGridView grd, DevExpress.Web.ASPxGridViewEditorEventArgs e, string pFieldName)
        {
            SetEditorViewState(grd, e, pFieldName, null);
        }

        public static DataTable TranslateSelectedRowsToDataTable(DevExpress.Web.ASPxGridView grd, string KeyValueName, params string[] columns)
        {
            #region 初始化DataTable
            DataTable dt = new DataTable();
            //添加列名
            foreach (GridViewColumn col in grd.Columns)
            {
                if (col is GridViewDataColumn)
                {
                    var dataCol = col as GridViewDataColumn;
                    //没有列明参数，默认全部获取
                    if (columns.Length == 0)
                    {
                        dt.Columns.Add(dataCol.FieldName);
                    }
                    //只获取传入的参数
                    else if (Array.IndexOf(columns, dataCol.FieldName) >= 0)
                    {
                        dt.Columns.Add(dataCol.FieldName);
                    }
                }

            }
            #endregion

            #region 加载DataTable
            List<object> ids = grd.GetSelectedFieldValues(KeyValueName);
            foreach (var id in ids)
            {
                var visibleIndex = grd.FindVisibleIndexByKeyValue(id);

                DataRow dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    var grdCol = grd.Columns[col.ColumnName];
                    var ctrl = GetRowCellTemplateControl(grd, id, col.ColumnName, col.ColumnName);
                    //如果是控件，那么加载控件的值
                    if (ctrl is ASPxEdit)
                    {
                        ASPxEdit devCtrl = ctrl as ASPxEdit;
                        string value = devCtrl.Value == null ? string.Empty : devCtrl.Value.ToString();
                        dr[col.ColumnName] = value;
                    }
                    //加载列的值
                    else
                    {
                        dr[col.ColumnName] = grd.GetRowValues(visibleIndex, col.ColumnName);
                    }
                }
                dt.Rows.Add(dr);
            }
            #endregion

            return dt;
        }

        public static Control GetRowCellTemplateControl(DevExpress.Web.ASPxGridView grd, object rowId, string controlId, string columnName)
        {
            return grd.FindRowCellTemplateControlByKey(rowId, grd.Columns[columnName] as GridViewDataColumn, controlId);
        }

        public static Control GetRowCellTemplateControl(DevExpress.Web.ASPxGridView grd, int visibleIndex, string controlId, string columnName)
        {
            return grd.FindRowCellTemplateControl(visibleIndex, grd.Columns[columnName] as GridViewDataColumn, controlId);
        }
    }
}
