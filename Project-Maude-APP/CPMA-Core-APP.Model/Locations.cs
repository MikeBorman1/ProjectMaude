using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Locations
    { 
        [JsonProperty(PropertyName = "locationName")]
        public string RecycleBin { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "longitude")]
        public double Longitude { get; set; }
    }
}
