using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Application.CandidatePreferences.Queries.GetCandidatePreferences;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/candidates/{candidateId}/[controller]s/")]
public class NotificationPreferenceController(IMediator mediator, ILogger<NotificationPreferenceController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid candidateId)
    {
        try
        {
            var result = await mediator.Send(new GetCandidatePreferencesQuery() { CandidateId = candidateId });
            return Ok(result);
        }
        catch(Exception e)
        {
            logger.LogError(e, "Get notification preferences: An error has occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
