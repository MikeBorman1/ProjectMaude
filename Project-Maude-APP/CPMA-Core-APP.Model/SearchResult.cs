using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class SearchResult
    {
        [JsonProperty(Required = Required.Always, PropertyName = "productId")]
        public int ProductID { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "productName")]
        public string ProductName { get; set; }

        [JsonProperty(Required = Required.AllowNull, PropertyName = "productPhotoId")]
        public Guid ProductPhotoID { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "mats")]
        public ObservableCollection <Material> Materials{ get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "isVerified")]
        public bool IsVerified { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "flagged")]
        public bool Flagged { get; set; }
    }
}
