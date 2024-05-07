using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.AboutYou;
public class WhenCallingGetAboutYouItem
{
    [Test, MoqAutoData]
    public async Task Then_The_Command_Is_Sent_To_Mediator_And_Ok_Returned(
        Guid applicationId,
        Guid candidateId,
        GetAboutYouItemQueryResult response,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] AboutYouController controller)
    {
        mediator.Setup(x => x.Send(It.Is<GetAboutYouItemQuery>(
                c =>
                    c.ApplicationId.Equals(applicationId) &&
                    c.CandidateId.Equals(candidateId)
            ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var actual = await controller.Get(candidateId, applicationId) as OkObjectResult;

        using (new AssertionScope())
        {
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeEquivalentTo((GetAboutYouItemApiResponse)response.AboutYou);
        }
    }

    [Test, MoqAutoData]
    public async Task Then_If_Exception_Returned_From_Mediator_Then_InternalServerError_Is_Returned(
        Guid applicationId,
        Guid candidateId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] AboutYouController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAboutYouItemQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        var actual = await controller.Get(candidateId, applicationId) as StatusCodeResult;

        using (new AssertionScope())
        {
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
