using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Common
{

    public static partial class StaticVariables
    {
        /// <summary>
        /// Addresses used for the API
        /// </summary>
        public static class APIAddresses
        {
            public const string GetAPIEndpoint = "https://itb-api-manager.azurewebsites.net/api/GetAPIEndpoint";
            public const string GetInfoBarcode = "getInfoBarcode";
            public const string GetSearchResults = "getSearchResults";
            public const string GetLocations = "getLocations";
            public const string GetRecycleCodeLocation = "getRecycleCodeLocation";
            public const string GetReports = "getReports";
            public const string SetUpvote = "setUpvote";
            public const string SetReports = "setReports";
            public const string GetSetFlag = "getSetFlag";
            public const string GetLastReportID = "getLastReportID";
            public const string SetProductLinkMaterial = "setProductLinkMaterial";
            public const string GetMaterials = "getMaterials";
            public const string GetRecycleInfo = "getRecycleInfo";
            public const string REPLACEME = "REPLACEME";
        }
    }

}