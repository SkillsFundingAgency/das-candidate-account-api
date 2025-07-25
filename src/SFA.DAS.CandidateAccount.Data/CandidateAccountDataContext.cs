using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.CandidateAccount.Data.AboutYou;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Address;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.CandidatePreferences;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Data.Preference;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Configuration;

namespace SFA.DAS.CandidateAccount.Data;

public interface ICandidateAccountDataContext
{
    DbSet<CandidateEntity> CandidateEntities { get; set; }
    DbSet<ApplicationEntity> ApplicationEntities { get; set; }
    DbSet<WorkHistoryEntity> WorkExperienceEntities { get; set; }
    DbSet<TrainingCourseEntity> TrainingCourseEntities { get; set; }
    DbSet<AdditionalQuestionEntity> AdditionalQuestionEntities { get; set; }
    DbSet<AboutYouEntity> AboutYouEntities { get; set; }
    DbSet<AddressEntity> AddressEntities { get; set; }
    DbSet<QualificationReferenceEntity> QualificationReferenceEntities { get; set; }
    DbSet<QualificationEntity> QualificationEntities { get; set; }
    DbSet<CandidatePreferencesEntity> CandidatePreferencesEntities { get; set; }
    DbSet<PreferenceEntity> PreferenceEntities { get; set; }
    DbSet<SavedVacancyEntity> SavedVacancyEntities { get; set; }
    DbSet<EmploymentLocationEntity> EmploymentLocationEntities { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
public class CandidateAccountDataContext : DbContext, ICandidateAccountDataContext
{
    public DbSet<CandidateEntity> CandidateEntities { get; set; }
    public DbSet<ApplicationEntity> ApplicationEntities { get; set; }
    public DbSet<WorkHistoryEntity> WorkExperienceEntities { get; set; }
    public DbSet<TrainingCourseEntity> TrainingCourseEntities { get; set; }
    public DbSet<AdditionalQuestionEntity> AdditionalQuestionEntities { get; set; }
    public DbSet<AboutYouEntity> AboutYouEntities { get; set; }
    public DbSet<AddressEntity> AddressEntities { get; set; }
    public DbSet<QualificationReferenceEntity> QualificationReferenceEntities { get; set; }
    public DbSet<QualificationEntity> QualificationEntities { get; set; }
    public DbSet<CandidatePreferencesEntity> CandidatePreferencesEntities { get; set; }
    public DbSet<PreferenceEntity> PreferenceEntities { get; set; }
    public DbSet<SavedVacancyEntity> SavedVacancyEntities { get; set; }
    public DbSet<EmploymentLocationEntity> EmploymentLocationEntities { get; set; }

    private readonly CandidateAccountConfiguration? _configuration;
    public CandidateAccountDataContext()
    {
    }

    public CandidateAccountDataContext(DbContextOptions options) : base(options)
    {

    }
    public CandidateAccountDataContext(IOptions<CandidateAccountConfiguration> config, DbContextOptions options) : base(options)
    {
        _configuration = config.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();

        var connection = new SqlConnection
        {
            ConnectionString = _configuration!.SqlConnectionString,
        };

        optionsBuilder.UseSqlServer(connection, options =>
            options.EnableRetryOnFailure(
                5,
                TimeSpan.FromSeconds(20),
                null
            ));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CandidateEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new WorkHistoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TrainingCourseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AdditionalQuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AboutYouEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QualificationReferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QualificationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CandidatePreferencesEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PreferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SavedVacancyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EmploymentLocationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}