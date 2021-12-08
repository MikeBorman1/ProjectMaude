using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Material
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

        [JsonProperty(Required = Required.Always, PropertyName = "recycleCodeID")]
        public int RecycleCode { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "warnings")]
        public ObservableCollection<Warning> Warnings { get; set; }
    }
}
