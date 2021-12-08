using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Common;
using System;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Tests.TestInterfaces

{
    public class MockLogin : ILogin
    {
        public async Task<string> LoginAsync(string identifier, string password)
        {
            if(identifier == TestingStaticVariables.TestingUsername && password == TestingStaticVariables.TestingPassword)
            {
                return await Task.Run(() =>
                {
                    return TestingStaticVariables.TestingSessionGuid;
                });
            }
            throw new Exception(TestingStaticVariables.TestingLogonError);
        }
    }
}
