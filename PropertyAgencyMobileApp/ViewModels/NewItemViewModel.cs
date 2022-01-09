using PropertyAgencyMobileApp.Models;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Event currentEvent = new Event();

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return (CurrentEvent.datetime != 0)
                && (CurrentEvent.duration != 0);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Event CurrentEvent
        {
            get => currentEvent;
            set => SetProperty(ref currentEvent, value);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            await DataStore.AddItemAsync(CurrentEvent);

            await Shell.Current.GoToAsync("..");
        }
    }
}
