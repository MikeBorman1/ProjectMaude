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
    public class APISetFlag : ISetFlag
    {
        public async Task SetFlag(string ProductId)
        {
            var requestBody = new JObject
            {
                ["productId"] = ProductId
            };
            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.GetSetFlag)), requestBody))
            {

            }
        }
    }
}
