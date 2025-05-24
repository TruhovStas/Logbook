using Logbook.BusinessLogic.Models.Solutions;
using FluentValidation;

namespace Logbook.BusinessLogic.Models.Validators.Solutions
{
    public class SolutionUpdateDtoValidator : AbstractValidator<SolutionUpdateDto>, IValidator
    {
        public SolutionUpdateDtoValidator()
        {
            RuleFor(x => x.SolutionVolume)
                .NotEmpty().WithMessage("Объём раствора не может быть пустым.")
                .GreaterThan(0).WithMessage("Объём должен быть положительным.");

            RuleFor(x => x.StorageConditions)
                .NotEmpty().WithMessage("Условия хранения не может быть пустыми.");

            RuleFor(x => x.StoragePeriod)
                .NotEmpty().WithMessage("Срок хранения не может быть пустыми.");

            RuleFor(x => x.SolutionTemperature)
                .NotEmpty().WithMessage("Температура раствора не может быть пустой.");

            RuleFor(x => x.Substance)
                .NotEmpty().WithMessage("Установочное вещество не может быть пустым.");

            RuleFor(x => x.SubstanceMasses)
                .NotEmpty().WithMessage("Навеска не может быть пустой");

            RuleFor(x => x.SolutionVolumes)
                .NotEmpty().WithMessage("Объём не может быть пустой.");
        }
    }
}
