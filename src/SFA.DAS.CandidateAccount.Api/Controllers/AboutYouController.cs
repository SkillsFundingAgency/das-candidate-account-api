using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.PutAboutYou;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("candidates/{candidateId}/applications/{applicationId}/about-you")]
public class AboutYouController(IMediator mediator, ILogger<AboutYouController> logger) : Controller
{
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutAboutYouItem([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, PutAboutYouItemRequest request)
    {
        try
        {
            var result = await mediator.Send(new UpsertAboutYouCommand
            {
                AboutYou = new AboutYou
                {
                    Id = id,
                    ApplicationId = applicationId,
                    Strengths = request.SkillsAndStrengths,
                    HobbiesAndInterests = request.HobbiesAndInterests,
                    Improvements = request.Improvements,
                    Support = request.Support,
                    Sex = request.Sex,
                    EthnicGroup = request.EthnicGroup,
                    EthnicSubGroup = request.EthnicSubGroup,
                    IsGenderIdentifySameSexAtBirth = request.IsGenderIdentifySameSexAtBirth,
                    OtherEthnicSubGroupAnswer = request.OtherEthnicSubGroupAnswer,
                },
                CandidateId = candidateId
            });

            if (result.IsCreated)
            {
                return Created($"{result.AboutYou.Id}", result.AboutYou);
            }
            return Ok(result.AboutYou);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Put AboutYou : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid candidateId, [FromRoute] Guid applicationId)
    {
        try
        {
            var result = await mediator.Send(new GetAboutYouItemQuery
            {
                CandidateId = candidateId,
                ApplicationId = applicationId
            });
            return Ok((GetAboutYouItemApiResponse)result.AboutYou);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetAboutYouItem : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
