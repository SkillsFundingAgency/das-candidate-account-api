﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.VolunteeringOrWorkExperience;
public class WhenHandlingGetVolunteeringOrWorkExperienceItemQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Returned(
            GetVolunteeringOrWorkExperienceItemQuery request,
            WorkHistoryEntity entity,
            [Frozen] Mock<IWorkHistoryRepository> workExperienceRepository,
            GetVolunteeringOrWorkExperienceItemQueryHandler handler)
    {
        workExperienceRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId, request.Id, WorkHistoryType.WorkExperience, CancellationToken.None)).ReturnsAsync(entity);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(entity, options => options
            .Excluding(ctx => ctx.WorkHistoryType)
            .Excluding(ctx => ctx.ApplicationEntity)
            .Excluding(ctx => ctx.JobTitle)
        );
    }
}
