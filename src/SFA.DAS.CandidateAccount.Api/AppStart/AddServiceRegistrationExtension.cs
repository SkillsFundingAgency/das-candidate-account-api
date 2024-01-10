using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IApplicationTemplateRepository, ApplicationTemplateRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        //services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(GetSearchApprenticeshipsIndexQuery).Assembly));
    }
}