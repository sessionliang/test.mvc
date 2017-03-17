using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Common
{
    public class FileHelper
    {
        public static bool IsExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string ReadText(string filePath)
        {
            if (FileHelper.IsExists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }
    }
}
