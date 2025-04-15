using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations;
using SFA.DAS.CandidateAccount.Domain.Application;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/candidates/{candidateId}/applications/{applicationId}/employment-locations")]
    public class EmploymentLocationController(IMediator mediator, ILogger<EmploymentLocationController> logger) : Controller
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutEmploymentLocation([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, PutEmploymentLocationApiRequest request)
        {
            try
            {
                var result = await mediator.Send(new UpsertEmploymentLocationCommand
                {
                    EmploymentLocation = new EmploymentLocation
                    {
                        ApplicationId = applicationId,
                        Id = id,
                        EmployerLocationOption = request.EmployerLocationOption,
                        EmploymentLocationInformation = request.EmploymentLocationInformation,
                        Addresses = request.Addresses
                    },
                    CandidateId = candidateId
                });

                if (result.IsCreated)
                {
                    return Created($"{result.EmploymentLocation.Id}", result.EmploymentLocation);
                }
                return Ok(result.EmploymentLocation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Put AdditionalQuestion : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}