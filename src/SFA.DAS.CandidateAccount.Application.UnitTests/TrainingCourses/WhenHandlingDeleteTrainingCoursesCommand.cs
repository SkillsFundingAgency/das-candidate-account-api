using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualificationsByReferenceId;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteTrainingCourse;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;


namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses
{
    public class WhenHandlingDeleteTrainingCoursesCommand
    {
        [Test, MoqAutoData]
        public async Task Handle_Should_Delete_WorkHistory_From_Repository(
         Guid candidateId,
         Guid applicationId,
         Guid id,
         [Frozen] Mock<ITrainingCourseRepository> mockRepository,
         [Frozen] Mock<IApplicationRepository> applicationRepository,
         DeleteTrainingCourseCommandHandler handler)
        {
            // Arrange
            var command = new DeleteTrainingCourseCommand
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                Id = id
            };

            var application = new ApplicationEntity { CandidateId = command.CandidateId, TrainingCoursesStatus = (short)SectionStatus.NotStarted };
            applicationRepository.Setup(x => x.GetById(command.ApplicationId, false))
                .ReturnsAsync(application);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepository.Verify(
                x => x.Delete(It.Is<Guid>(a => a.Equals(applicationId)),
                              It.Is<Guid>(w => w.Equals(id)),
                              It.Is<Guid>(c => c.Equals(candidateId))),
                Times.Once);
        }


        [Test, MoqAutoData]
        public async Task If_SectionStatus_Is_PreviousAnswer_Then_SectionStatus_Set_To_InProgress(
            DeleteTrainingCourseCommand command,
            [Frozen] Mock<IApplicationRepository> applicationRepository,
            DeleteTrainingCourseCommandHandler handler)
        {
            var application = new ApplicationEntity { CandidateId = command.CandidateId, TrainingCoursesStatus = (short)SectionStatus.PreviousAnswer };
            applicationRepository.Setup(x => x.GetById(command.ApplicationId, false))
                .ReturnsAsync(application);

            applicationRepository.Setup(x => x.Update(It.IsAny<ApplicationEntity>())).ReturnsAsync(application);

            await handler.Handle(command, CancellationToken.None);

            applicationRepository.Verify(x => x.Update(It.Is<ApplicationEntity>(a => a.TrainingCoursesStatus == (short)SectionStatus.InProgress)));
        }
    }
}
