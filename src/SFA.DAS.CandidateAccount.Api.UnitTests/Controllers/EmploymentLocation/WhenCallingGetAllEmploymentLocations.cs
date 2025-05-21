using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.EmploymentLocation
{
    [TestFixture]
    public class WhenCallingGetAllEmploymentLocations
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid applicationId,
            Guid candidateId,
            GetEmploymentLocationsQueryResult response,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] EmploymentLocationController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetEmploymentLocationsQuery>(
                    c =>
                        c.ApplicationId.Equals(applicationId) &&
                        c.CandidateId.Equals(candidateId)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act
            var actual = await controller.GetAll(candidateId, applicationId) as OkObjectResult;

            //Assert
            using var scope = new AssertionScope();
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeEquivalentTo(response);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Exception_Internal_Server_Error_Returned(
            Guid applicationId,
            Guid candidateId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] EmploymentLocationController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetEmploymentLocationsQuery>(
                    c =>
                        c.ApplicationId.Equals(applicationId) &&
                        c.CandidateId.Equals(candidateId)
                ), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            //Act
            var actual = await controller.GetAll(candidateId, applicationId) as StatusCodeResult;

            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
