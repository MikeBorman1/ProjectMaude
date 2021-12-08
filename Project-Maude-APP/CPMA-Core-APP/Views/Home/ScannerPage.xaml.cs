using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPMA_Core_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();

            scanView.Options.DelayBetweenAnalyzingFrames = 5;
            scanView.Options.DelayBetweenContinuousScans = 5;
        }

        void Handle_FlashButtonClicked(object sender, System.EventArgs e)
        {
            scanView.IsTorchOn = !scanView.IsTorchOn;
        }
    }
}