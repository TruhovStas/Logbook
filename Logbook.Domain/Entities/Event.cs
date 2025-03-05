namespace EventsWeb.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string? Category { get; set; }
        public int MaxParticipants { get; set; }
        public virtual IEnumerable<Participant>? Participants { get; set; }
        public string? Image { get; set; }
    }
}
