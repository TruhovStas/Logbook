namespace EventsWeb.BusinessLogic.Models.Participants
{
    public class ParticipantCreateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public int EventId { get; set; }
    }
}
