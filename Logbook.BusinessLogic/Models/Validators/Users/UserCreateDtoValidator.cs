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
                .NotEmpty().WithMessage("Логин не может быть пустым.")
                .MaximumLength(UserValidatorConfiguration.UserNameMaxLength)
                .WithMessage($"Логин должен быть больше {UserValidatorConfiguration.UserNameMaxLength} символов.")
                .Must(UsernameIsUnique)
                .WithMessage("Пользователь с таким логином уже существует.");

            RuleFor(x => x.Fio)
                .NotEmpty().WithMessage("ФИО не дможет быть пустым.")
                .WithMessage("Пользователь с таким ФИО уже существует.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не может быть пустым.")
                .MinimumLength(UserValidatorConfiguration.PasswordMinLength)
                .WithMessage($"Пароль должен содержать хотя бы {UserValidatorConfiguration.PasswordMinLength} символов.")
                .MaximumLength(UserValidatorConfiguration.PasswordMaxLength)
                .WithMessage($"Пароль не может превышать {UserValidatorConfiguration.PasswordMaxLength} символов.");
        }

        private bool UsernameIsUnique(string username)
        {
            var user = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            return user == null;
        }
    }
}