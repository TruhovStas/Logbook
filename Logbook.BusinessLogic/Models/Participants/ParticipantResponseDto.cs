namespace EventsWeb.BusinessLogic.Models.Participants
{
    public class ParticipantResponseDto : BaseResponseDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
    }
}
