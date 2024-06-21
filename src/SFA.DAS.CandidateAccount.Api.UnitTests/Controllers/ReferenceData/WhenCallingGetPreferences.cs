using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailablePreferences;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.ReferenceData;

public class WhenCallingGetPreferences
{
    [Test, MoqAutoData]
    public async Task Then_The_Query_Is_Handled_And_Data_Returned(
        GetAvailablePreferencesQueryResult result,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ReferenceDataController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAvailablePreferencesQuery>(), CancellationToken.None))
            .ReturnsAsync(result);

        var actual = await controller.GetPreferences() as OkObjectResult;
        
        actual!.StatusCode.Should().Be((int) HttpStatusCode.OK);
        actual.Value.Should().BeEquivalentTo(new {Preferences= result.Preferences});
    }

    [Test, MoqAutoData]
    public async Task Then_If_There_Is_An_Exception_Internal_Server_Error_Response_Returned(
        [Frozen] Mock<IMediator> mediator,
        [Greedy]ReferenceDataController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAvailablePreferencesQuery>(), CancellationToken.None))
            .ThrowsAsync(new Exception());

        var actual = await controller.GetPreferences() as StatusCodeResult;

        actual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}