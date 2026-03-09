using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.AddSavedVacancy;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.SavedVacancies
{
    [TestFixture]
    public class WhenCallingPut
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            Guid candidateId,
            SavedVacancyRequest request,
            AddSavedVacancyCommandResult commandResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] SavedVacancyController controller)
        {
            mediator.Setup(x => x.Send(It.Is<AddSavedVacancyCommand>(c => c.CandidateId == candidateId && c.VacancyReference == request.VacancyReference), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResult);

            var result = await controller.Put(candidateId, request) as OkObjectResult;
            result.Value.Should().BeEquivalentTo(commandResult.SavedVacancy);
        }
    }
}
