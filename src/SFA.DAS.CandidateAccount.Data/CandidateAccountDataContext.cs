using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.AboutYou;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidates;
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
    DbSet<QualificationReferenceEntity> QualificationReferenceEntities { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken  = default (CancellationToken));
}
public class CandidateAccountDataContext : DbContext, ICandidateAccountDataContext
{
    private const string AzureResource = "https://database.windows.net/";
    private readonly ChainedTokenCredential _azureServiceTokenProvider;
    private readonly EnvironmentConfiguration _environmentConfiguration;
    public DbSet<CandidateEntity> CandidateEntities { get; set; }
    public DbSet<ApplicationEntity> ApplicationEntities { get; set; }
    public DbSet<WorkHistoryEntity> WorkExperienceEntities { get; set; }
    public DbSet<TrainingCourseEntity> TrainingCourseEntities { get; set; }
    public DbSet<AdditionalQuestionEntity> AdditionalQuestionEntities { get; set; }
    public DbSet<AboutYouEntity> AboutYouEntities { get; set; }
    public DbSet<QualificationReferenceEntity> QualificationReferenceEntities { get; set; }

    private readonly CandidateAccountConfiguration? _configuration;
    public CandidateAccountDataContext()
    {
    }

    public CandidateAccountDataContext(DbContextOptions options) : base(options)
    {
            
    }
    public CandidateAccountDataContext(IOptions<CandidateAccountConfiguration> config, DbContextOptions options, ChainedTokenCredential azureServiceTokenProvider, EnvironmentConfiguration environmentConfiguration) :base(options)
    {
        _azureServiceTokenProvider = azureServiceTokenProvider;
        _environmentConfiguration = environmentConfiguration;
        _configuration = config.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
            
        if (_configuration == null 
            || _environmentConfiguration.EnvironmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
            || _environmentConfiguration.EnvironmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }
            
        var connection = new SqlConnection
        {
            ConnectionString = _configuration.ConnectionString,
            AccessToken = _azureServiceTokenProvider.GetTokenAsync(new TokenRequestContext(scopes: new string[] { AzureResource })).Result.Token,
        };
            
        optionsBuilder.UseSqlServer(connection,options=>
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

        base.OnModelCreating(modelBuilder);
    }
}