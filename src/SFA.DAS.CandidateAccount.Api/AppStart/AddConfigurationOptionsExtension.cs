using Microsoft.Extensions.Options;
using SFA.DAS.CandidateAccount.Domain.Configuration;

namespace SFA.DAS.CandidateAccount.Api.AppStart;

public static class AddConfigurationOptionsExtension
{
    public static void AddConfigurationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CandidateAccountConfiguration>(configuration.GetSection(nameof(CandidateAccountConfiguration)));
        services.AddSingleton(cfg => cfg.GetService<IOptions<CandidateAccountConfiguration>>().Value);
    }
}