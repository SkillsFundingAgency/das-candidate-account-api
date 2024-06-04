using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationByVacancyReference;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/")]
public class VacanciesController(IMediator mediator, ILogger<VacanciesController> logger) : ControllerBase
{
    [HttpGet]
    [Route("{vacancyRef}/candidates")]
    public async Task<IActionResult> GetCandidateApplications([FromRoute]string vacancyRef, [FromQuery]bool allowEmailContact, [FromQuery]Guid? preferenceId, [FromQuery]ApplicationStatus? applicationStatus)
    {
        try
        {
            var result = await mediator.Send(new GetCandidatesByApplicationVacancyQuery
            {
                VacancyReference = vacancyRef,
                CanEmailOnly = allowEmailContact,
                StatusId = applicationStatus != null ? (short)applicationStatus : null,
                PreferenceId = preferenceId
            });
            return Ok(new GetCandidatesApiResponse
            {
                Candidates = result.Candidates.Select(c=>new CandidateApplication
                {
                    Candidate = c.CandidateEntity,
                    ApplicationId = c.Id,
                    ApplicationCreatedDate = c.CreatedDate
                }).ToList()
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetCandidateApplications : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}