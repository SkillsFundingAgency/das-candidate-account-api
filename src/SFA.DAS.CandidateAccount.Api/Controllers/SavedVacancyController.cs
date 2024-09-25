using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.AddSavedVacancy;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancies;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancy;
using System.Net;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.RemoveSavedVacancy;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [Route("api/candidates/{candidateId}/saved-vacancies")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SavedVacancyController(IMediator mediator, ILogger<SavedVacancyController> logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetByCandidateId(Guid candidateId)
        {
            var result = await mediator.Send(new GetSavedVacanciesByCandidateIdQuery { CandidateId = candidateId });
            return Ok(result);
        }

        [HttpGet("{vacancyReference}")]
        public async Task<IActionResult> GetByVacancyReference(Guid candidateId, string vacancyReference)
        {
            try
            {
                var result = await mediator.Send(new GetSavedVacancyQuery(candidateId, vacancyReference));

                if (result.Id == Guid.Empty) return NotFound();

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetByVacancyReference : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid candidateId, SavedVacancyRequest request)
        {
            var result = await mediator.Send(new AddSavedVacancyCommand
            {
                CandidateId = candidateId,
                VacancyReference = request.VacancyReference,
                CreatedOn = request.CreatedOn
            });
            return Ok(result.SavedVacancy);
        }

        [HttpDelete("{vacancyReference}")]
        public async Task<IActionResult> RemoveSavedVacancy(Guid candidateId, string vacancyReference)
        {
            try
            {
                await mediator.Send(new RemoveSavedVacancyCommand(candidateId, vacancyReference));

                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "RemoveSavedVacancy : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
