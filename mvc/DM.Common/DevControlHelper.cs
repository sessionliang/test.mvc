using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Common
{
    public class DevControlHelper
    {
        /// <summary>
        /// 绑定ASPxComboBox
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dt"></param>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <param name="containsEmpty"></param>
        /// <param name="defaultValue"></param>
        public static void BindComboBox(ASPxComboBox cmb, DataTable dt, string valueField, string textField, bool containsEmpty, object defaultValue)
        {
            if (dt == null) return;
            cmb.DataSource = dt;
            cmb.TextField = textField;
            cmb.ValueField = valueField;
            cmb.DataBind();
            if (containsEmpty)
            {
                ListEditItem item = new ListEditItem()
                {
                    Value = string.Empty,
                    Text = "-- 请选择 --"
                };
                cmb.Items.Insert(0, item);
            }
            cmb.Value = (defaultValue == null) ? string.Empty : defaultValue;
        }

        /// <summary>
        /// 过滤ASPxComboBox
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="showValues"></param>
        /// <param name="displayValues"></param>
        public static void FilterComboBox(ASPxComboBox cmb, List<object> showValues, List<object> displayValues)
        {
            if (cmb == null || cmb.DataSource == null)
            {
                return;
            }
            
            if (showValues != null)
            {
                //显示指定的Values
                var tmpItemList = new List<ListEditItem>();
                var titleItme = cmb.Items.FindByText("-- 请选择 --");
                foreach (object value in showValues)
                {
                    tmpItemList.Add(cmb.Items.FindByValue(value));
                }
                if (titleItme != null)
                {
                    tmpItemList.Insert(0, titleItme);
                }
                cmb.Items.Clear();
                cmb.Items.AddRange(tmpItemList);
            }
            else if (displayValues != null)
            {
                //隐藏制定的Values
                foreach (object value in displayValues)
                {
                    cmb.Items.Remove(cmb.Items.FindByValue(value));
                }
            }
        }

        /// <summary>
        /// 绑定ASPxCheckBoxList
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="dt"></param>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <param name="defaultValue"></param>
        public static void BindCheckBoxList(ASPxCheckBoxList cbl, DataTable dt, string valueField, string textField, object defaultValue)
        {
            cbl.DataSource = dt;
            cbl.TextField = textField;
            cbl.ValueField = valueField;
            cbl.DataBind();
            cbl.Value = (defaultValue == null) ? string.Empty : defaultValue;
        }

        /// <summary>
        /// 绑定ASPxRadioButtonList
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="dt"></param>
        /// <param name="valueField"></param>
        /// <param name="textField"></param>
        /// <param name="defaultValue"></param>
        public static void BindRadioButtonList(ASPxRadioButtonList rbl, DataTable dt, string valueField, string textField, object defaultValue)
        {
            rbl.DataSource = dt;
            rbl.TextField = textField;
            rbl.ValueField = valueField;
            rbl.DataBind();
            rbl.Value = (defaultValue == null) ? string.Empty : defaultValue;
        }

        /// <summary>
        /// 绑定ASPxGridView
        /// </summary>
        /// <param name="gdv"></param>
        /// <param name="dt"></param>
        public static void BindGridView(ASPxGridView gdv, DataTable dt)
        {
            if (gdv == null || dt == null) return;
            gdv.DataSource = dt;
            gdv.DataBind();
        }
        /// <summary>
        /// 绑定ASPxTreeList
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dt"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="parentFieldName"></param>
        public static void BindTreeList(ASPxTreeList tree, DataTable dt, string keyFieldName, string parentFieldName)
        {
            if (tree == null || dt == null) return;
            if (dt.Columns.IndexOf(keyFieldName) < 0 || dt.Columns.IndexOf(parentFieldName) < 0) return;
            tree.KeyFieldName = keyFieldName;
            tree.ParentFieldName = parentFieldName;
            tree.DataSource = dt;
            tree.DataBind();
        }
    }
}

