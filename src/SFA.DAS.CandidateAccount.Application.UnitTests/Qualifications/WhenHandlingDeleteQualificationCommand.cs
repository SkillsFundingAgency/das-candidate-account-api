using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualification;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Qualifications;

public class WhenHandlingDeleteQualificationCommand
{
    [Test, MoqAutoData]
    public async Task Then_The_Command_Is_Handled_And_Qualification_Deleted(
        DeleteQualificationCommand command,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        DeleteQualificationCommandHandler handler)
    {
        await handler.Handle(command, CancellationToken.None);
        
        qualificationRepository.Verify(x=>x.DeleteCandidateApplicationQualificationById(command.CandidateId, command.ApplicationId, command.Id), Times.Once);
    }
}