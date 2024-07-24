﻿using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancies;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.SavedVacancies
{
    [TestFixture]
    public class WhenCallingGetByCandidateId
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid candidateId,
            GetSavedVacanciesByCandidateIdQueryResult byCandidateIdQueryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] SavedVacancyController controller)
        {
            mediator.Setup(x => x.Send(It.Is<GetSavedVacanciesByCandidateIdQuery>(query => query.CandidateId == candidateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(byCandidateIdQueryResult);

            var result = await controller.GetByCandidateId(candidateId) as OkObjectResult;
            result.Value.Should().BeEquivalentTo(byCandidateIdQueryResult);
        }
    }
}
