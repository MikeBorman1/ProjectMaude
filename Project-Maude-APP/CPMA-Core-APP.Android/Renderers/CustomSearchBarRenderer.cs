using System;
using Android.Content;
using Android.Graphics;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace CPMA_Core_APP.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {

        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
        {
            base.OnElementChanged(args);
        }
    }
}