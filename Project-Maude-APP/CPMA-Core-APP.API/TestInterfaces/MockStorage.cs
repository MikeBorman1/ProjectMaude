using CPMA_Core_APP.API;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Common;
using System;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Tests.TestInterfaces

{
    public class MockStorage : IStorage
    {
        public async Task UploadPhoto(string photoUri, string photoPath)
        {
            return;
        }
        public async Task CopyPhoto(string source, string target)
        {
            return;
        }
    }
}
