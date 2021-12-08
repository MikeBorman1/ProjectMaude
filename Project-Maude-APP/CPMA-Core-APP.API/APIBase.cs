using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.API
{
    public static class APIBaseHelpers
    {
        public static AsyncLazy<string> EndpointBase = new AsyncLazy<string>(async delegate
        {
            APIBase client = new APIBase();
            return (await client.GetAPIUri());
        });
    }

    public class APIBase
    {
        public readonly Uri GetAPIAzFun = new Uri(APIAddresses.GetAPIEndpoint);

        public async Task<string> GetAPIUri()
        {
            var versionString = VersionTracking.CurrentVersion;
            var device = Device.RuntimePlatform;
            //Passing both version and device for future variations
            var requestBody = new JObject
            {
                ["versionCode"] = versionString,
                ["device"] = device
            };

            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await ApiEndpoints.PostJsonUriAsync(client, GetAPIAzFun, requestBody))
                    {
                        

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var ret = await response.Content.ReadAsStringAsync();
                            return ret;
                        }

                        throw new Exception("Something went wrong");
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                return "https://inthebagapi-dev.azurewebsites.net/api/REPLACEME?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
            }

        }
    }
}
