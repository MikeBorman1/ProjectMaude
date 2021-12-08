using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Common
{
    public static partial class StaticVariables
    {
        /// <summary>
        /// used when passing parameters between pages.
        /// </summary>
        public static class ParameterKeys
        {
            public static string keywordSearch = "keywordSearch";
            public static string barcodeSearch = "barcodeSearch";
            public static string searchResult = "searchResult";
            public static string barcode = "barcode";
            public static string report = "report";
            public static string materials = "materials";
            public static string reportForm = "reportForm";
            public static string responseType = "responseType";
            public static string solveVM = "solveVM";
        }
    }
}
