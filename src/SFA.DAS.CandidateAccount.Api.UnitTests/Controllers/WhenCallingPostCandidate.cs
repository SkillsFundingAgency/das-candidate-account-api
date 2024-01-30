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

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers;

public class WhenCallingPostCandidate
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid id,
        CandidateRequest candidateRequest,
        CreateCandidateResponse createCandidateResponse,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<CreateCandidateRequest>(c => 
                c.Email.Equals(candidateRequest.Email)
                && c.GovUkIdentifier.Equals(candidateRequest.GovUkIdentifier)
                && c.FirstName.Equals(candidateRequest.FirstName)
                && c.LastName.Equals(candidateRequest.LastName)
                && c.DateOfBirth.Equals(candidateRequest.DateOfBirth)
                && c.Id.Equals(id)
            ), CancellationToken.None))
            .ReturnsAsync(createCandidateResponse);
        
        //Act
        var actual = await controller.PostCandidate(id, candidateRequest);
        
        //Assert
        var result = actual as CreatedResult;
        var actualResult = result.Value as Domain.Candidate.Candidate;
        actualResult.Should().BeEquivalentTo(createCandidateResponse.Candidate);
    }
    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        Guid id,
        CandidateRequest candidateRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<UpsertApplicationCommand>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));
        
        //Act
        var actual = await controller.PostCandidate(id, candidateRequest);
        
        //Assert
        var result = actual as StatusCodeResult;
        result?.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}