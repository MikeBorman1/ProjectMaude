using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace CPMA_Core_APP.Utils
{
    public static class KeyPointsUtils
    {
        public static string BuildKeyPoints(Material m)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var recycleStatement = m.Recyclable ? m.RecycleBin + " bin" : "general waste";
            return textInfo.ToTitleCase(m.Name) + ": " + recycleStatement;
        }
    }
}
