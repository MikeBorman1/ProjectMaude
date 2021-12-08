using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace CPMA_Core_APP.Views
{
    public partial class TabbedHomePage : Controls.CustomTabbedPage
    {
        public TabbedHomePage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            Title = CurrentPage.Title;
        }

        //code behind prevent hardware backbutton
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
