using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application
{
    [TestFixture]
    public class WhenCallingGetApplicationsCount
    {
        [Test, MoqAutoData]
        public async Task Then_The_Application_Count_Is_Returned(
            Guid candidateId,
            ApplicationStatus status,
            GetApplicationsCountQueryResult queryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] ApplicationController controller)
        {

            mediator.Setup(x => x.Send(It.Is<GetApplicationsCountQuery>(
                    c =>
                        c.CandidateId.Equals(candidateId) &&
                        c.Status.Equals(status)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var result = await controller.GetApplicationsCount(candidateId, status);

            result.Should().BeOfType<OkObjectResult>();
            var actionResult = result as OkObjectResult;

            actionResult.Value.Should().BeOfType<GetApplicationsCountQueryResult>();
            var value = actionResult.Value as GetApplicationsCountQueryResult;
            value.Should().BeEquivalentTo(queryResult);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Exception_Returned_From_Mediator_Then_InternalServerError_Is_Returned(
            Guid candidateId,
            ApplicationStatus status,
            GetApplicationsCountQueryResult queryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] ApplicationController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetApplicationsCountQuery>(
                    c =>
                        c.CandidateId.Equals(candidateId) &&
                        c.Status.Equals(status)
                ), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            //Act
            var actual = await controller.GetApplicationsCount(candidateId, status) as StatusCodeResult;

            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
