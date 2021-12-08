using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Warning
    {
        [JsonProperty(Required = Required.Always, PropertyName = "warning")]
        public string WarningMessage { get; set; }
        
    }
}