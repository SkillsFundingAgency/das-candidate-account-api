using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.WorkExperience
{
    [TestFixture]
    public class WhenInsertingWorkExperience
    {
        [Test, RecursiveMoqAutoData]
        public async Task ThenTheCandidateIsInserted(
            WorkExperienceEntity workExperience,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            WorkExperienceRepository repository)
        {
            //Arrange
            context.Setup(x => x.WorkExperienceEntities).ReturnsDbSet(new List<WorkExperienceEntity>());

            //Act
            await repository.Insert(workExperience);

            //Assert
            context.Verify(x => x.WorkExperienceEntities.AddAsync(workExperience, CancellationToken.None), Times.Once);
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
