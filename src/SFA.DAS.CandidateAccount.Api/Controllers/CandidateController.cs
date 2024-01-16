using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/")]
public class CandidateController(IMediator mediator, ILogger<ApplicationController> logger) : Controller
{
    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> PostCandidate(Guid id, CandidateRequest request)
    {
        try
        {
            var result = await mediator.Send(new CreateCandidateRequest
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                GovUkIdentifier = request.GovUkIdentifier,
                DateOfBirth = request.DateOfBirth
            });

            return Created($"{result.Candidate.Id}",result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Application : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}