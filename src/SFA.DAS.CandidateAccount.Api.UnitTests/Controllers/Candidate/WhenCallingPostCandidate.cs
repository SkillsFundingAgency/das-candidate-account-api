using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

public class WhenCallingPostCandidate
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid id,
        PostCandidateRequest postCandidateRequest,
        CreateCandidateCommandResponse createCandidateCommandResponse,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<CreateCandidateCommand>(c => 
                c.Email.Equals(postCandidateRequest.Email)
                && c.GovUkIdentifier.Equals(id.ToString())
                && c.FirstName.Equals(postCandidateRequest.FirstName)
                && c.LastName.Equals(postCandidateRequest.LastName)
                && c.DateOfBirth.Equals(postCandidateRequest.DateOfBirth)
            ), CancellationToken.None))
            .ReturnsAsync(createCandidateCommandResponse);
        
        //Act
        var actual = await controller.PostCandidate(id, postCandidateRequest);
        
        //Assert
        var result = actual as CreatedResult;
        var actualResult = result.Value as Domain.Candidate.Candidate;
        actualResult.Should().BeEquivalentTo(createCandidateCommandResponse.Candidate);
    }
    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        Guid id,
        PostCandidateRequest postCandidateRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<UpsertApplicationCommand>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));
        
        //Act
        var actual = await controller.PostCandidate(id, postCandidateRequest);
        
        //Assert
        var result = actual as StatusCodeResult;
        result?.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}