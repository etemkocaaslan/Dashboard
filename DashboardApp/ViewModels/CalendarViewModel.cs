using CommunityToolkit.Mvvm.Input;
using DashboardApp.ViewModels;
using GoogleCalendarIntegration;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

public class CalendarViewModel : ViewModelBase
{
    private GoogleCalendarService _calendarService;
    public ObservableCollection<CalendarEvent> Events { get; set; }

    // Properties for new event details
    private string _newEventSummary;
    public string NewEventSummary
    {
        get { return _newEventSummary; }
        set { _newEventSummary = value; OnPropertyChanged(); }
    }

    private string _newEventDescription;
    public string NewEventDescription
    {
        get { return _newEventDescription; }
        set { _newEventDescription = value; OnPropertyChanged(); }
    }

    private DateTime _newEventStart;
    public DateTime NewEventStart
    {
        get { return _newEventStart; }
        set { _newEventStart = value; OnPropertyChanged(); }
    }

    private DateTime _newEventEnd;
    public DateTime NewEventEnd
    {
        get { return _newEventEnd; }
        set { _newEventEnd = value; OnPropertyChanged(); }
    }

    public ICommand LoadEventsCommand { get; }
    public ICommand AddEventCommand { get; }
    public ICommand UpdateEventCommand { get; }
    public ICommand DeleteEventCommand { get; }

    public CalendarViewModel()
    {
        _calendarService = new GoogleCalendarService();
        InitializeService();

        Events = new ObservableCollection<CalendarEvent>();

        LoadEventsCommand = new RelayCommand(async () => await LoadEvents());
        AddEventCommand = new RelayCommand(async () => await AddEvent());
        UpdateEventCommand = new RelayCommand<CalendarEvent>(async (e) => await UpdateEvent(e));
        DeleteEventCommand = new RelayCommand<CalendarEvent>(async (e) => await DeleteEvent(e));

        // Set default values for the new event
        NewEventStart = DateTime.Now;
        NewEventEnd = DateTime.Now.AddHours(1);
    }

    private void InitializeService()
    {
        string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "credentials.json");
        if (!File.Exists(credentialsPath))
        {
            throw new FileNotFoundException("The credentials file was not found.", credentialsPath);
        }
        _calendarService.InitializeService(credentialsPath);
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

    private async Task AddEvent()
    {
        var newEvent = new CalendarEvent
        {
            Summary = NewEventSummary,
            Description = NewEventDescription,
            Start = NewEventStart,
            End = NewEventEnd
        };

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
