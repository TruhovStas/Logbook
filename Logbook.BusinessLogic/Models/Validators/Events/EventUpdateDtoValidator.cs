using EventsWeb.BusinessLogic.Models.Events;
using FluentValidation;

namespace EventsWeb.BusinessLogic.Models.Validators.Events
{
    public class EventUpdateDtoValidator : AbstractValidator<EventUpdateDto>, IValidator
    {
        public EventUpdateDtoValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be specified.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tilte cannot be empty.")
                .MaximumLength(EventValidatorConfiguration.TitleMaxLength)
                .WithMessage($"Name cannot exceed {EventValidatorConfiguration.TitleMaxLength} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location cannot be empty.");

            RuleFor(x => x.DateTime)
                .NotEmpty().WithMessage("Date cannot be empty.")
                .GreaterThan(DateTime.Now).WithMessage("Date must be in the future.");

            RuleFor(x => x.Category)
                .MaximumLength(EventValidatorConfiguration.CategoryMaxLength)
                .WithMessage($"Category name cannot exceed {EventValidatorConfiguration.CategoryMaxLength} characters.");

            RuleFor(x => x.MaxParticipants)
                .NotEmpty().WithMessage("Maximum amount of participants cannot be empty.")
                .GreaterThan(0).WithMessage("Maximum amount of participants must be positive.");
        }
    }
}
