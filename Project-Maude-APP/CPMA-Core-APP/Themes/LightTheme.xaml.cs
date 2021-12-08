using CPMA_Core_APP.Common;
using static CPMA_Core_APP.Common.StaticVariables.ThemeStrings;
using Xamarin.Forms;
using FFImageLoading.Svg.Forms;
using System.Collections.Generic;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.Themes
{
    public partial class LightTheme : ResourceDictionary
    {
        public LightTheme()
        {
            InitializeComponent();

            Add(TextColor, Color.Black);

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

            Add(BackgroundColor, Color.White);
            Add(BackgroundStartColor, Color.White);
            Add(BackgroundEndColor, Color.White);
            Add(BackgroundColorHex, Colors.Hex.C5MidBlueHex);
            Add(BackgroundWarningRed, Colors.Hex.ITBWarnRedHex);
            Add(BackgroundWarningGreen, Colors.Hex.ITBWarnGreenHex);
            Add(HeadingColor, Colors.Hex.ITBHeadingHex);
            Add(HeadingFontColor, Colors.Hex.ITBHeadingFontHex);

            Add(ButtonPrimaryColor, Colors.AccidentalGreen);
            Add(ScanButtonColor, Colors.PineGreen);
            Add(ButtonSecondaryColor, Color.White);

            //Fonts
            Add(p1, Fonts.p1);
            Add(FontSize1, 16);

            //string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();

            Add(CrossSource, "resource://CPMA_Core_APP.Resources.Icons.Cross.png");
            Add(CameraSource, "resource://CPMA_Core_APP.Resources.Icons.Camera.png");
            Add(WhiteCameraSource, "resource://CPMA_Core_APP.Resources.Icons.WhiteCamera.png");
            Add(MagnifyingGlassSource, "resource://CPMA_Core_APP.Resources.Icons.MagnifyingGlass.png");
            Add(UpvoteSource, "resource://CPMA_Core_APP.Resources.Icons.Upvote.png");
            Add(DownvoteSource, "resource://CPMA_Core_APP.Resources.Icons.Downvote.png");
            Add(ProfileIconSource, "resource://CPMA_Core_APP.Resources.Icons.ProfileIcon.png");
            Add(SubmitIconSource, "resource://CPMA_Core_APP.Resources.Icons.SubmitIcon.png");
            Add(ExpandIconSource, "resource://CPMA_Core_APP.Resources.Icons.Expand.png");
            Add(CheckmarkSource, "resource://CPMA_Core_APP.Resources.Icons.Checkmark.png");
            Add(VerifiedIconSource, "resource://CPMA_Core_APP.Resources.Icons.VerifiedIcon.png");
            Add(FlaggedIconSource, "resource://CPMA_Core_APP.Resources.Icons.FlaggedIcon.png");
            Add(MapIconSource, "resource://CPMA_Core_APP.Resources.Icons.MapIcon.png");
            Add(InfoIconSource, "resource://CPMA_Core_APP.Resources.Icons.infobutton.png");
            Add(BlankSource, "resource://CPMA_Core_APP.Resources.Icons.Blank.png");
            Add(BarcodeSource, "resource://CPMA_Core_APP.Resources.Icons.Barcode.png");
        }
    }
}