using System;
using System.Collections.Generic;
using System.Linq;
using CPMA_Core_APP.Common;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace CPMA_Core_APP.iOS.Renderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            UIColor ios7BlueColor = Colors.GRGreen.ToUIColor();

            if (e.NewElement != null)
            {
                //var att = new UITextAttributes
                //{
                //    Font = UIFont.FromName("Quicksand", 20)
                //};

                //UINavigationBar.Appearance.SetTitleTextAttributes(att);

                var buttonStyle = new UITextAttributes
                {
                    //Font = UIFont.FromName("Quicksand", 14),
                    TextColor = ios7BlueColor
                };

                UIBarButtonItem.Appearance.SetTitleTextAttributes(buttonStyle, UIControlState.Normal);
            }
        }
    }
}