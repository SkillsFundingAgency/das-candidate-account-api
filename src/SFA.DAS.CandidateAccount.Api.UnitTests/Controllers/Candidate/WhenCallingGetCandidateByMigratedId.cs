using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedId;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

public class WhenCallingGetCandidateByMigratedId
{
    [Test, MoqAutoData]
    public async Task Then_Mediator_Query_Is_Called_And_Candidate_Returned(
        Guid id,
        GetCandidateByMigratedIdQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<GetCandidateByMigratedIdQuery>(c => c.MigratedCandidateId.Equals(id)), CancellationToken.None))
            .ReturnsAsync(queryResult);
        
        //Act
        var actual = await controller.GetCandidateByMigratedId(id) as OkObjectResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
        actual.Value.Should().BeEquivalentTo(queryResult.Candidate);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Null_From_Mediator_Response_Not_Found_Returned(
        Guid id,
        GetCandidateByMigratedIdQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        queryResult.Candidate = null;
        mediator.Setup(x => x.Send(It.Is<GetCandidateByMigratedIdQuery>(c => c.MigratedCandidateId.Equals(id)), CancellationToken.None))
            .ReturnsAsync(queryResult);

        //Act
        var actual = await controller.GetCandidateByMigratedId(id) as NotFoundResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Exception_Internal_Server_Error_Returned(
        Guid id,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<GetCandidateByMigratedIdQuery>(c => c.MigratedCandidateId.Equals(id)), CancellationToken.None))
            .ThrowsAsync(new Exception());
            
        //Act
        var actual = await controller.GetCandidateByMigratedId(id) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}