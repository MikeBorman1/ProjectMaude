using static CPMA_Core_APP.API.ApiEndpoints;
using CPMA_Core_APP.API.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.Implementations
{
    /// <summary>
    /// Implementing the login interface. this is the method referenced in app.xaml.cs as a registered container. 
    /// see the login view model to see how this is dependency injected to the view models.
    /// see the MockLogin for how this is unit tested.
    /// </summary>
    public class APILogin : ILogin
    {
        public async Task<string> LoginAsync(string identifier, string password)
        {
            var requestBody = new JObject
            {
                ["identifier"] = identifier,
                ["password"] = password
            };

            using (var client = new HttpClient())
            using (var response = await client.PostJsonUriAsync(new Uri(String.Format(await EndpointBase(), "login")), requestBody))
            {
                var content = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.StatusCode == HttpStatusCode.OK)
                    return content["session"].ToObject<string>();

                throw new Exception(content["message"].ToObject<string>());
            }
        }
    }
}
