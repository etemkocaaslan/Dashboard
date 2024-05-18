using Moq;

namespace GoogleCalendarIntegration.UnitTests
{
    public class GoogleCalendarServiceTests
    {
        private readonly Mock<GoogleCalendarService> _mockService;
        private readonly GoogleCalendarService _service;

        public GoogleCalendarServiceTests()
        {
            _mockService = new Mock<GoogleCalendarService>();
            _mockService.Setup(s => s.GetUpcomingEventsAsync(It.IsAny<int>())).ReturnsAsync(GetMockEvents());
            _service = _mockService.Object;
        }

        private List<CalendarEvent> GetMockEvents()
        {
            return new List<CalendarEvent>
            {
                new CalendarEvent
                {
                    Id = "1",
                    Summary = "Test Event 1",
                    Description = "Description for Test Event 1",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1)
                },
                new CalendarEvent
                {
                    Id = "2",
                    Summary = "Test Event 2",
                    Description = "Description for Test Event 2",
                    Start = DateTime.Now.AddHours(1),
                    End = DateTime.Now.AddHours(2)
                }
            };
        }

        [Fact]
        public async Task GetUpcomingEventsAsync_ShouldReturnEvents()
        {
            var events = await _service.GetUpcomingEventsAsync();
            Assert.NotNull(events);
            Assert.NotEmpty(events);
        }

        [Fact]
        public async Task AddEventAsync_ShouldAddEvent()
        {
            var newEvent = new CalendarEvent
            {
                Summary = "New Test Event",
                Description = "Description for New Test Event",
                Start = DateTime.Now.AddHours(2),
                End = DateTime.Now.AddHours(3)
            };

            await _service.AddEventAsync(newEvent);
            // Verify if AddEventAsync was called with correct parameters
            _mockService.Verify(s => s.AddEventAsync(It.Is<CalendarEvent>(e => e.Summary == "New Test Event")), Times.Once);
        }
    }
}
