using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateWorkHistory;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience;

[TestFixture]
public class WhenHandlingCreateWorkHistoryCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateWorkHistoryCommand request,
        WorkHistoryEntity entity,
        [Frozen] Mock<IWorkHistoryRepository> workExperienceRepository,
        CreateWorkHistoryCommandHandler handler)
    {
        workExperienceRepository.Setup(x => x.Insert(It.Is<WorkHistoryEntity>(c =>
            c.ApplicationId  == request.ApplicationId &&
            c.Employer == request.EmployerName &&
            c.JobTitle == request.JobTitle &&
            c.Description == request.JobDescription &&
            c.StartDate == request.StartDate &&
            c.EndDate == request.EndDate
            ))).ReturnsAsync(entity);
        
        var actual = await handler.Handle(request, CancellationToken.None);

        actual.WorkHistory.Should().BeEquivalentTo(entity, options => options.Excluding(c=>c.Id));
    }
}