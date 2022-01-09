using PropertyAgencyMobileApp.Models;
using PropertyAgencyMobileApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Event> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "My events today";
            Items = new ObservableCollection<Event>();
            LoadItemsCommand =
                new Command(async () => await ExecuteLoadItemsCommand());

            AddItemCommand = new Command(OnAddItem);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (Event item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private Command cancelEvent;

        public ICommand CancelEvent
        {
            get
            {
                if (cancelEvent == null)
                {
                    cancelEvent = new Command<Event>(PerformCancelEvent);
                }

                return cancelEvent;
            }
        }

        private async void PerformCancelEvent(Event @event)
        {
            if (@event == null)
            {
                return;
            }

            if (await FeedbackService
                .Ask("Do you really want to delete the event?"))
            {
                await DataStore.DeleteItemAsync(@event.uuid);
                await FeedbackService
                    .Inform("The event was successfully deleted");
                IsBusy = true;
            }
        }
    }
}