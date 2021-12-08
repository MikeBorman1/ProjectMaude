
using CPMA_Core_APP.Controls;
using Foobar.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(GradientContentPage), typeof(GradientContentPageRenderer))]

namespace Foobar.Droid.Renderers
{
    public class GradientContentPageRenderer : PageRenderer
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }

        public GradientContentPageRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
                StartColor.ToAndroid(),
                EndColor.ToAndroid(),
                Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == GradientContentPage.StartColorProperty.PropertyName || e.PropertyName == GradientContentPage.EndColorProperty.PropertyName)
            {
                var page = sender as GradientContentPage;
                StartColor = page.StartColor;
                EndColor = page.EndColor;
                this.Invalidate();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            var page = e.NewElement as GradientContentPage;
            StartColor = page.StartColor;
            EndColor = page.EndColor;
        }
    }
}