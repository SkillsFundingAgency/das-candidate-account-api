using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsByVacancyReference;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;
using SFA.DAS.CandidateAccount.Domain.Application;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/")]
public class VacanciesController(IMediator mediator, ILogger<VacanciesController> logger) : ControllerBase
{
    [HttpGet]
    [Route("{vacancyRef}/candidates")]
    public async Task<IActionResult> GetCandidateApplications([FromRoute] string vacancyRef,
        [FromQuery] bool allowEmailContact,
        [FromQuery] Guid? preferenceId,
        [FromQuery] ApplicationStatus? applicationStatus)
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
                Candidates = result.Candidates.Select(c => new CandidateApplication
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
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("{vacancyRef}/applications")]
    public async Task<IActionResult> GetApplications([FromRoute] string vacancyRef)
    {
        try
        {
            var result = await mediator.Send(new GetApplicationsByVacancyReferenceQuery(vacancyRef));
            return Ok(new GetApplicationsApiResponse
            {
                Applications = result.Applications.Select(app => new GetApplicationsApiResponse.Application
                {
                    Id = app.Id,
                    CandidateId = app.CandidateId,
                    VacancyReference = app.VacancyReference,
                    SubmittedDate = app.SubmittedDate,
                    CreatedDate = app.CreatedDate,
                    WithdrawnDate = app.WithdrawnDate,
                    EmploymentLocation = app.EmploymentLocation,
                    Candidate = new GetApplicationsApiResponse.Candidate
                    {
                        Id = app.Candidate.Id,
                        Email = app.Candidate.Email,
                        LastName = app.Candidate.LastName,
                        FirstName = app.Candidate.FirstName,
                        MiddleNames = app.Candidate.MiddleNames
                    },
                    Status = app.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetApplications : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}