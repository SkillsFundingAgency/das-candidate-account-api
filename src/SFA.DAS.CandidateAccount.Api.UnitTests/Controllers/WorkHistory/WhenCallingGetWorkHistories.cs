using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.WorkHistory
{
    public class WhenCallingGetWorkHistories
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Ok_Returned(
        Guid applicationId,
        Guid candidateId,
        GetApplicationWorkHistoriesQueryResult response,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] WorkHistoryController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetApplicationWorkHistoriesQuery>(
                    c =>
                        c.ApplicationId.Equals(applicationId) &&
                        c.CandidateId.Equals(candidateId)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act
            var actual = await controller.GetWorkHistories(candidateId, applicationId) as OkObjectResult;

            //Assert
            using var scope = new AssertionScope();
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeEquivalentTo(response.WorkHistories);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Null_Returned_From_Mediator_Then_Null_Is_Returned(
            Guid applicationId,
            Guid candidateId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] WorkHistoryController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetApplicationWorkHistoriesQuery>(
                    c =>
                        c.ApplicationId.Equals(applicationId) &&
                        c.CandidateId.Equals(candidateId)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetApplicationWorkHistoriesQueryResult
                {
                    WorkHistories = null!
                });

            //Act
            var actual = await controller.GetWorkHistories(candidateId, applicationId) as OkObjectResult;

            //Assert
            using var scope = new AssertionScope();
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeNull();
        }

        [Test, MoqAutoData]
        public async Task Then_If_Exception_Returned_From_Mediator_Then_InternalServerError_Is_Returned(
            Guid applicationId,
            Guid candidateId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] WorkHistoryController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetApplicationWorkHistoriesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            //Act
            var actual = await controller.GetWorkHistories(applicationId, candidateId) as StatusCodeResult;

            //Assert
            Assert.That(actual, Is.Not.Null);
            actual?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
