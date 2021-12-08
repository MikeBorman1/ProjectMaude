using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class RecycleInfo
    {
        [JsonProperty(Required = Required.Always, PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(Required = Required.Always, PropertyName = "accepted")]
        public string Accepted { get; set; }
        
      
        [JsonProperty(Required = Required.Always, PropertyName = "rejected")]
        public string Rejected { get; set; }


       
        
           
    }
}
