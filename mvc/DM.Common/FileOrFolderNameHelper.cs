using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common
{
    public static class FileOrFolderNameHelper
    {
        public static string ConvertFileName(string pInFileName)
        {
            pInFileName = pInFileName.Replace("\\", "_");
            pInFileName = pInFileName.Replace("/", "_");
            pInFileName = pInFileName.Replace(":", "_");
            pInFileName = pInFileName.Replace("*", "_");
            pInFileName = pInFileName.Replace("?", "_");
            pInFileName = pInFileName.Replace("\"", "_");
            pInFileName = pInFileName.Replace("<", "_");
            pInFileName = pInFileName.Replace(">", "_");
            pInFileName = pInFileName.Replace("|", "_");
            return pInFileName;
        }
    }
}
