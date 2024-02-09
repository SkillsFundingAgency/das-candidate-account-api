using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience;

[TestFixture]
public class WhenHandlingUpdateWorkHistoryCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Updated(
        UpdateWorkHistoryCommand request,
        [Frozen] Mock<IWorkHistoryRepository> repository,
        UpdateWorkHistoryCommandHandler handler)
    {
        repository.Setup(x => x.Update(It.Is<WorkHistoryEntity>(c =>
            c.Id == request.Id &&
            c.Employer == request.EmployerName &&
            c.WorkHistoryType == (byte)request.WorkHistoryType &&
            c.JobTitle == request.JobTitle &&
            c.Description == request.JobDescription &&
            c.StartDate == request.StartDate &&
            c.EndDate == request.EndDate
            ))).Returns(() => Task.CompletedTask);

        await handler.Handle(request, CancellationToken.None);

        repository.Verify(x => x.Update(It.IsAny<WorkHistoryEntity>()), Times.Once);
    }
}