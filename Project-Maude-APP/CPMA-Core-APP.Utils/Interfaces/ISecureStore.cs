using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Utils.Interfaces
{
    public interface ISecureStore
    {
        Task SavePostcode(string postcode);
        void RemovePostcode();
        Task<string> RetrievePostcode();

        Task<bool> CheckPostcode();
        //Task<bool> RetrieveUpvote(int rpId);
    }
}
