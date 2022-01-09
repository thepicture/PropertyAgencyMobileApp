using PropertyAgencyMobileApp.Views;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}
