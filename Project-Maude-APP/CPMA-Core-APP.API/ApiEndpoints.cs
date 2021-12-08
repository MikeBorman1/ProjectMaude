using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CPMA_Core_APP.API
{
    public static class ApiEndpoints
    {

        //TODO: Update the base endpoint to the azure functions app.
        public static async Task<string> EndpointBase()
        {
            return await APIBaseHelpers.EndpointBase;
        }

        /// <summary>
        /// Make a Post Request to the server with the relevant data.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostJsonUriAsync(this HttpClient client, Uri requestUri, JObject json)
        {
            return await client.PostAsync(requestUri, new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
        }

        public static AuthenticationHeaderValue GenerateAuthorizationHeader(string session)
        {
            return new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"session:{session}")));
        }

        public static bool IsConnected()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        private static async Task<int> ExtractId(HttpResponseMessage response)
        {
            var responseText = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //TODO need some sort of contract with the API that we will always return the ID like so:
                return JObject.Parse(responseText)["id"].Value<int>();
            }
            else
            {
                throw new Exception("Upload failed");
            }
        }
    }
}
