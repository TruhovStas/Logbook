using Logbook.BusinessLogic.Models.Equipments;
using FluentValidation;

namespace Logbook.BusinessLogic.Models.Validators.Equipments
{
    public class EquipmentCreateDtoValidator : AbstractValidator<EquipmentCreateDto>, IValidator
    {
        public EquipmentCreateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым.");

            RuleFor(x => x.SerialNumber)
                .NotEmpty().WithMessage("Заводской номер не может быть пустым.");
        }
    }
}
