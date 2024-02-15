using AutoFixture.NUnit3;
using FluentAssertions.Formatting;
using Moq;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.TrainingCourse
{
    public class WhenDeletingTrainingCourse
    {
        [Test, RecursiveMoqAutoData]
        public async Task ThenTheJobIsDeleted(
        TrainingCourseEntity trainingCourseEntity,
        [Frozen] Mock<ICandidateAccountDataContext> context,
        TrainingCourseRespository repository,
        Guid candidateId)
        {
            //Arrange
            context.Setup(x => x.TrainingCourseEntities).ReturnsDbSet(new List<TrainingCourseEntity>());

            //Act
            await repository.Delete(trainingCourseEntity.ApplicationId, trainingCourseEntity.Id, candidateId);

            //Assert
            context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
