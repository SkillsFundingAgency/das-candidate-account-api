using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Data;
using SFA.DAS.CandidateAccount.Domain.Configuration;

namespace SFA.DAS.CandidateAccount.Api.AppStart;

public static class DatabaseExtensions
{
    public static void AddDatabaseRegistration(this IServiceCollection services, CandidateAccountConfiguration config, string? environmentName)
    {
        services.AddHttpContextAccessor();
        if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<CandidateAccountDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.CandidateAccount"), ServiceLifetime.Transient);
        }
        else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<CandidateAccountDataContext>(options=>options.UseSqlServer(config.ConnectionString),ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<CandidateAccountDataContext>(ServiceLifetime.Transient);    
        }
            
        services.AddSingleton(new EnvironmentConfiguration(environmentName));

        services.AddScoped<ICandidateAccountDataContext, CandidateAccountDataContext>(provider => provider.GetService<CandidateAccountDataContext>()!);
        services.AddScoped(provider => new Lazy<CandidateAccountDataContext>(provider.GetService<CandidateAccountDataContext>()!));
        services.AddSingleton(new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new AzureCliCredential(),
            new VisualStudioCodeCredential(),
            new VisualStudioCredential())
        );
    }
}