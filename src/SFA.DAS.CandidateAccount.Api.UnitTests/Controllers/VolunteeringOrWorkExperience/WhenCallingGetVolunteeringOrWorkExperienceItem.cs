using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.VolunteeringOrWorkExperience;
public class WhenCallingGetVolunteeringOrWorkExperienceItem
{
    [Test, MoqAutoData]
    public async Task Then_The_Response_Is_Returned_As_Expected(
        Guid applicationId,
        Guid candidateId,
        Guid id,
        GetVolunteeringOrWorkExperienceItemQueryResult response,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] VolunteeringOrWorkExperienceController controller)
    {
        mediator.Setup(x => x.Send(It.Is<GetVolunteeringOrWorkExperienceItemQuery>(
                c =>
                    c.ApplicationId.Equals(applicationId) &&
                    c.CandidateId.Equals(candidateId) &&
                    c.Id.Equals(id)
            ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var actual = await controller.Get(candidateId, applicationId, id) as OkObjectResult;

        using (new AssertionScope())
        {
            actual.Should().NotBeNull();
            actual?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            actual?.Value.Should().BeEquivalentTo((GetVolunteeringOrWorkExperienceItemApiResponse)response);
        }
    }

    [Test, MoqAutoData]
    public async Task And_Response_Is_Null_Then_Return_NotFound(
        Guid applicationId,
        Guid candidateId,
        Guid id,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] VolunteeringOrWorkExperienceController controller)
    {
        mediator.Setup(x => x.Send(It.Is<GetVolunteeringOrWorkExperienceItemQuery>(
                c =>
                    c.ApplicationId.Equals(applicationId) &&
                    c.CandidateId.Equals(candidateId) &&
                    c.Id.Equals(id)
            ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        var actual = await controller.Get(candidateId, applicationId, id);

        actual.Should().BeOfType<NotFoundResult>();
    }
}
