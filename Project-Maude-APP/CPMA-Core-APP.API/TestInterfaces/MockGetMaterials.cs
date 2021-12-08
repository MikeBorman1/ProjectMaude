using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockGetMaterials : IGetMaterials
    {
        public async Task<MaterialSearch[]> getMaterials()
        {

                var list = new List<MaterialSearch>
                {
                    new MaterialSearch()
                    {
                        Name = "Plastic"
                    },
                    new MaterialSearch()
                    {
                        Name = "Metal"
                    },
                    new MaterialSearch()
                    {
                        Name = "Glass"
                    }
                };


                return await Task.Run(() =>
                {
                    return list.ToArray();
                });
        }

    }
}
