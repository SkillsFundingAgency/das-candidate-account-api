using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/candidates/{candidateId}/applications/{applicationId}/employment-locations")]
    public class EmploymentLocationController(IMediator mediator, ILogger<EmploymentLocationController> logger) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll([FromRoute] Guid candidateId, [FromRoute] Guid applicationId)
        {
            try
            {
                var result = await mediator.Send(new GetEmploymentLocationsQuery
                {
                    CandidateId = candidateId,
                    ApplicationId = applicationId,
                });

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Get Employment Locations : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}