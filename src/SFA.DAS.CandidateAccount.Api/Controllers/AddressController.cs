using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]es/")]
public class AddressController(IMediator mediator, ILogger<AddressController> logger) : Controller
{
    [HttpGet]
    [Route("{candidateId}")]
    public async Task<IActionResult> Get([FromRoute] Guid candidateId)
    {
        try
        {
            var result = await mediator.Send(new GetAddressQuery
            {
                CandidateId = candidateId,
            });

            if (result.Address is null)
            {
                return NotFound();
            }
            return Ok((GetAddressApiResponse)result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetAddress : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
