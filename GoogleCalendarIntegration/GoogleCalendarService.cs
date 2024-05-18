using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace GoogleCalendarIntegration
{
    public class GoogleCalendarService
    {
        private static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private static readonly string ApplicationName = "DashboardApp";
        private CalendarService _service;

        public GoogleCalendarService() { }

        public GoogleCalendarService(string credentialsPath)
        {
            InitializeService(credentialsPath);
        }

        public void InitializeService(string credentialsPath)
        {
            if (!File.Exists(credentialsPath))
            {
                throw new FileNotFoundException("The credentials file was not found.", credentialsPath);
            }

            UserCredential credential = LoadCredentials(credentialsPath);
            _service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private UserCredential LoadCredentials(string credentialsPath)
        {
            using var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read);
            string tokenPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "token.json");

            return GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(tokenPath, true)).Result;
        }

        public virtual async Task<List<CalendarEvent>> GetUpcomingEventsAsync(int maxResults = 10)
        {
            ValidateServiceInitialized();

            var request = _service.Events.List("primary");
            request.MaxResults = maxResults;
            request.TimeMin = DateTime.Now;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            request.SingleEvents = true;

            var events = await request.ExecuteAsync();
            return ConvertToCalendarEvents(events);
        }

        public virtual async Task AddEventAsync(CalendarEvent newEvent)
        {
            ValidateServiceInitialized();

            var eventItem = new Event
            {
                Summary = newEvent.Summary,
                Description = newEvent.Description,
                Start = new EventDateTime { DateTime = newEvent.Start, TimeZone = "America/Los_Angeles" },
                End = new EventDateTime { DateTime = newEvent.End, TimeZone = "America/Los_Angeles" }
            };

            await _service.Events.Insert(eventItem, "primary").ExecuteAsync();
        }

        public virtual async Task UpdateEventAsync(CalendarEvent updatedEvent)
        {
            ValidateServiceInitialized();

            var eventItem = new Event
            {
                Id = updatedEvent.Id,
                Summary = updatedEvent.Summary,
                Description = updatedEvent.Description,
                Start = new EventDateTime { DateTime = updatedEvent.Start, TimeZone = "America/Los_Angeles" },
                End = new EventDateTime { DateTime = updatedEvent.End, TimeZone = "America/Los_Angeles" }
            };

            await _service.Events.Update(eventItem, "primary", eventItem.Id).ExecuteAsync();
        }

        public virtual async Task DeleteEventAsync(string eventId)
        {
            ValidateServiceInitialized();
            await _service.Events.Delete("primary", eventId).ExecuteAsync();
        }

        private void ValidateServiceInitialized()
        {
            if (_service == null)
            {
                throw new InvalidOperationException("Google Calendar Service is not initialized. Call InitializeService method with valid credentials.");
            }
        }

        private List<CalendarEvent> ConvertToCalendarEvents(Events events)
        {
            var calendarEvents = new List<CalendarEvent>();
            foreach (var eventItem in events.Items)
            {
                calendarEvents.Add(new CalendarEvent
                {
                    Id = eventItem.Id,
                    Summary = eventItem.Summary,
                    Description = eventItem.Description,
                    Start = eventItem.Start.DateTime ?? DateTime.Parse(eventItem.Start.Date),
                    End = eventItem.End.DateTime ?? DateTime.Parse(eventItem.End.Date)
                });
            }
            return calendarEvents;
        }
    }
}
