using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateSkillsAndStrengths;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.AboutYou;
public class WhenCallingPutAboutYou
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid id,
        Guid candidateId,
        Guid applicationId,
        PutAboutYouItemRequest upsertAboutYouRequest,
        UpsertAboutYouCommandResult upsertAboutYouCommandResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] AboutYouController controller)
    {
        upsertAboutYouCommandResult.IsCreated = true;
        mediator.Setup(x => x.Send(It.Is<UpsertAboutYouCommand>(c =>
                c.AboutYou.Id == id
                && c.AboutYou.Strengths.Equals(upsertAboutYouRequest.SkillsAndStrengths)
            ), CancellationToken.None))
            .ReturnsAsync(upsertAboutYouCommandResult);

        var actual = await controller.PutAboutYouItem(candidateId, applicationId, id, upsertAboutYouRequest);
        var result = actual as CreatedResult;
        var actualResult = result.Value as Domain.Candidate.AboutYou;

        actual.Should().BeOfType<CreatedResult>();
        actualResult.Should().BeEquivalentTo(upsertAboutYouCommandResult.AboutYou);
    }

    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_NotCreated_Then_Ok_Result_Returned(
        Guid id,
        Guid candidateId,
        Guid applicationId,
        PutAboutYouItemRequest upsertAboutYouRequest,
        UpsertAboutYouCommandResult upsertAboutYouCommandResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] AboutYouController controller)
    {
        upsertAboutYouCommandResult.IsCreated = false;
        mediator.Setup(x => x.Send(It.Is<UpsertAboutYouCommand>(c =>
                c.AboutYou.Id == id
                && c.AboutYou.Strengths.Equals(upsertAboutYouRequest.SkillsAndStrengths)
            ), CancellationToken.None))
            .ReturnsAsync(upsertAboutYouCommandResult);

        var actual = await controller.PutAboutYouItem(candidateId, applicationId, id, upsertAboutYouRequest);
        var result = actual as OkObjectResult;
        var actualResult = result.Value as Domain.Candidate.AboutYou;

        actual.Should().BeOfType<OkObjectResult>();
        actualResult.Should().BeEquivalentTo(upsertAboutYouCommandResult.AboutYou);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        PutAboutYouItemRequest aboutYouController,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] AboutYouController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<UpsertAboutYouCommand>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        var actual = await controller.PutAboutYouItem(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), aboutYouController);

        actual.As<StatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
