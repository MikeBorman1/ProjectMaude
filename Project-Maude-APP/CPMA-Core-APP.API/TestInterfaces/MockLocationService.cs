using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockLocationService : IGetLocations
    {

       
        public async Task<Locations[]> getLocations()
        {

                var list = new List<Locations>
                {
                    new Locations()
                    {
                        RecycleBin = "Blue",
                        Latitude = 49.4482,
                        Longitude = 2.5895
                    },
                   
                };

           

            return await Task.Run(() =>
                {
                    return list.ToArray();
                });
        }
        public async Task<Locations[]> getRecycleCodeLocation(int RecycelCodeID)
            {
        
                var list = new List<Locations>
                {
                    new Locations()
                    {
                        RecycleBin = "Blue",
                        Latitude = 49.4482,
                        Longitude = 2.5895
                    },

                };
        return await Task.Run(() =>
                {
            return list.ToArray();
        });
    }
}
}
