using Logbook.BusinessLogic.Models.Forecasts;
using FluentValidation;

namespace Logbook.BusinessLogic.Models.Validators.Forecasts
{
    public class ForecastUpdateDtoValidator : AbstractValidator<ForecastUpdateDto>, IValidator
    {
        public ForecastUpdateDtoValidator()
        {
            RuleFor(x => x.DepositionDate)
                .NotEmpty().WithMessage("Дата и время снятия показателей не могут быть пустыми.");

            RuleFor(x => x.Temprature1)
                .NotEmpty().WithMessage("Температура не может быть пустой.");

            RuleFor(x => x.Humidity1)
                .GreaterThanOrEqualTo(0).WithMessage("Влажность не может быть ниже 0%.")
                .LessThanOrEqualTo(100).WithMessage("Влажность не может быть выше 100%.");

            RuleFor(x => x.Pressure1)
                .NotEmpty().WithMessage("Давление не может быть пустым.");

            RuleFor(x => x.Temprature2)
                .NotEmpty().WithMessage("Температура не может быть пустой.");

            RuleFor(x => x.Humidity2)
                .GreaterThanOrEqualTo(0).WithMessage("Влажность не может быть ниже 0%.")
                .LessThanOrEqualTo(100).WithMessage("Влажность не может быть выше 100%.");

            RuleFor(x => x.Pressure2)
                .NotEmpty().WithMessage("Давление не может быть пустым.");

            RuleFor(x => x.TempratureBox)
                .NotEmpty().WithMessage("Температура не может быть пустой.");

            RuleFor(x => x.HumidityBox)
                .GreaterThanOrEqualTo(0).WithMessage("Влажность не может быть ниже 0%.")
                .LessThanOrEqualTo(100).WithMessage("Влажность не может быть выше 100%.");

            RuleFor(x => x.PressureBox)
                .NotEmpty().WithMessage("Давление не может быть пустым.");

            RuleFor(x => x.Equipments)
                .NotEmpty().WithMessage("Оборудование не может быть пустым.");
        }
    }
}
