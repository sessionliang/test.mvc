using DM.Common;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace DM.Web.UIExtension
{
    public abstract class BaseControl
    {
        protected const string FormatXmlBasePath = "/UIExtension/";
        protected const string FormatXmlFileName = "format.html";

        /// <summary>
        /// 每一个控件的渲染方法
        /// </summary>
        /// <returns></returns>
        public abstract string Render(RouteValueDictionary attrs);

        /// <summary>
        /// 控件名称
        /// </summary>
        public abstract string ControlName { get; }

        /// <summary>
        /// 控件模板
        /// </summary>
        public virtual string FormatString
        {
            get
            {
                return GetControlFormat();
            }
        }

        /// <summary>
        /// 获取控件FormatString
        /// </summary>
        /// <returns></returns>
        public virtual string GetControlFormat()
        {
            string filePath = HttpContext.Current.Request.MapPath(VirtualPathUtility.Combine(FormatXmlBasePath, Path.Combine(ControlName, FormatXmlFileName)));

            return FileHelper.ReadText(filePath);
        }

        /// <summary>
        /// 对属性赋值
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public virtual string Format(string html, string attribute, string value)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }
            //遍历指定属性值
            var regexAttr = new Regex("\\[\\[" + attribute + ":?([^(\\]\\])]{0,})\\]\\]");

            var mcsAttr = regexAttr.Matches(html);
            if (mcsAttr != null && mcsAttr.Count > 0)
            {
                foreach (Match match in mcsAttr)
                {
                    var defaultValue = match.Groups[1];
                    if (!string.IsNullOrEmpty(value))
                    {
                        html = html.Replace(match.Value, value);
                    }
                    else
                    {
                        html = html.Replace(match.Value, defaultValue.Value);
                    }
                }
            }

            return html;
        }

        /// <summary>
        /// 遍历属性组赋值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public virtual string Format(string html, RouteValueDictionary attributes)
        {
            foreach (var attr in attributes)
            {
                html = Format(html, attr.Key, Convert.ToString(attr.Value));
            }

            //遍历默认属性值\\[\\[[^(\\]\\])]{1,}:?([\\]]{0,})\\]\\]
            var regexDefault = new Regex("\\[\\[[\\w\\d]+[:]{1,}(.+)\\]\\]");
            var mcsDefault = regexDefault.Matches(html);
            if (mcsDefault != null && mcsDefault.Count > 0)
            {
                foreach (Match match in mcsDefault)
                {
                    var defaultValue = match.Groups[1];
                    if (defaultValue != null)
                    {
                        html = html.Replace(match.Value, defaultValue.Value);
                    }
                }
            }
            return html;
        }
    }
}