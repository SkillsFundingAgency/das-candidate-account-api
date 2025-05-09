using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application
{
    [TestFixture]
    public class WhenGettingCountByCandidateId
    {
        [Test]
        [RecursiveMoqInlineAutoData(ApplicationStatus.Successful)]
        [RecursiveMoqInlineAutoData(ApplicationStatus.UnSuccessful)]
        public async Task Then_The_Applications_Count_Is_Returned_By_CandidateId(
            ApplicationStatus status,
            Guid candidateId,
            List<ApplicationEntity> applications,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            ApplicationRepository repository)
        {
            foreach (var application in applications)
            {
                application.CandidateId = candidateId;
                application.Status = (short)status;
            }

            var allApplications = new List<ApplicationEntity>();
            allApplications.AddRange(applications);

            context.Setup(x => x.ApplicationEntities)
                .ReturnsDbSet(allApplications);

            var actual = await repository.GetCountByStatus(candidateId, (short) status, CancellationToken.None);

            actual.Should().BeEquivalentTo(applications);
        }
    }
}
