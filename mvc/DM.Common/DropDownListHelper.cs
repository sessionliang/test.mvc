using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DM.Common
{
    public static class DropDownListHelper
    {
        public static void BindData(System.Web.UI.WebControls.DropDownList list, DataSet dset, bool AddChoose)
        {
            try
            {
                if (dset != null)
                {
                    if (AddChoose)
                    {
                        DataRow newRow = dset.Tables[0].NewRow();
                        newRow["ID"] = "-1";
                        newRow["NAME"] = "==请选择==";
                        dset.Tables[0].Rows.InsertAt(newRow, 0);
                        dset.Tables[0].AcceptChanges();
                    }
                    list.DataSource = dset;
                    list.DataTextField = "NAME";
                    list.DataValueField = "ID";
                    list.DataBind();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void BindData(System.Web.UI.WebControls.DropDownList list, DataSet dset, string SelectedValue)
        {
            try
            {
                if (dset != null)
                {
                    foreach (DataRow row in dset.Tables[0].Rows)
                    {
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(row["NAME"].ToString(), row["ID"].ToString());
                        if (row["ID"].ToString() == SelectedValue)
                        {
                            item.Selected = true;
                        }
                        list.Items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void SelectItemByValue(System.Web.UI.WebControls.DropDownList list, string SelectedValue)
        {
            try
            {
                list.SelectedIndex = list.Items.IndexOf(list.Items.FindByValue(SelectedValue.Trim()));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void SelectItemByText(System.Web.UI.WebControls.DropDownList list, string SelectedText)
        {
            try
            {
                list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText(SelectedText.Trim()));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
