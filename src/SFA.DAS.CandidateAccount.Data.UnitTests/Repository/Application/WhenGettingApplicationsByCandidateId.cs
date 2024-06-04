using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenGettingApplicationsByCandidateId
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Applications_Are_Returned_By_CandidateId(
        Guid candidateId,
        List<ApplicationEntity> applications,
        List<ApplicationEntity> otherApplications,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        foreach (var application in applications)
        {
            application.CandidateId = candidateId;
        }

        var allApplications = new List<ApplicationEntity>();
        allApplications.AddRange(applications);
        allApplications.AddRange(otherApplications);

        context.Setup(x => x.ApplicationEntities)
            .ReturnsDbSet(allApplications);

        var actual = await repository.GetByCandidateId(candidateId, null);

        actual.Should().BeEquivalentTo(applications);
    }
}