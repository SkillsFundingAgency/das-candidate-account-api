using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Domain.Application;

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
            var result = await mediator.Send(new UpsertApplicationRequest
            {
                Email = applicationRequest.Email,
                VacancyReference = vacancyReference,
                Status = applicationRequest.Status,
                DisabilityStatus = applicationRequest.DisabilityStatus,
                IsApplicationQuestionsComplete = applicationRequest.IsApplicationQuestionsComplete,
                IsDisabilityConfidenceComplete = applicationRequest.IsDisabilityConfidenceComplete,
                IsEducationHistoryComplete = applicationRequest.IsEducationHistoryComplete,
                IsInterviewAdjustmentsComplete = applicationRequest.IsInterviewAdjustmentsComplete,
                IsWorkHistoryComplete = applicationRequest.IsWorkHistoryComplete 
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
        catch (Exception e)
        {
            logger.LogError(e,"Unable to update application");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}