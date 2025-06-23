using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailablePreferences;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailableQualifications;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/")]
public class ReferenceDataController(IMediator mediator, ILogger<ReferenceDataController> logger) : Controller
{
    [HttpGet]
    [Route("qualifications")]
    public async Task<IActionResult> GetQualifications()
    {
        try
        {
            var result = await mediator.Send(new GetAvailableQualificationsQuery());
            return Ok(new {QualificationReferences = result.QualificationReferences});
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetQualifications : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("preferences")]
    public async Task<IActionResult> GetPreferences()
    {
        try
        {
            var result = await mediator.Send(new GetAvailablePreferencesQuery());
            return Ok(new {Preferences = result.Preferences});
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetPreferences : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}