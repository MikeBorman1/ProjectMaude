using CPMA_Core_APP.Common;
using static CPMA_Core_APP.Common.StaticVariables.ThemeStrings;
using Xamarin.Forms;
using FFImageLoading.Svg.Forms;
using System.Collections.Generic;

namespace CPMA_Core_APP.Themes
{
    public partial class DarkTheme : ResourceDictionary
    {
        public DarkTheme()
        {
            InitializeComponent();

            Add(TextColor, Color.White);

            Color loginRegisterTextColor;
            if (Device.RuntimePlatform == Device.iOS)
            {
                loginRegisterTextColor = Color.Black;
            }
            else
            {
                loginRegisterTextColor = Color.White;
            }
            Add(LoginRegisterTextColor, loginRegisterTextColor);

            Color loginRegisterButtonTextColor;
            if (Device.RuntimePlatform == Device.iOS)
            {
                loginRegisterButtonTextColor = Color.White;
            }
            else
            {
                loginRegisterButtonTextColor = Color.Black;
            }
            Add(LoginRegisterButtonTextColor, loginRegisterButtonTextColor);

            Add(BackgroundColor, Color.Black);
            Add(BackgroundStartColor, Color.Black);
            Add(BackgroundEndColor, Color.Black);
            Add(BackgroundColorHex, "#FFFFFF");

        }
    }
}