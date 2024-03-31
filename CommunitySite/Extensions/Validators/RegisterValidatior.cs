using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using FluentValidation;

namespace CommunitySite.Extensions.Validators
{
    public class RegisterValidatior : AbstractValidator<UserViewModel>
    {
        private readonly IUserService _userService;
        public RegisterValidatior(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.SurName)
                .NotEmpty().WithMessage("Your surname cannot be empty")
                .Length(1, 99);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Your lastname cannot be empty")
                .Length(1, 99);

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Your email cannot be empty")
            .EmailAddress().WithMessage("This not a valid email adress")
            .Length(1, 99)
            .MustAsync(async (email, _) =>
                await _userService.ExistUser(email)
            ).WithMessage("An email already exist");

            RuleFor(x => x.Passwords)
                .NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

            RuleFor(x => x.BirthYear)
                .NotEmpty()
                .InclusiveBetween(1900, 2024).WithMessage("Your birthyear not valid");

            RuleFor(x => x.BirthMonth)
                .NotEmpty()
                .InclusiveBetween(1, 12).WithMessage("Your birthmonth not valid");

            RuleFor(x => x.BirthDay)
                .NotEmpty()
                .InclusiveBetween(1, 31).WithMessage("Your birthday not valid");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UserViewModel>.CreateWithOptions((UserViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
