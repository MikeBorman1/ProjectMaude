using CPMA_Core_APP.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.Utils
{
    public class SecureStore : ISecureStore
    {
        public void RemovePostcode()
        {
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                SecureStorage.RemoveAll();
            }
        }

        public async Task<string> RetrievePostcode()
        {
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                return await SecureStorage.GetAsync(PersistedItemKeys.Postcode);
            }
            return await Task.Run(() =>
            {
                return "OnVirtual";
            });
        }


		public async Task SavePostcode(string postcode)
        {
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                await SecureStorage.SetAsync(PersistedItemKeys.Postcode, postcode);
            }
        }

        public async Task<bool> CheckPostcode()
        {
            if (await RetrievePostcode() == "OnVirtual")
            {
                return false;
            }

            else
            {
                return true;
            }
        } 
    }
}
