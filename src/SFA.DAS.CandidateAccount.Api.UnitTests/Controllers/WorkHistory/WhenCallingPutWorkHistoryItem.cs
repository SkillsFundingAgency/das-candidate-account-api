using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.WorkHistory;

public class WhenCallingPutWorkHistory
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid id,
        Guid candidateId,
        Guid applicationId,
        PutWorkHIstoryItemRequest workHistoryItemRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] WorkHistoryController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<UpdateWorkHistoryCommand>(c => 
            c.Id.Equals(id)
            && c.CandidateId.Equals(candidateId)
            && c.ApplicationId.Equals(applicationId)
            && c.EmployerName.Equals(workHistoryItemRequest.Employer)
            && c.JobTitle.Equals(workHistoryItemRequest.JobTitle)
            && c.JobDescription.Equals(workHistoryItemRequest.Description)
            && c.StartDate.Equals(workHistoryItemRequest.StartDate)
            && c.EndDate.Equals(workHistoryItemRequest.EndDate)
            ), CancellationToken.None));
        
        //Act
        var actual = await controller.PutWorkHistoryItem(candidateId, applicationId, id, workHistoryItemRequest);
        
        //Assert
        actual.Should().BeOfType<OkResult>();
    }
}