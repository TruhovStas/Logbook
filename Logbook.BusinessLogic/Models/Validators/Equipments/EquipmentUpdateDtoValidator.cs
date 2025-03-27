using FluentValidation;
using Logbook.BusinessLogic.Models.Equipments;

namespace Logbook.BusinessLogic.Models.Validators.Equipments
{
    public class EquipmentUpdateDtoValidator : AbstractValidator<EquipmentUpdateDto>, IValidator
    {
        public EquipmentUpdateDtoValidator() 
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
