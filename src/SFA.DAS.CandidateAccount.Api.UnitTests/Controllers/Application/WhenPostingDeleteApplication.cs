using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteApplication;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application;

public class WhenPostingDeleteApplication
{
    [Test, MoqAutoData]
    public async Task Returns_NotFound_Correctly(
        Guid candidateId,
        Guid applicationId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController sut)
    {
        // arrange
        mediator
            .Setup(x => x.Send(It.Is<DeleteApplicationCommand>(cmd => cmd.ApplicationId == applicationId && cmd.CandidateId == candidateId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(DeleteApplicationCommandResult.None);

        // act
        var result = await sut.DeleteApplication(applicationId, candidateId, CancellationToken.None) as NotFoundResult;

        // assert
        result.Should().NotBeNull();
    }
    
    [Test, MoqAutoData]
    public async Task Returns_NoContent_Correctly(
        Guid candidateId,
        Guid applicationId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController sut)
    {
        // arrange
        mediator
            .Setup(x => x.Send(It.Is<DeleteApplicationCommand>(cmd => cmd.ApplicationId == applicationId && cmd.CandidateId == candidateId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteApplicationCommandResult(applicationId));

        // act
        var result = await sut.DeleteApplication(applicationId, candidateId, CancellationToken.None) as NoContentResult;

        // assert
        result.Should().NotBeNull();
    }
}