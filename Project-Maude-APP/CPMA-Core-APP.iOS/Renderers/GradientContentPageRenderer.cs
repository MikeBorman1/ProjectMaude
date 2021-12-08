using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.iOS.Renderers;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientContentPage), typeof(GradientContentPageRenderer))]
namespace CPMA_Core_APP.iOS.Renderers
{
    public class GradientContentPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Element.PropertyChanged += OnElementPropertyChanged;
        }

        protected  void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GradientContentPage.StartColorProperty.PropertyName || e.PropertyName == GradientContentPage.EndColorProperty.PropertyName)
            {
                var page = sender as GradientContentPage;
                var gradientLayer = new CAGradientLayer();
                gradientLayer.Frame = View.Bounds;
                gradientLayer.Colors = new CGColor[] { page.StartColor.ToCGColor(), page.EndColor.ToCGColor() };
                var oldLayer = View.Layer.Sublayers[0];
                View.Layer.ReplaceSublayer(oldLayer, gradientLayer);
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) // perform initial setup
            {
                var page = e.NewElement as GradientContentPage;
                var gradientLayer = new CAGradientLayer();
                gradientLayer.Frame = View.Bounds;
                gradientLayer.Colors = new CGColor[] { page.StartColor.ToCGColor(), page.EndColor.ToCGColor() };
                View.Layer.InsertSublayer(gradientLayer, 0);
            }
        }
    }
}