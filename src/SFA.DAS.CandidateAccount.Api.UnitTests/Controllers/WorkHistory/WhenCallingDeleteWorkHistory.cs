using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.WorkHistory
{
    public class WhenCallingDeleteWorkHistory
    {
        [Test, MoqAutoData]
        public async Task Then_If_MediatorCall_Returns_Ok_Then_Ok_Result_Returned(
            Guid candidateId,
            Guid applicationId,
            Guid workHistoryId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] WorkHistoryController controller)
        {
            var actual = await controller.DeleteWorkHistory(candidateId, applicationId, workHistoryId) as OkObjectResult;

            actual.Should().BeOfType<OkObjectResult>();
            mediator.Verify(x => x.Send(It.Is<DeleteJobCommand>(c =>
                    c.CandidateId.Equals(candidateId) &&
                    c.ApplicationId.Equals(applicationId) &&
                    c.JobId.Equals(workHistoryId)
                ), CancellationToken.None));

        }

        [Test, MoqAutoData]
        public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
            Guid candidateId,
            Guid applicationId,
            Guid workHistoryId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] WorkHistoryController controller)
        {
            // Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteJobCommand>(), CancellationToken.None))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var actual = await controller.DeleteWorkHistory(candidateId, applicationId, workHistoryId);

            // Assert
            actual.Should().BeOfType<StatusCodeResult>();
            var result = actual as StatusCodeResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }



    }
}
