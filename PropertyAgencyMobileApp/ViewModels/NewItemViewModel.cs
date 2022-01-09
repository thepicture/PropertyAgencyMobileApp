using PropertyAgencyMobileApp.Models;
using PropertyAgencyMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Event currentEvent = new Event();
        private DateTime eventDate = DateTime.Today;
        private TimeSpan eventTime;
        private int? durationInMinutes;
        private IEnumerable<OptionEventType> eventTypes;
        private OptionEventType currentEventType;
        private string comment;

        public NewItemViewModel()
        {
            EventTypes = new List<OptionEventType>
            {
                new OptionEventType("Meeting", "meeting"),
                new OptionEventType("Presentation", "presentation"),
                new OptionEventType("Phone call", "phone_call"),
            };

            CurrentEventType = EventTypes.First();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            bool isDateCorrect = EventDate != null;
            bool isTimeCorrect = EventTime != null;
            bool isSelectedDateHasSense =
                EventDate + EventTime > DateTime.Now;
            bool isDurationInMinutesNullOrHasSense = DurationInMinutes == null
                                                     || DurationInMinutes > 1;
            return isDateCorrect && isTimeCorrect
                && isSelectedDateHasSense
                && isDurationInMinutesNullOrHasSense;
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Event CurrentEvent
        {
            get => currentEvent;
            set => SetProperty(ref currentEvent, value);
        }
        public DateTime EventDate
        {
            get => eventDate;
            set => SetProperty(ref eventDate, value);
        }
        public TimeSpan EventTime
        {
            get => eventTime;
            set => SetProperty(ref eventTime, value);
        }
        public int? DurationInMinutes
        {
            get => durationInMinutes;
            set => SetProperty(ref durationInMinutes, value);
        }
        public IEnumerable<OptionEventType> EventTypes
        {
            get => eventTypes;
            set => SetProperty(ref eventTypes, value);
        }
        public OptionEventType CurrentEventType
        {
            get => currentEventType;
            set => SetProperty(ref currentEventType, value);
        }
        public string Comment
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }

        private async void OnCancel()
        {
            if (await FeedbackService.Ask("Do you really want to cancel " +
                "adding a new event?"))
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnSave()
        {
            Event @event = new Event
            {
                agent_id = new AgentSurrogateIdResolver().ResolveId(),
                comment = Comment,
                datetime = ((DateTimeOffset)EventDate).ToUnixTimeSeconds(),
                duration = DurationInMinutes,
                type = CurrentEventType.Value,
            };
            _ = await DataStore.AddItemAsync(@event);
            await FeedbackService.Inform("The event was successfully added");
            await Shell.Current.GoToAsync("..");
        }
    }
}