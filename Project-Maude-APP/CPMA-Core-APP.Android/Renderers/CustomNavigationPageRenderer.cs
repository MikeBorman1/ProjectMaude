using System;
using Android.Content;
using Android.Graphics;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace CPMA_Core_APP.Droid.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();

            if (e.Child.GetType() == typeof(Android.Widget.TextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                var quicksand = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "Quicksand-Regular.ttf");
                textView.Typeface = quicksand;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            // If your app is not using the AppCompatTextView, you don't need this check
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var quicksand = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "Quicksand-Regular.ttf");
                textView.Typeface = quicksand;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }

            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatButton))
            {
                var textView = (Android.Support.V7.Widget.AppCompatButton)e.Child;
                var quicksand = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "Quicksand-Regular.ttf");
                textView.Typeface = quicksand;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}