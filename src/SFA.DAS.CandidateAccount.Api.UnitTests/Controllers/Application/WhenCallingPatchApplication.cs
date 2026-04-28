using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson.Operations;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application;

public class WhenCallingPatchApplication
{
    [Test, MoqAutoData]
    public async Task Then_The_Command_Is_Sent_To_Mediator_And_Ok_Returned(
        Guid id,
        Guid candidateId,
        PatchApplicationCommandResponse response,
        Operation<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<PatchApplicationCommand>(
                c=> 
                c.Id.Equals(id)
                && c.CandidateId.Equals(candidateId)
                && c.Patch.Operations.First().path.Equals(request.path)
                ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        //Act
        var actual = await controller.PatchApplication(id, candidateId, new JsonPatchDocument<PatchApplication>(
            [request], new JsonSerializerOptions())) as OkObjectResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
        actual.Value.Should().BeEquivalentTo(response.Application);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_Null_Returned_From_Mediator_Then_NotFound_Is_Returned(
        Guid id,
        Guid candidateId,
        Operation<PatchApplication> request,
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
        var actual = await controller.PatchApplication(id, candidateId, new JsonPatchDocument<PatchApplication>(
            [request], new JsonSerializerOptions())) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
        Guid id,
        Guid candidateId,
        Operation<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<PatchApplicationCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());
        
        //Act
        var actual = await controller.PatchApplication(id, candidateId, new JsonPatchDocument<PatchApplication>(
            [request], new JsonSerializerOptions())) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_ValidationError_Then_BadRequest_Response_Returned(
        Guid id,
        Guid candidateId,
        Operation<PatchApplication> request,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<PatchApplicationCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException("Error"));
        
        //Act
        var actual = await controller.PatchApplication(id, candidateId, new JsonPatchDocument<PatchApplication>(
            [request], new JsonSerializerOptions())) as StatusCodeResult;
        
        //Assert
        var result = actual as BadRequestResult;
        result?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
    }
}