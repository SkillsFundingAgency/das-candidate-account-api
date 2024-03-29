﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience;


[TestFixture]
public class WhenHandlingGetApplicationWorkHistoriesQueryHandler
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entities_Returned(
        GetApplicationWorkHistoriesQuery request,
        List<WorkHistoryEntity> entities,
        [Frozen] Mock<IWorkHistoryRepository> workExperienceRepository,
        GetApplicationWorkHistoriesQueryHandler handler)
    {
        workExperienceRepository.Setup(x => x.GetAll(request.ApplicationId, request.CandidateId, request.WorkHistoryType, CancellationToken.None)).ReturnsAsync(entities);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.WorkHistories.Should().BeEquivalentTo(entities, options => options
            .Excluding(ctx => ctx.WorkHistoryType).Excluding(ctx => ctx.ApplicationEntity)
        );
    }
}