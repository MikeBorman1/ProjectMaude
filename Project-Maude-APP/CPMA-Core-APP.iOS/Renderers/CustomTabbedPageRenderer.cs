using System;
using System.Threading.Tasks;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CPMA_Core_APP.Common;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CPMA_Core_APP.iOS.Renderers

{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TabBar.BackgroundColor = Colors.GRGreen.ToUIColor();


            if (TabBar.Items != null)
            {
                var items = TabBar.Items;
                foreach (var t in items)
                {
                    var txtFont = new UITextAttributes()
                    {
                        //Font = UIFont.FromName("Quicksand-Bold",16),
                        TextColor = UIColor.White,
                    };

                    var txtFontActive = new UITextAttributes()
                    {
                        //Font = UIFont.FromName("Quicksand-Bold", 16),
                        TextColor = Color.FromHex("#C9E8FB").ToUIColor()    
                    };

                    t.SetTitleTextAttributes(txtFont, UIControlState.Normal);
                    t.SetTitleTextAttributes(txtFontActive, UIControlState.Selected);
                }
            }
        }        
    }
}

