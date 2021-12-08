using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class MaterialSearch
    {
        [JsonProperty(Required = Required.Always, PropertyName = "material")]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "isRecyclable")]
        public bool Recyclable { get; set; }

        [JsonProperty(PropertyName = "imageID")]
        public string ImageUrl { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "recycleBin")]
        public string RecycleBin { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "isBin")]
        public bool IsBin { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "materialID")]
        public int MaterialID { get; set; }


    }
}
