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

    public class APIGetLocations : IGetLocations
    {
        public async Task<Locations[]> getLocations()
        {
            var requestBody = new JObject
            {
                

            };

            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.GetLocations)), requestBody))
            {
                var thing = await response.Content.ReadAsStringAsync();



                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Locations[]>(thing);
                }


                return await Task.Run(() =>
                {
                    return new Locations[0];
                });



            }
        }

        public async Task<Locations[]> getRecycleCodeLocation(int recycleCodeID)
        {
            var requestBody = new JObject
            {
                ["recycleCodeID"] = recycleCodeID
            };

            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.GetRecycleCodeLocation)), requestBody))
            {
                var thing = await response.Content.ReadAsStringAsync();



                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Locations[]>(thing);
                }


                return await Task.Run(() =>
                {
                    return new Locations[0];
                });



            }
        }
    }
}
