using PropertyAgencyMobileApp.Models;
using PropertyAgencyMobileApp.ViewModels;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Event Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}