using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPMA_Core_APP.Views
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        //code behind prevent hardware backbutton
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}