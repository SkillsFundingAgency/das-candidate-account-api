using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.AddSavedVacancy;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancies;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [Route("api/candidates/{candidateId}/saved-vacancies")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SavedVacancyController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetByCandidateId(Guid candidateId)
        {
            var result = await mediator.Send(new GetSavedVacanciesByCandidateIdQuery { CandidateId = candidateId });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid candidateId, SavedVacancyRequest request)
        {
            var result = await mediator.Send(new AddSavedVacancyCommand() { CandidateId = candidateId, VacancyReference = request.VacancyReference});
            return Ok(result.SavedVacancy);
        }
    }
}
