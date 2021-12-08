using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Common;
using CPMA_Core_APP.Utils.Interfaces;
using System;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockSecureStore : ISecureStore
    {
        public void RemovePostcode()
        {
            return;
        }

        public async Task<string> RetrievePostcode()
        {
            return await Task.Run(() =>
            {
                return TestingStaticVariables.TestingPostcode;
            });
        }

        public async Task SavePostcode(string session)
        {
            return;
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
