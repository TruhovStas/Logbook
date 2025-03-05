namespace EventsWeb.BusinessLogic.Models.Events
{
    public class EventUpdateDto : BaseResponseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string? Category { get; set; }
        public int MaxParticipants { get; set; }
        public string? Image { get; set; }
    }
}
