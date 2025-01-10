using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]s/")]
public class CandidateController(IMediator mediator, ILogger<ApplicationController> logger) : Controller
{
    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> PostCandidate(string id, PostCandidateRequest request)
    {
        try
        {
            var result = await mediator.Send(new CreateCandidateCommand
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                GovUkIdentifier = id,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                MigratedEmail = request.MigratedEmail,
                MigratedCandidateId = request.MigratedCandidateId
            });

            return Created($"{result.Candidate.Id}",result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Application : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCandidate(string id)
    {
        try
        {
            var result = await mediator.Send(new GetCandidateQuery
            {
                Id = id
            });
            if (result.Candidate == null)
            {
                return NotFound();
            }
            return Ok(result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Get Candidate : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
    
    [HttpPut]
    [Route("{candidateId}")]
    public async Task<IActionResult> PutCandidate([FromRoute] Guid candidateId, PutCandidateRequest postCandidateRequest)
    {
        try
        {
            var result = await mediator.Send(new UpsertCandidateCommand
            {
                Candidate = new Candidate
                {
                    Id = candidateId,
                    DateOfBirth = postCandidateRequest.DateOfBirth,
                    Email = postCandidateRequest.Email,
                    FirstName = postCandidateRequest.FirstName,
                    LastName = postCandidateRequest.LastName,
                    MiddleNames = postCandidateRequest.MiddleNames,
                    PhoneNumber = postCandidateRequest.PhoneNumber,
                    TermsOfUseAcceptedOn = postCandidateRequest.TermsOfUseAcceptedOn,
                    Status = postCandidateRequest.Status,
                    MigratedEmail = postCandidateRequest.MigratedEmail,
                    MigratedCandidateId = postCandidateRequest.MigratedCandidateId
                }
            });

            if (result.IsCreated)
            {
                return Created($"{result.Candidate.Id}",result.Candidate);
            }
            return Ok(result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Candidate : An error occurred");
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("{candidateId}")]
    public async Task<IActionResult> DeleteCandidate([FromRoute] Guid candidateId)
    {
        try
        {
            await mediator.Send(new DeleteCandidateCommand(candidateId));

            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Delete Candidate : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}