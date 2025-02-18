namespace SSF.Surveys.Domain.SubjectTypes.ValueObjects;

public class SubjectId : BaseValueObject<SubjectId>
{
    public SubjectId ( long value )
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(value));
        Value=value;
    }

    public long Value { get; protected set; }

    protected override IEnumerable<object> GetEqualityComponents ()
    {
        yield return Value;
    }
    public static SubjectId FromLong ( long value )
    {
        return new SubjectId(value);
    }
    public long ToLong ()
    {
        return Value;
    }
    public override string ToString ()
    {
        return Value.ToString();
    }
}