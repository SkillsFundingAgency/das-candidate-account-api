using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetInactiveCandidates;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]s/")]
public class CandidateController(IMediator mediator, ILogger<ApplicationController> logger) : ControllerBase
{
    [HttpPost]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> PostCandidate(string id, PostCandidateRequest request)
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
                MigratedCandidateId = request.MigratedCandidateId,
            });

            return TypedResults.Created($"{result.Candidate.Id}",result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Application : An error occurred");
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetCandidate(string id)
    {
        try
        {
            var result = await mediator.Send(new GetCandidateQuery
            {
                Id = id
            });
            if (result.Candidate == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Get Candidate : An error occurred");
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut]
    [Route("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> PutCandidate([FromRoute] Guid candidateId, PutCandidateRequest postCandidateRequest)
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
                return TypedResults.Created($"{result.Candidate.Id}",result.Candidate);
            }
            return TypedResults.Ok(result.Candidate);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Upsert Candidate : An error occurred");
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Route("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> DeleteCandidate([FromRoute] Guid candidateId)
    {
        try
        {
            await mediator.Send(new DeleteCandidateCommand(candidateId));
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Delete Candidate : An error occurred");
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("GetInactiveCandidates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetInactiveCandidates([FromQuery, Required] DateTime cutOffDateTime, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
    {
        try
        {
            var result = await mediator.Send(new GetInactiveCandidatesQuery(cutOffDateTime, pageNumber, pageSize));
            return TypedResults.Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Get Candidates By Activity : An error occurred");
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("by-email/{emailAddress}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetCandidateByEmailAddress([FromRoute] string emailAddress)
    {
        try
        {
            var result = await mediator.Send(new GetCandidateByEmailQuery(emailAddress));
            return result is { Candidate: null } 
                ? TypedResults.NotFound()
                : TypedResults.Ok(result.Candidate);
        }
        catch (InvalidOperationException)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Title = "Too many accounts",
                Detail = "More than one account found to match the specified email address",                
                Status = StatusCodes.Status400BadRequest,
            });
        }
    }
    
    [HttpPatch, Consumes("application/json", "application/json-patch+json", "text/json", "application/*+json")]
    [Route("{candidateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> PatchCandidate(
        [FromRoute] string candidateId,
        [FromBody] JsonPatchDocument<Candidate> patchDocument)
    {
        var candidateResult = await mediator.Send(new GetCandidateQuery { Id = candidateId });
        if (candidateResult is { Candidate: null })
        {
            return TypedResults.NotFound();
        }

        var candidate = candidateResult.Candidate;
        patchDocument.ApplyTo(candidate);
        
        var upsertResult = await mediator.Send(new UpsertCandidateCommand { Candidate = candidate });
        return TypedResults.Ok(upsertResult.Candidate);
    }
}