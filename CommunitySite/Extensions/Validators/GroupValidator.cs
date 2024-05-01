using CommunitySite.Data.ViewModels;
using FluentValidation;

namespace CommunitySite.Extensions.Validators
{
    public class GroupValidator : AbstractValidator<GroupViewModel>
    {
        public GroupValidator()
        {
            RuleFor(x => x.Name)
                .Length(0, 100)
                .WithMessage("The text is too long!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<GroupViewModel>.CreateWithOptions((GroupViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
