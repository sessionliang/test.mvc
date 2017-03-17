using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DM.Common
{
    public static class ListBoxHelper
    {
        public static void BindData(System.Web.UI.WebControls.ListBox listbox, DataSet dset, string Value, string Text)
        {
            try
            {
                if (dset != null)
                {
                    listbox.DataSource = dset;
                    listbox.DataTextField = Text;
                    listbox.DataValueField = Value;
                    listbox.DataBind();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void SelectItemByValue(System.Web.UI.WebControls.ListBox listbox, string SelectedValue)
        {
            try
            {
                listbox.SelectedIndex = listbox.Items.IndexOf(listbox.Items.FindByValue(SelectedValue.Trim()));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string GetSelectedItemsToString(DevExpress.Web.ListEditItemCollection pItems)
        {
            if (pItems.Count == 0) return null;
            StringBuilder strCodes = new StringBuilder();
            for (int i = 0; i < pItems.Count; i++)
            {
                DevExpress.Web.ListEditItem item = pItems[i];
                if (item.Selected)
                    strCodes.Append(item.Value.ToString() + ",");
            }
            return strCodes.ToString().TrimEnd(',');
        }
    }
}
