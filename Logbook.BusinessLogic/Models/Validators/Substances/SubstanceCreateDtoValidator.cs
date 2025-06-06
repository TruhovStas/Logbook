﻿using Logbook.BusinessLogic.Models.Substances;
using FluentValidation;

namespace Logbook.BusinessLogic.Models.Validators.Substances
{
    public class SubstanceCreateDtoValidator : AbstractValidator<SubstanceCreateDto>, IValidator
    {
        public SubstanceCreateDtoValidator()
        {
            RuleFor(x => x.Formula)
                .NotEmpty().WithMessage("Формула не может быть пустой.");

            RuleFor(x => x.MolarMass)
                .NotEmpty().WithMessage("Молярная масса не может быть пустой.")
                .GreaterThan(0).WithMessage("Молярная масса должна быть положительной.");

            RuleFor(x => x.Concentration)
                .NotEmpty().WithMessage("концентрация не может быть пустой.")
                .GreaterThan(0).WithMessage("Концентрация должна быть положительной.");
        }
    }
}
