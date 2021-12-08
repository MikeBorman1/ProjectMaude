using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CPMA_Core_APP.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Report
    {
        [JsonProperty(Required = Required.Default, PropertyName = "reportImageUrl")]
        public string ReportImageUrl { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "reportScore")]
        public int ReportScore { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "reportTitle")]
        public string ReportTitle { get; set; }

        [JsonProperty(Required = Required.AllowNull, PropertyName = "barcode")]
        public string Barcode { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "reportId")]
        public int ReportId { get; set; }

        public void Load() {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            ReportImageUrl = textInfo.ToLower(ReportImageUrl);
        }
    }
}
