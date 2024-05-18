using GoogleCalendarIntegration;

namespace GoogleCalendarMiddleware
{
    public class CalendarMiddleware
    {
        private readonly GoogleCalendarService _googleCalendarService;

        public CalendarMiddleware(string credentialsPath)
        {
            _googleCalendarService = new GoogleCalendarService(credentialsPath);
        }

        public async Task<List<CalendarEvent>> GetEventsAsync(int maxResults = 10)
        {
            return await _googleCalendarService.GetUpcomingEventsAsync(maxResults);
        }

        public async Task AddEventAsync(CalendarEvent newEvent)
        {
            await _googleCalendarService.AddEventAsync(newEvent);
        }

        public async Task UpdateEventAsync(CalendarEvent updatedEvent)
        {
            await _googleCalendarService.UpdateEventAsync(updatedEvent);
        }

        public async Task DeleteEventAsync(string eventId)
        {
            await _googleCalendarService.DeleteEventAsync(eventId);
        }
    }
}
