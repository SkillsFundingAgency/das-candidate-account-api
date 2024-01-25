using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateJob;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers;

public class WhenCallingPostWorkHistory
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid candidateId,
        Guid applicationId,
        WorkHistoryRequest workHistoryRequest,
        CreateWorkHistoryResponse createWorkHistoryResponse,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] WorkHistoryController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<CreateWorkHistoryCommand>(c => 
                c.CandidateId.Equals(candidateId) &&
                c.ApplicationId.Equals(applicationId) &&
                c.EmployerName.Equals(workHistoryRequest.EmployerName)
                && c.JobTitle.Equals(workHistoryRequest.JobTitle)
                && c.JobDescription.Equals(workHistoryRequest.JobDescription)
                && c.StartDate.Equals(workHistoryRequest.StartDate)
                && c.EndDate.Equals(workHistoryRequest.EndDate)
            ), CancellationToken.None))
            .ReturnsAsync(createWorkHistoryResponse);
        
        //Act
        var actual = await controller.PostWorkHistory(candidateId, applicationId, workHistoryRequest);
        
        //Assert
        var result = actual as CreatedResult;
        var actualResult = result.Value as CandidateAccount.Domain.Application.WorkHistoryEntity;
        actualResult.Should().BeEquivalentTo(createWorkHistoryResponse.WorkHistory);
    }
    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        Guid candidateId,
        Guid applicationId,
        WorkHistoryRequest workHistoryRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] WorkHistoryController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<UpsertApplicationCommand>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));
        
        //Act
        var actual = await controller.PostWorkHistory(candidateId, applicationId, workHistoryRequest);
        
        //Assert
        var result = actual as StatusCodeResult;
        result?.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
    }
}