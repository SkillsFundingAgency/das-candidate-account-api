using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertAdditionalQuestion;
using SFA.DAS.CandidateAccount.Domain.Application;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("candidates/{candidateId}/applications/{applicationId}/additional-question")]
    public class AdditionalQuestionController(IMediator mediator, ILogger<AdditionalQuestionController> logger) : Controller
    {
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAdditionalQuestionItem([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, AdditionalQuestionRequest request)
        {
            try
            {
                var result = await mediator.Send(new UpsertAdditionalQuestionCommand
                {
                    AdditionalQuestion = new AdditionalQuestion
                    {
                        Id = id,
                        ApplicationId = applicationId,
                        CandidateId = candidateId,
                        Answer = request.Answer,
                        QuestionId = request.QuestionId
                    },
                    CandidateId = candidateId
                });

                if (result.IsCreated)
                {
                    return Created($"{result.AdditionalQuestion.Id}", result.AdditionalQuestion);
                }
                return Ok(result.AdditionalQuestion);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Put AdditionalQuestion : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
