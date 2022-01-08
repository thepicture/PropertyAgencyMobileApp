using PropertyAgencyMobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}