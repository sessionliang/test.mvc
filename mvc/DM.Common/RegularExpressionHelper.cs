using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DM.Common
{
    public static class RegularExpressionHelper
    {
        public static readonly string REGULAR_NAMING = "^[a-zA-Z][a-zA-Z0-9_]*$"; //命名规则：26个英文字母、数字、下划线组成的字符串
        public static readonly string REGULAR_ABBR = "^[A-Za-z]+$";
        public static bool IsMatch(string expression, string value)
        {
            Regex reg = new Regex(expression);
            return reg.IsMatch(value);
        }
    }
}
