using System.Diagnostics.CodeAnalysis;

namespace SSF.Surveys.Domain.SubjectTypes.ValueObjects;

public class SubjectTypeId : BaseValueObject<SubjectTypeId>
{

    public int Value { get; protected set; }

    private SubjectTypeId ()
    {
    }

    public SubjectTypeId ( [NotNull] int value )
    {
           ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(value));
        Value=value;
    }

    public static SubjectTypeId FromInt ( int value )
    {
        return new SubjectTypeId(value);
    }
    public int ToInt ()
    {
        return Value;
    }
  
    protected override IEnumerable<object> GetEqualityComponents ()
    {
        yield return Value;
    }
    public override string ToString ()
    {
        return Value.ToString();
    }
}
