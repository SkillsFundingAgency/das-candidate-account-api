using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetWorkHistoryItem;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.WorkHistory
{
    [TestFixture]
    public class WhenCallingGetWorkHistoryItem
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid applicationId,
            Guid candidateId,
            Guid id,
            WorkHistoryType workHistoryType,
            GetWorkHistoryItemQueryResult response,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] WorkHistoryController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetWorkHistoryItemQuery>(
                    c =>
                        c.ApplicationId.Equals(applicationId) &&
                        c.CandidateId.Equals(candidateId) &&
                        c.Id.Equals(id) &&
                        c.WorkHistoryType.Equals(workHistoryType)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act
            var actual = await controller.Get(candidateId, applicationId, id, workHistoryType) as OkObjectResult;

            //Assert
            using var scope = new AssertionScope();
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeEquivalentTo((GetWorkHistoryItemApiResponse)response);
        }
    }
}
