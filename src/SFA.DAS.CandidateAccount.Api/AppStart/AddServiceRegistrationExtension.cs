using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(CreateApplicationRequest).Assembly));
    }
}