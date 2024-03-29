using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.AboutYou;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IWorkHistoryRepository, WorkHistoryRepository>();
        services.AddScoped<ITrainingCourseRespository, TrainingCourseRepository>();
        services.AddScoped<IAdditionalQuestionRepository, AdditionalQuestionRepository>();
        services.AddScoped<IAboutYouRespository, AboutYouRepository>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(UpsertApplicationCommand).Assembly));
    }
}