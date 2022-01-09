using PropertyAgencyMobileApp.Services;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<EventDataStore>();
            DependencyService.Register<AlertFeedbackService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
