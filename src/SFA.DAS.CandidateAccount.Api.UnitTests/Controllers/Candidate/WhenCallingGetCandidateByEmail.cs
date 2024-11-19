using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

public class WhenCallingGetCandidateByEmail
{
    [Test, MoqAutoData]
    public async Task Then_Mediator_Query_Is_Called_And_Candidate_Returned(
        string email,
        GetCandidateByEmailQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<GetCandidateByEmailQuery>(c => c.Email.Equals(email)), CancellationToken.None))
            .ReturnsAsync(queryResult);
        
        //Act
        var actual = await controller.GetCandidateByEmail(email) as OkObjectResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
        actual.Value.Should().BeEquivalentTo(queryResult.Candidate);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Null_From_Mediator_Response_Not_Found_Returned(
        string email,
        GetCandidateByEmailQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        queryResult.Candidate = null;
        mediator.Setup(x => x.Send(It.Is<GetCandidateByEmailQuery>(c => c.Email.Equals(email)), CancellationToken.None))
            .ReturnsAsync(queryResult);

        //Act
        var actual = await controller.GetCandidateByEmail(email) as NotFoundResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Exception_Internal_Server_Error_Returned(
        string email,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<GetCandidateByEmailQuery>(c => c.Email.Equals(email)), CancellationToken.None))
            .ThrowsAsync(new Exception());
            
        //Act
        var actual = await controller.GetCandidateByEmail(email) as StatusCodeResult;
        
        //Assert
        Assert.That(actual, Is.Not.Null);
        actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}