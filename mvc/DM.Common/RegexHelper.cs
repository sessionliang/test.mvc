using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DM.Common
{
    public class RegexHelper
    {
        public static string HumpToString(string input, string split = "-")
        {
            var regex = new Regex("((?<=[a-z])(?=[A-Z]))");
            return regex.Replace(input, split).ToLower();
        }
    }
}
