using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationByVacancyReference;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplications;
using SFA.DAS.CandidateAccount.Domain.Application;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/")]
public class ApplicationController(IMediator mediator, ILogger<ApplicationController> logger) : Controller
{
    [HttpPut]
    [Route("[controller]s/{vacancyReference}")]
    public async Task<IActionResult> PutApplication([FromRoute]string vacancyReference, ApplicationRequest applicationRequest)
    {
        try
        {
            var result = await mediator.Send(new UpsertApplicationCommand
            {
                CandidateId = applicationRequest.CandidateId,
                VacancyReference = vacancyReference,
                Status = applicationRequest.Status,
                DisabilityStatus = applicationRequest.DisabilityStatus,
                IsApplicationQuestionsComplete = applicationRequest.IsApplicationQuestionsComplete,
                IsDisabilityConfidenceComplete = applicationRequest.IsDisabilityConfidenceComplete,
                IsEducationHistoryComplete = applicationRequest.IsEducationHistoryComplete,
                IsInterviewAdjustmentsComplete = applicationRequest.IsInterviewAdjustmentsComplete,
                IsWorkHistoryComplete = applicationRequest.IsWorkHistoryComplete,
                IsAdditionalQuestion1Complete = applicationRequest.IsAdditionalQuestion1Complete,
                IsAdditionalQuestion2Complete = applicationRequest.IsAdditionalQuestion2Complete,
                IsEmploymentLocationComplete = applicationRequest.IsEmploymentLocationComplete,
                AdditionalQuestions = applicationRequest.AdditionalQuestions,
                EmploymentLocation = applicationRequest.EmploymentLocation
            });

            if (result.IsCreated)
            {
                return Created($"{result.Application.Id}",result.Application);
            }
            return Ok(result.Application);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult.ErrorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Application : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
    
    [HttpPatch]
    [Route("Candidates/{candidateId}/[controller]s/{id}")]
    public async Task<IActionResult> PatchApplication([FromRoute]Guid id,[FromRoute]Guid candidateId, [FromBody]JsonPatchDocument<PatchApplication> applicationRequest)
    {
        try
        {
            var result = await mediator.Send(new PatchApplicationCommand
            {
                Patch = applicationRequest,
                CandidateId = candidateId,
                Id = id
            });

            if (result.Application == null)
            {
                return NotFound();
            }

            return Ok(result.Application);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult.ErrorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Unable to update application");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("Candidates/{candidateId}/[controller]s/{id}")]
    public async Task<IActionResult> GetApplication([FromRoute] Guid id, [FromRoute] Guid candidateId, [FromQuery]bool includeDetail = false)
    {
        try
        {
            var result = await mediator.Send(new GetApplicationQuery
            {
                CandidateId = candidateId,
                ApplicationId = id,
                IncludeDetail = includeDetail
            });

            if (result == null || result.Application == null)
            {
                return NotFound();
            }

            var getApplicationApiResponse = includeDetail
                ? (GetApplicationApiResponse)(ApplicationDetail)result.Application
                : (Domain.Application.Application)result.Application;
            return Ok(getApplicationApiResponse);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult.ErrorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Unable to get application");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("Candidates/{candidateId}/[controller]s")]
    public async Task<IActionResult> GetApplications([FromRoute] Guid candidateId, [FromQuery] ApplicationStatus? status)
    {
        try
        {
            var result = await mediator.Send(new GetApplicationsQuery
            {
                CandidateId = candidateId,
                Status = status
            });

            return Ok(result);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult.ErrorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to get applications");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("Candidates/{candidateId}/[controller]s/GetByReference/{vacancyReference:required}")]
    public async Task<IActionResult> GetApplicationByVacancyReference([FromRoute] Guid candidateId, [FromRoute] string vacancyReference)
    {
        try
        {
            var result = await mediator.Send(new GetApplicationByVacancyReferenceQuery
            {
                CandidateId = candidateId,
                VacancyReference = vacancyReference
            });

            if (result.Application == null)
            {
                return NotFound();
            }

            return Ok(result.Application);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult.ErrorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to get application by reference");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    [Route("[controller]s")]
    public async Task<IActionResult> PostApplication(PostApplicationRequest applicationRequest)
    {
        try
        {
            var result = await mediator.Send(new AddLegacyApplicationCommand
            {
                LegacyApplication   = applicationRequest.LegacyApplication
            });

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Post Application : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}