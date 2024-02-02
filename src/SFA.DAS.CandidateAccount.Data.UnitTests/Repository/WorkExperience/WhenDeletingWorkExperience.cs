using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.WorkExperience
{
    public class WhenDeletingWorkExperience
    {
        [Test, RecursiveMoqAutoData]
        public async Task ThenTheJobIsDeleted(
            WorkHistoryEntity workHistory,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            WorkHistoryRepository repository)
        {
            //Arrange
            context.Setup(x => x.WorkExperienceEntities).ReturnsDbSet(new List<WorkHistoryEntity>());

            //Act
            await repository.Delete(workHistory.ApplicationId, workHistory.Id, workHistory.ApplicationEntity.Id);

            //Assert
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

    }

}