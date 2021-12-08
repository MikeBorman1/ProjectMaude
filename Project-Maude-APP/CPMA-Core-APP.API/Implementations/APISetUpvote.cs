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


namespace CPMA_Core_APP.API
{
    public class APISetUpvote : ISetUpvote
    {
        public async Task Upvote(int ReportId)
        {
            var requestBody = new JObject
            {
                ["ReportId"] = ReportId,
                ["VoteFlag"] = 1
            };
            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.SetUpvote)), requestBody))
            {

            }
        }

        public async Task Downvote(int ReportId)
        {
            var requestBody = new JObject
            {
                ["ReportId"] = ReportId,
                ["VoteFlag"] = 0
            };
            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri((await EndpointBase()).Replace(APIAddresses.REPLACEME, APIAddresses.SetUpvote)), requestBody))
            {

            }
        }


    }
}
