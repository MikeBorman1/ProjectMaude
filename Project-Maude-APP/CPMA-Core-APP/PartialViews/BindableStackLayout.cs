using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CPMA_Core_APP.PartialViews
{
    public class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable), 
            typeof(BindableStackLayout),
            propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateItems());

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(BindableStackLayout));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        
        void PopulateItems()
        {
            Children.Clear();
            if (ItemsSource == null) return;
            foreach (var item in ItemsSource)
            {
                var itemTemplate = ItemTemplate.CreateContent() as View;
                if (itemTemplate == null)
                    throw new Exception("ItemTemplate must construct have a top level result in a View");
                itemTemplate.BindingContext = item;
                Children.Add(itemTemplate);
            }
        }
    }
}
