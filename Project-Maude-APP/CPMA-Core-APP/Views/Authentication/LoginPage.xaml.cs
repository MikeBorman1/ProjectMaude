using CPMA_Core_APP.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPMA_Core_APP.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
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
