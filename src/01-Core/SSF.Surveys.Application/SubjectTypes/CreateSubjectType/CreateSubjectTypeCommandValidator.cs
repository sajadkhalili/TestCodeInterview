using Microsoft.Extensions.Localization;

using SSF.Surveys.Domain.SubjectTypes.Contracts.CreateSubjectType;

namespace SSF.Surveys.Application.SubjectTypes.CreateSubjectType;

public class CreateSubjectTypeCommandValidator : ModelValidator<CreateSubjectTypeCommand>
{

    public CreateSubjectTypeCommandValidator ( IStringLocalizer<SharedResource> localizer )
    {

        ClassLevelCascadeMode=CascadeMode.Stop;
        RuleLevelCascadeMode=CascadeMode.Stop;

        RuleFor(expression: st => st.Title)
              .NotNull().WithMessage("Error.Validation.NotNull")
            .NotEmpty().WithMessage("Error.Validation.NotEmpty")
        .Length(3, 30).WithMessage(string.Format(localizer["Error.Validation.MinMaxLength"].Value, 3, 30))

       .Must(title => !string.IsNullOrEmpty(title)&&title.IsPersian())

        .WithMessage(localizer["Error.Validation.MustBePersian"].Value);

    }
}
