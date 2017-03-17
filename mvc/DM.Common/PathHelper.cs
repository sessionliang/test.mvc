using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common
{
    public static class PathHelper
    {
        public static string GetPhysicalApplicationPath()
        {
            return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
        }
    }
}
