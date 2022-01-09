using System.Threading.Tasks;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.Services
{
    public class AlertFeedbackService : IFeedbackService
    {
        private readonly Page _page;

        public AlertFeedbackService()
        {
            _page = Application.Current.MainPage;
        }

        public async Task<bool> Ask(string message)
        {
            return await _page.DisplayAlert("Question",
                                            message,
                                            "Yes",
                                            "No");
        }

        public async Task Inform(string message)
        {
            await _page.DisplayAlert("Information",
                                            message,
                                            "OK");
        }

        public async Task Panic(string message)
        {
            await _page.DisplayAlert("Error",
                                            message,
                                            "OK");
        }

        public async Task Warn(string message)
        {
            await _page.DisplayAlert("Warning",
                                            message,
                                            "OK");
        }
    }
}
