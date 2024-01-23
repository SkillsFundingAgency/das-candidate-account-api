using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateJob;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience;

public class WhenHandlingCreateWorkHistoryCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateWorkHistoryCommand request,
        WorkExperienceEntity entity,
        [Frozen] Mock<IWorkExperienceRepository> workExperienceRepository,
        CreateWorkHistoryCommandHandler handler)
    {
        workExperienceRepository.Setup(x => x.Insert(It.Is<WorkExperienceEntity>(c =>
            c.ApplicationId    == request.ApplicationId &&
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