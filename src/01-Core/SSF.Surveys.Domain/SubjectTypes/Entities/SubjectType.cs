using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Entities;

public class SubjectType : AggregateRoot<SubjectTypeId>
{
    public PersianString Title { get; private set; }
    protected SubjectType ()
    {

    }

    public SubjectType ( PersianString title )
    {
        Title=title;
    }

    public void Edit ( PersianString title )
    {
        Title=title;
    }

    #region Navigation Property

    #endregion Navigation Property  
}
