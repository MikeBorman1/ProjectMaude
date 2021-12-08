using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Utils.Interfaces
{
    public interface IUpvoteStore
    {
        Task<List<int>> GetUpvoteData();
        Task Upvote(int rpId);
        
        Task Downvote(int rpId);

    }
}
