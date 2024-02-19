using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("candidates/{candidateId}/applications/{applicationId}/volunteeringorworkexperience")]
public class VolunteeringOrWorkExperienceController(IMediator mediator, ILogger<VolunteeringOrWorkExperienceController> logger) : Controller
{
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id)
    {
        try
        {
            var result = await mediator.Send(new GetVolunteeringOrWorkExperienceItemQuery
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                Id = id
            });

            if (result is null)
            {
                return NotFound();
            }

            return Ok((GetVolunteeringOrWorkExperienceItemApiResponse)result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetVolunteeringOrWorkExperienceItem : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
