using CPMA_Core_APP.API;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Common;
using CPMA_Core_APP.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Tests.TestInterfaces

{
    public class MockUpvoteStorage : IUpvoteStore
    {

        public async Task Upvote(int rpId)
        {
            return;
        }
        public async Task Downvote(int rpId)
        {
            return;
        }

        public Task<List<int>> GetUpvoteData()
        {
            return Task.Run(() => new List<int>(){ 1, 2, 3, 4, 5 });
        }
    }
}
