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
        TrainingCourseRepository repository)
        {
            //Arrange
            context.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(new List<TrainingCourseEntity>()
            {
                trainingCourseEntity
            });

            //Act
            await repository.Delete(trainingCourseEntity.ApplicationId, trainingCourseEntity.Id, trainingCourseEntity.ApplicationEntity.CandidateId);

            //Assert
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task When_TrainingCourse_NotFound_Then_Delete_NotCalled(
            TrainingCourseEntity trainingCourseEntity,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            TrainingCourseRepository repository)
        {
            //Arrange
            context.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(new List<TrainingCourseEntity>
            {
                Capacity = 0
            });

            //Act
            await repository.Delete(trainingCourseEntity.ApplicationId, trainingCourseEntity.Id, trainingCourseEntity.ApplicationEntity.CandidateId);

            //Assert
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
