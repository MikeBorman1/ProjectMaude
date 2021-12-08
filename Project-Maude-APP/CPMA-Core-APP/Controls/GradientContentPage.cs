using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CPMA_Core_APP.Controls
{
    public class GradientContentPage : ContentPage
    {
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(
            nameof(StartColor),
            typeof(Color),
            typeof(GradientContentPage));

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(
            nameof(EndColor),
            typeof(Color),
            typeof(GradientContentPage));

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
    }
}
