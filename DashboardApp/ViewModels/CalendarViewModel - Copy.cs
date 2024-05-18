using CommunityToolkit.Mvvm.Input;
using GoogleCalendarIntegration;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DashboardApp.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private GoogleCalendarService _calendarService;
        public ObservableCollection<CalendarEvent> Events { get; set; }

        public ICommand LoadEventsCommand { get; }
        public ICommand AddEventCommand { get; }
        public ICommand UpdateEventCommand { get; }
        public ICommand DeleteEventCommand { get; }

        public CalendarViewModel()
        {
            _calendarService = new GoogleCalendarService("path/to/credentials.json");
            Events = new ObservableCollection<CalendarEvent>();

            LoadEventsCommand = new RelayCommand(async () => await LoadEvents());
            AddEventCommand = new RelayCommand<CalendarEvent>(async (e) => await AddEvent(e));
            UpdateEventCommand = new RelayCommand<CalendarEvent>(async (e) => await UpdateEvent(e));
            DeleteEventCommand = new RelayCommand<CalendarEvent>(async (e) => await DeleteEvent(e));
        }

        private async Task LoadEvents()
        {
            var events = await _calendarService.GetUpcomingEventsAsync();
            Events.Clear();
            foreach (var eventItem in events)
            {
                Events.Add(eventItem);
            }
        }

        private async Task AddEvent(CalendarEvent newEvent)
        {
            await _calendarService.AddEventAsync(newEvent);
            await LoadEvents();
        }

        private async Task UpdateEvent(CalendarEvent updatedEvent)
        {
            await _calendarService.UpdateEventAsync(updatedEvent);
            await LoadEvents();
        }

        private async Task DeleteEvent(CalendarEvent eventToDelete)
        {
            await _calendarService.DeleteEventAsync(eventToDelete.Id);
            await LoadEvents();
        }
    }

}
