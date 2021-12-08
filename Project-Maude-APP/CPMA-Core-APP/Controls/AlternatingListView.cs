using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;
using CPMA_Core_APP.Themes;
using System.Linq;
using System.Collections.Generic;

namespace CPMA_Core_APP.Controls
{
    public class LocationDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // TODO: Maybe some more error handling here
            var listcontainer = (ListView)container;
            var list = new List<string>(listcontainer.ItemsSource.Cast<string>());

            int index = list.IndexOf(item.ToString());

            return index % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
    public class BeachDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // TODO: Maybe some more error handling here
            var listcontainer = (ListView)container;
            var list = new List<object>(listcontainer.ItemsSource.Cast<object>());

            int index = list.IndexOf(item);

            return index % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
