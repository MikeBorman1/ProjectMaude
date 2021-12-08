using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockGetInfoBarcode : IGetInfoBarcode
    {
        public async Task<SearchResult[]> getProductInfo(string barcode)
        {
            if (barcode != "0000") {
                var mats = new List<Material>
                {
                    new Material()
                    {
                        Name = "Plastic",
                        ImageUrl = "1",
                        Recyclable = true,
                        RecycleBin = "Blue",
                        IsBin = false
                    }
                };

                var list = new List<SearchResult>
                {
                    new SearchResult()
                    {
                        ProductName = "Plastic Bottle",
                        ProductPhotoID = Guid.NewGuid(),
                        Materials = new ObservableCollection<Material>(mats),
                    }
                };


                return await Task.Run(() =>
                {
                    return list.ToArray();
                });
            }

            return new SearchResult[0];
        }

    }
}
