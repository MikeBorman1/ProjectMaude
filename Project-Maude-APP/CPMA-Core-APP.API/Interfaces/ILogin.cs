using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.Interfaces
{
    /// <summary>
    /// Example interface the interface will contain the api endpoint. we can then mock this for the unit tests.
    /// </summary>
    public interface ILogin
    {
        Task<string> LoginAsync(string identifier, string password);
    }
}
