using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualificationsByReferenceId;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Qualifications;

public class WhenHandlingDeleteQualificationsByReferenceIdCommand
{
    [Test, MoqAutoData]
    public async Task Then_The_Command_Is_Handled_And_Qualification_Deleted(
        DeleteQualificationsByReferenceIdCommand command,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        DeleteQualificationsByReferenceIdCommandHandler handler)
    {
        await handler.Handle(command, CancellationToken.None);
        
        qualificationRepository.Verify(x=>x.DeleteCandidateApplicationQualificationsByReferenceId(command.CandidateId, command.ApplicationId, command.QualificationReferenceId), Times.Once);
    }
}