using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.UserAccount.Address;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetAddress;

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
    
    [HttpPut("{govUkIdentifier}")]
    public async Task<IActionResult> Put([FromRoute] string govUkIdentifier, AddressRequest request)
    {
        try
        {
            var result = await mediator.Send(new CreateUserAddressCommand
            {
                GovUkIdentifier = govUkIdentifier,
                Email = request.Email,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                AddressLine3 = request.AddressLine3,
                AddressLine4 = request.AddressLine4,
                Postcode = request.Postcode,
                Uprn = request.Uprn
            });

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Put address : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
