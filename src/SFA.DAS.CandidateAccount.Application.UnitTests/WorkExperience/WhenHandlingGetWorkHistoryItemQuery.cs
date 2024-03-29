﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetWorkHistoryItem;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience
{
    [TestFixture]
    public class WhenHandlingGetWorkHistoryItemQuery
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Request_Is_Handled_And_Entities_Returned(
            GetWorkHistoryItemQuery request,
            WorkHistoryEntity entities,
            [Frozen] Mock<IWorkHistoryRepository> workExperienceRepository,
            GetWorkHistoryItemQueryHandler handler)
        {
            workExperienceRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId, request.Id, request.WorkHistoryType, CancellationToken.None)).ReturnsAsync(entities);

            var actual = await handler.Handle(request, CancellationToken.None);

            actual.Should().BeEquivalentTo(entities, options => options
                .Excluding(ctx => ctx.WorkHistoryType)
                .Excluding(ctx => ctx.ApplicationEntity)
            );
        }
    }
}
