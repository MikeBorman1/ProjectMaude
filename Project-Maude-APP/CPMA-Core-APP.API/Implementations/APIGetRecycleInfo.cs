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
   
    public class APIGetRecycleInfo : IGetRecycleInfo
    {
        public async Task<RecycleInfo[]> getInfo()
        {
            var requestBody = new JObject
            {
               
            
            };
             
            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.GetRecycleInfo)), requestBody))
            {
                var thing = await response.Content.ReadAsStringAsync();

                
                
                    
                return JsonConvert.DeserializeObject<RecycleInfo[]>(thing);
                    


                

                
            }
        }
    }
}
