namespace SSF.Surveys.Domain.SubjectTypes.Contracts.CreateSubjectType;

public record CreateSubjectTypeCommand : ICommand<CreateSubjectTypeResult>
{
    public string Title { get; private set; }

    public CreateSubjectTypeCommand ( string title )
    {
        Title=title;

    }
}
