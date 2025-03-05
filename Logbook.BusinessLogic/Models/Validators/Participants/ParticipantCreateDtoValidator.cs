using EventsWeb.BusinessLogic.Models.Participants;
using FluentValidation;

namespace EventsWeb.BusinessLogic.Models.Validators.Participants
{
    public class ParticipantCreateDtoValidator : AbstractValidator<ParticipantCreateDto>, IValidator
    {
        public ParticipantCreateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(ParticipantValidatorConfiguration.NameMaxLength)
                .WithMessage($"Name cannot exceed {ParticipantValidatorConfiguration.NameMaxLength} characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname cannot be empty.")
                .MaximumLength(ParticipantValidatorConfiguration.LastNameMaxLength)
                .WithMessage($"Surname cannot exceed {ParticipantValidatorConfiguration.LastNameMaxLength} characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birthdate cannot be empty.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("BirthDate must be in the past.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

             RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Id must be specified.")
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
