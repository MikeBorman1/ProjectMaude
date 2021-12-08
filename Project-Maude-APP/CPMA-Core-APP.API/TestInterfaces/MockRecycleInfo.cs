using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockRecycleInfo : IGetRecycleInfo
    {

       
        public async Task<RecycleInfo[]> getInfo()
        {

                var list = new List<RecycleInfo>
                {
                    new RecycleInfo()
                    {
                        Name = "Glass",
                        Rejected =  "- Any colour glass bottles and jars",
                        Accepted = "- Broken drinks glasses ; -Window glass; -Pyrex Cookware; -Glassware; -Crockery"
                    },
                   
                };

           

            return await Task.Run(() =>
                {
                    return list.ToArray();
                });
        }
        
    }
}
