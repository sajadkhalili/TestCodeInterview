using Microsoft.Extensions.Localization;
using SSF.Surveys.Domain.SubjectTypes.Contracts.EditSubjectType;

namespace SSF.Surveys.Application.SubjectTypes.EditSubjectType;

public class EditSubjectTypeCommandValidator : ModelValidator<EditSubjectTypeCommand>
{
    public EditSubjectTypeCommandValidator ( IStringLocalizer<SharedResource> localizer )
    {

        ClassLevelCascadeMode=CascadeMode.Continue;
        RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(expression: st => st.Title)
              .NotNull().WithMessage("Error.Validation.NotNull")
            .NotEmpty().WithMessage("Error.Validation.NotEmpty")
        .Length(3, 30).WithMessage(string.Format(localizer["Error.Validation.MinMaxLenght"].Value, 3, 30))

       .Must(title => title!=null&&StringExtensions.IsPersian(title))
        .WithMessage(localizer["Error.Validation.MustBePersian"].Value);
    }
}
