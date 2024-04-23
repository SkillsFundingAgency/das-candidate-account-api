using AutoFixture.NUnit3;
using FluentAssertions;
using SFA.DAS.CandidateAccount.Api.ApiResponses;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.ApiResponses;

public class WhenMappingGetApplicationApiResponseFromMeditorResponse
{
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped(Domain.Application.Application application)
    {
        var actual = (GetApplicationApiResponse)application;

        actual.Should().BeEquivalentTo(application);
    }
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped_For_Detail(Domain.Application.ApplicationDetail application)
    {
        var actual = (GetApplicationApiResponse)application;

        actual.Should().BeEquivalentTo(application, options => options
            .Excluding(c=>c.AboutYou)
            .Excluding(c=>c.TrainingCourses)
        );
    }
}