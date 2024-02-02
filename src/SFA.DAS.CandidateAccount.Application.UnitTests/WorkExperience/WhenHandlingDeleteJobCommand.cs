using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience
{
    public class WhenHandlingDeleteJobCommand
    {
        [Test, MoqAutoData]
        public async Task Handle_Should_Delete_WorkHistory_From_Repository(
            Guid candidateId,
            Guid applicationId,
            Guid workHistoryId,
            [Frozen] Mock<IWorkHistoryRepository> mockRepository,
            DeleteJobCommandHandler handler)
        {
            // Arrange
            var command = new DeleteJobCommand
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                JobId = workHistoryId
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepository.Verify(
                x => x.Delete(It.Is<Guid>(a => a.Equals(applicationId)),
                              It.Is<Guid>(w => w.Equals(workHistoryId)),
                              It.Is<Guid>(c => c.Equals(candidateId))),
                Times.Once);
        }
    }
}
