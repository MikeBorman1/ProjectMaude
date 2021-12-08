using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.API.ApiEndpoints;

namespace CPMA_Core_APP.API
{
    public class APISetReport : ISetReport
    {
        public async Task SetReport(string ReportImageID, string ReportTitle, string Barcode)
        {
            var requestBody = new JObject
            {
                ["ReportImageID"] = ReportImageID,
                ["ReportTitle"] = ReportTitle,
                ["Barcode"] = Barcode,
            };
            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.SetReports)), requestBody))
            {

            }
        }
    }
}
