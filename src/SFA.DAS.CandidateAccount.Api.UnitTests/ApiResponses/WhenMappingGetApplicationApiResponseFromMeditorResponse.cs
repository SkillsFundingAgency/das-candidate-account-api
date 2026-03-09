using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.ApiResponses;

public class WhenMappingGetApplicationApiResponseFromMediatorResponse
{
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped(Domain.Application.Application application)
    {
        var actual = (GetApplicationApiResponse)application;

        actual.Should().BeEquivalentTo(application);
    }
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped_For_Detail(ApplicationDetail application)
    {
        var actual = (GetApplicationApiResponse)application;

        actual.Should().BeEquivalentTo(application, options => options
            .Excluding(c=>c.TrainingCourses)
        );
    }
}