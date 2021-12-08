using System;
using System.Collections.Generic;
using System.Linq;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace CPMA_Core_APP.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public UISearchBar bar { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
        {
            base.OnElementChanged(args);
            if(this.Control != null)
            {
                bar = (UISearchBar)this.Control;

                bar.BarTintColor = UIColor.White;
                bar.BackgroundColor = UIColor.White;
            }
        }
    }
}