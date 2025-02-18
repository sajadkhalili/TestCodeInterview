using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.EF.Commands.SubjectTypes;
public class SubjectTypeIdConverter : ValueConverter<SubjectTypeId, int>
{
    public SubjectTypeIdConverter () :
        base(c => c.Value, c => new SubjectTypeId(c))
    {

    }
}

