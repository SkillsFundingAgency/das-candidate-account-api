using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.EmploymentLocation
{
    [TestFixture]
    public class WhenCallingPutEmploymentLocations
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid id,
            Guid applicationId,
            Guid candidateId,
            PutEmploymentLocationApiRequest request,
            UpsertEmploymentLocationCommandResponse response,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] EmploymentLocationController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<UpsertEmploymentLocationCommand>(
                    c =>
                        c.CandidateId.Equals(candidateId) &&
                        c.EmploymentLocation.ApplicationId.Equals(applicationId) &&
                        c.EmploymentLocation.Id.Equals(id) &&
                        c.EmploymentLocation.EmployerLocationOption.Equals(request.EmployerLocationOption) &&
                        c.EmploymentLocation.EmploymentLocationInformation.Equals(request.EmploymentLocationInformation) &&
                        c.EmploymentLocation.Addresses.Equals(request.Addresses)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            //Act
            var actual = await controller.PutEmploymentLocation(candidateId, applicationId, id, request) as CreatedResult;

            //Assert
            using var scope = new AssertionScope();
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.Created);

            var actualResult = actual.Value as Domain.Application.EmploymentLocation;

            actual.Should().BeOfType<CreatedResult>();
            actualResult.Should().BeEquivalentTo(response.EmploymentLocation);
        }


        [Test, MoqAutoData]
        public async Task Then_If_MediatorCall_Returns_NotCreated_Then_Ok_Result_Returned(
            Guid id,
            Guid applicationId,
            Guid candidateId,
            PutEmploymentLocationApiRequest request,
            UpsertEmploymentLocationCommandResponse response,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] EmploymentLocationController controller)
        {
            response.IsCreated = false;
            mediator.Setup(x => x.Send(It.Is<UpsertEmploymentLocationCommand>(
                    c =>
                        c.CandidateId.Equals(candidateId) &&
                        c.EmploymentLocation.ApplicationId.Equals(applicationId) &&
                        c.EmploymentLocation.Id.Equals(id) &&
                        c.EmploymentLocation.EmployerLocationOption.Equals(request.EmployerLocationOption) &&
                        c.EmploymentLocation.EmploymentLocationInformation.Equals(request.EmploymentLocationInformation) &&
                        c.EmploymentLocation.Addresses.Equals(request.Addresses)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var actual = await controller.PutEmploymentLocation(candidateId, applicationId, id, request);
            var result = actual as OkObjectResult;
            var actualResult = result.Value as Domain.Application.EmploymentLocation;

            actual.Should().BeOfType<OkObjectResult>();
            actualResult.Should().BeEquivalentTo(response.EmploymentLocation);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
            PutEmploymentLocationApiRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] EmploymentLocationController controller)
        {
            mediator.Setup(x => x.Send(It.IsAny<UpsertEmploymentLocationCommand>(),
                CancellationToken.None)).ThrowsAsync(new Exception("Error"));

            var actual = await controller.PutEmploymentLocation(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), request);

            actual.As<StatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
