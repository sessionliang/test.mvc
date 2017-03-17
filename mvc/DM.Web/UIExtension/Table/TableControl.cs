using DM.Web.UIExtension;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class TableControl : BaseControl
    {
        public override string ControlName
        {
            get
            {
                return "Table";
            }
        }

        public const string Attribute_Id = "id";
        public const string Attribute_Url = "dataUrl";
        public const string Attribute_Columns = "columns";


        /// <summary>
        /// 1. 获取TableFormatString -- 使用html模板
        /// 2. 获取TableAttrString   -- 使用json对象
        /// 3. 获取TableHeaderString -- 使用json对象
        /// 4. 渲染                  -- 正则表达式
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public override string Render(RouteValueDictionary attrs)
        {
            string result = this.FormatString;

            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            if (attrs == null)
            {
                return result;
            }
            //特定属性赋值
            if (attrs.ContainsKey(Attribute_Columns))
            {
                result = base.Format(result, Attribute_Columns, GetColumnsHtml(attrs[Attribute_Columns]));
            }
            //一般属性赋值
            result = base.Format(result, attrs);
            return result;
        }

        public string GetColumnsHtml(dynamic columns)
        {
            StringBuilder columnHtml = new StringBuilder();
            foreach (var column in columns)
            {
                var attrs = column;
                if (!(column is RouteValueDictionary))
                {
                    attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(column);
                }

                columnHtml.Append(RenderColumn(attrs));
            }
            return columnHtml.ToString();
        }

        public string RenderColumn(RouteValueDictionary column)
        {
            var builder = new TagBuilder("th");

            if (Convert.ToBoolean(column["dataCheckbox"]))
            {
                builder.Attributes.Add("data-checkbox", "true");
            }
            if (Convert.ToBoolean(column["dataSortable"]))
            {
                builder.Attributes.Add("data-sortable", "true");
            }

            builder.Attributes.Add("data-field", Convert.ToString(column["dataField"]));
            builder.Attributes.Add("data-formatter", Convert.ToString(column["dataFormatter"]));
            builder.Attributes.Add("data-align", Convert.ToString(column["dataAlign"]));

            builder.SetInnerText(Convert.ToString(column["dataName"]));

            return builder.ToString(TagRenderMode.Normal);
        }
    }

    public static class TableExtension
    {
        public static MvcHtmlString Table(this HtmlHelper helper, string id, string url, ArrayList columns, object htmlAttributes)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            attrs.Add(TableControl.Attribute_Id, id);
            attrs.Add(TableControl.Attribute_Url, url);
            attrs.Add(TableControl.Attribute_Columns, columns);

            return MvcHtmlString.Create(new TableControl().Render(attrs));
        }
    }

    public static class TableFieldExtension
    {
        public static RouteValueDictionary FieldFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData);
            attrs.Add("dataName", modelMetadata.DisplayName);
            attrs.Add("dataField", modelMetadata.PropertyName[0].ToString().ToLower() + modelMetadata.PropertyName.Substring(1));
            return attrs;
        }
    }
}