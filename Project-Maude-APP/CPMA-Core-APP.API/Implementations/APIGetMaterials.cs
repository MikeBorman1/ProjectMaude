using static CPMA_Core_APP.API.ApiEndpoints;
using CPMA_Core_APP.API.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CPMA_Core_APP.Model;
using Newtonsoft.Json;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.API.Implementations
{

    public class APIGetMaterials : IGetMaterials
    {
        public async Task<MaterialSearch[]> getMaterials()
        {
            var requestBody = new JObject
            {
               
            };

            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.GetMaterials)), requestBody))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var thing = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MaterialSearch[]>(thing);
                }
               return await Task.Run(() =>
                {
                    return new MaterialSearch[0];
                });
            }
        }
    }
}
