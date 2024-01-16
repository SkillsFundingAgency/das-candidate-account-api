using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/")]
public class ApplicationController(IMediator mediator, ILogger<ApplicationController> logger) : Controller
{
    [HttpPut]
    [Route("{vacancyReference}")]
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
}