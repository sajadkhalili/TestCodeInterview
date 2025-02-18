using SSF.Surveys.Domain.SubjectTypes.Entities;

namespace SSF.Surveys.EF.Commands.SubjectTypes;

public sealed class SubjectTypeConfiguration : BaseEntityTypeConfiguration<SubjectType>
{
    public override void Configure ( EntityTypeBuilder<SubjectType> builder )
    {
        base.Configure(builder);

        builder.Property(st => st.Id).ValueGeneratedOnAdd();

        //     builder.HasKey(x => x.Id).HasName("Id");
        //builder.Property(s=>s.Id).ValueGeneratedOnAdd()
        //    .HasConversion<SubjectTypeIdConverter>();
    }
}
