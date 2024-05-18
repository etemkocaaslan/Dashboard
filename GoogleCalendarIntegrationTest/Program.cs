using GoogleCalendarIntegration;

namespace GoogleCalendarIntegrationTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "credentials.json");
            if (!File.Exists(credentialsPath))
            {
                Console.WriteLine("The credentials file does not exist at the specified path: " + credentialsPath);
                return;
            }

            var calendarService = new GoogleCalendarService();

            calendarService.InitializeService(credentialsPath);

            Console.WriteLine("Fetching upcoming events...");
            var events = await calendarService.GetUpcomingEventsAsync();
            DisplayEvents(events);

            Console.WriteLine("\nAdding a new event...");
            var newEvent = new CalendarEvent
            {
                Summary = "Test Event",
                Description = "This is a test event.",
                Start = DateTime.Now.AddHours(1),
                End = DateTime.Now.AddHours(2)
            };
            await calendarService.AddEventAsync(newEvent);
            Console.WriteLine("Event added.");

            Console.WriteLine("\nFetching updated list of events...");
            events = await calendarService.GetUpcomingEventsAsync();
            DisplayEvents(events);
        }

        private static void DisplayEvents(System.Collections.Generic.List<CalendarEvent> events)
        {
            if (events.Count == 0)
            {
                Console.WriteLine("No upcoming events found.");
                return;
            }

            foreach (var evt in events)
            {
                Console.WriteLine($"Event: {evt.Summary}\nDescription: {evt.Description}\nStart: {evt.Start}\nEnd: {evt.End}\n");
            }
        }
    }
}
