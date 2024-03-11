using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries;

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
            return Ok(result.QualificationReferences.OrderBy(c=>c.Order));
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetQualifications : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}