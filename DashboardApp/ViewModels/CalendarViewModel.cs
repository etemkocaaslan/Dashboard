using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DashboardApp.Models;

namespace DashboardApp.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private ObservableCollection<CalendarEvent> _calendarEvents = new ObservableCollection<CalendarEvent>();
        public ObservableCollection<CalendarEvent> CalendarEvents
        {
            get => _calendarEvents;
            set => SetProperty(ref _calendarEvents, value);
        }

        private CalendarEvent _selectedCalendarEvent;
        public CalendarEvent SelectedCalendarEvent
        {
            get => _selectedCalendarEvent;
            set
            {
                SetProperty(ref _selectedCalendarEvent, value);
                EditEventCommand.NotifyCanExecuteChanged();
                DeleteEventCommand.NotifyCanExecuteChanged();
            }
        }

        private CalendarEvent _newCalendarEvent = new CalendarEvent();
        public CalendarEvent NewCalendarEvent
        {
            get => _newCalendarEvent;
            set => SetProperty(ref _newCalendarEvent, value);
        }

        public IRelayCommand AddEventCommand { get; }
        public IRelayCommand EditEventCommand { get; }
        public IRelayCommand DeleteEventCommand { get; }

        public CalendarViewModel()
        {
            AddEventCommand = new RelayCommand(AddEvent);
            EditEventCommand = new RelayCommand(EditEvent, () => SelectedCalendarEvent != null);
            DeleteEventCommand = new RelayCommand(DeleteEvent, () => SelectedCalendarEvent != null);
        }

        private void AddEvent()
        {
            if (!string.IsNullOrWhiteSpace(NewCalendarEvent.Title))
            {
                NewCalendarEvent.Id = CalendarEvents.Count + 1;
                CalendarEvents.Add(new CalendarEvent
                {
                    Id = NewCalendarEvent.Id,
                    Title = NewCalendarEvent.Title,
                    StartDate = NewCalendarEvent.StartDate,
                    EndDate = NewCalendarEvent.EndDate,
                    Description = NewCalendarEvent.Description
                });
                NewCalendarEvent = new CalendarEvent();
            }
        }

        private void EditEvent()
        {
            if (SelectedCalendarEvent != null)
            {
                var item = CalendarEvents.FirstOrDefault(e => e.Id == SelectedCalendarEvent.Id);
                if (item != null)
                {
                    item.Title = SelectedCalendarEvent.Title;
                    item.StartDate = SelectedCalendarEvent.StartDate;
                    item.EndDate = SelectedCalendarEvent.EndDate;
                    item.Description = SelectedCalendarEvent.Description;
                }
            }
        }

        private void DeleteEvent()
        {
            if (SelectedCalendarEvent != null)
            {
                CalendarEvents.Remove(SelectedCalendarEvent);
            }
        }
    }
}
