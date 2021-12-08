using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockGetSearchResults : IGetSearchResults
    {
        public async Task<SearchResult[]> Search(string keyword)
        {

            var mats = new List<Material>
                {
                    new Material()
                    {
                        Name = "plastic",
                        ImageUrl = "1",
                        Recyclable = true,
                        RecycleBin = "Blue"
                    },
                    new Material()
                    {
                        Name = "metal",
                        ImageUrl = "2",
                        Recyclable = false,
                        RecycleBin = "BLue"
                    },
                    new Material()
                    {
                        Name = "paper",
                        ImageUrl = "3",
                        Recyclable = true,
                        RecycleBin = "Green"
                    }
                };

            var list = new List<SearchResult>()
                {
                    new SearchResult()
                    {
                        ProductName = "plastic bottle",
                        ProductPhotoID = Guid.NewGuid(),
                        Materials = new ObservableCollection<Material>(mats)
                    }
                };


            return await Task.Run(() =>
            {
                return list.ToArray();
            });
        }
    }
}
