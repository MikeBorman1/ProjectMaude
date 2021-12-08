using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API
{
    public interface ISetUpvote
    {
        Task Upvote(int ReportId);
        Task Downvote(int ReportId);
    }
}
