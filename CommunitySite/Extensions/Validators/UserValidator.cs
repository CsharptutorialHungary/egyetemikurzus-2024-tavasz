using CommunitySite.Data.ViewModels;
using FluentValidation;

namespace CommunitySite.Extensions.Validators
{
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.SurName)
                .Length(0, 100)
                .WithMessage("The text is too long!");

            RuleFor(x => x.LastName)
                .Length(0, 100)
                .WithMessage("The text is too long!");

            RuleFor(x => x.Workplace)
                .Length(0, 100)
                .WithMessage("The text is too long!");

            RuleFor(x => x.School)
                .Length(0, 100)
                .WithMessage("The text is too long!");
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
