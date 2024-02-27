using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;


namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.TrainingCourse
{
    public class WhenDeletingTrainingCourse
    {
        [Test, RecursiveMoqAutoData]
        public async Task ThenTheJobIsDeleted(
        TrainingCourseEntity trainingCourseEntity,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        TrainingCourseRespository repository
        )
        {
            //Arrange
            context.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(new List<TrainingCourseEntity>());

            //Act
            await repository.Delete(trainingCourseEntity.ApplicationId, trainingCourseEntity.Id, trainingCourseEntity.ApplicationEntity.Id);

            //Assert
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
