using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;
using SSF.Surveys.EF.Commands.SubjectTypes;

namespace SSF.Surveys.EF.Commands.Common;
public class SurveyContext ( DbContextOptions<SurveyContext> dbContextOptions ) : SqlCommandDbContext(dbContextOptions)
{
    //public DbSet<Question> Questions { get; set; }
    //public DbSet<SurveyTime> SurveyTimes { get; set; }
    //public DbSet<Response> Responses { get; set; }
    public DbSet<SubjectType> SubjectTypes { get; set; }
    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<SubjectType>().Property(st => st.Id).ValueGeneratedOnAdd();
        //    .HasConversion<SubjectTypeIdConverter>();
        modelBuilder.ApplyConfiguration(new SubjectTypeConfiguration());

        modelBuilder.HasDefaultSchema(InfraConstants.SurveySchemaName);
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(SurveyContext).Assembly);
    }
    protected override void ConfigureConventions ( ModelConfigurationBuilder configurationBuilder )
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<SubjectTypeId>()
           .HaveConversion<SubjectTypeIdConverter>();

    }
}

