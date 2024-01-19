using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers;

public class WhenCallingPatchApplication
{
    [Test, MoqAutoData]
    public async Task Then_The_Command_Is_Sent_To_Mediator_And_Ok_Returned(
        Guid id,
        PatchApplicationCommandResponse response,
        JsonPatchDocument<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<PatchApplicationCommand>(
                c=> 
                c.Id.Equals(id)
                && c.Patch.Equals(request)
                ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        //Act
        var actual = await controller.PatchApplication(id, request) as OkObjectResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
        actual.Value.Should().BeEquivalentTo(response.Application);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_Null_Returned_From_Mediator_Then_NotFound_Is_Returned(
        Guid id,
        JsonPatchDocument<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<PatchApplicationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PatchApplicationCommandResponse
            {
                Application = null
            });
        
        //Act
        var actual = await controller.PatchApplication(id, request) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
        Guid id,
        JsonPatchDocument<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<PatchApplicationCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());
        
        //Act
        var actual = await controller.PatchApplication(id, request) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}