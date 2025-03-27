using FluentValidation;
using Logbook.BusinessLogic.Models.Users;
using Microsoft.AspNetCore.Identity;
using Logbook.DataAccess.Entities;

namespace Logbook.BusinessLogic.Models.Validators.Users
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>, IValidator
    {
        private readonly UserManager<User> _userManager;
        public UserCreateDtoValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("����� �� ����� ���� ������.")
                .MaximumLength(UserValidatorConfiguration.UserNameMaxLength)
                .WithMessage($"����� ������ ���� ������ {UserValidatorConfiguration.UserNameMaxLength} ��������.")
                .Must(UsernameIsUnique)
                .WithMessage("������������ � ����� ������� ��� ����������.");

            RuleFor(x => x.Fio)
                .NotEmpty().WithMessage("��� �� ������ ���� ������.")
                .WithMessage("������������ � ����� ��� ��� ����������.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("������ �� ����� ���� ������.")
                .MinimumLength(UserValidatorConfiguration.PasswordMinLength)
                .WithMessage($"������ ������ ��������� ���� �� {UserValidatorConfiguration.PasswordMinLength} ��������.")
                .MaximumLength(UserValidatorConfiguration.PasswordMaxLength)
                .WithMessage($"������ �� ����� ��������� {UserValidatorConfiguration.PasswordMaxLength} ��������.");
        }

        private bool UsernameIsUnique(string username)
        {
            var user = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            return user == null;
        }
    }
}