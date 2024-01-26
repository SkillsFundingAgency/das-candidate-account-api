using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.WorkExperience;

[TestFixture]
public class WhenGettingWorkExperiences
{
    [Test, RecursiveMoqAutoData]
    public async Task ThenTheWorkExperiencesIsFetched(
        Guid applicationId,
        WorkHistoryEntity workHistories,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        WorkHistoryRepository repository)
    {
        //Arrange
        workHistories.ApplicationId = applicationId;
        context.Setup(x => x.WorkExperienceEntities).ReturnsDbSet(new List<WorkHistoryEntity>
        {
            workHistories
        });

        //Act
        var actual = await repository.Get(applicationId, CancellationToken.None);

        //Assert
        actual.Should().NotBeNull();
        actual.FirstOrDefault().Should().BeEquivalentTo(workHistories);
    }
}