using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application
{
    [TestFixture]
    public class WhenPostingLegacyApplication
    {
        [Test, MoqAutoData]
        public async Task Then_Legacy_Application_Is_Created_And_Its_Id_Is_Returned(
            PostApplicationRequest request,
            AddLegacyApplicationCommandResponse commandResponse,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] ApplicationController controller)
        {
            mediator.Setup(x => x.Send(It.Is<AddLegacyApplicationCommand>(c => c.LegacyApplication == request.LegacyApplication), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            var result = await controller.PostApplication(request);

            result.Should().BeOfType<OkObjectResult>();
            var actionResult = result as OkObjectResult;
            actionResult.Value.Should().BeOfType<AddLegacyApplicationCommandResponse>();
            var value = actionResult.Value as AddLegacyApplicationCommandResponse;
            value.Id.Should().Be(commandResponse.Id);
        }
    }
}
