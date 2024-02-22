using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkHistories;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteWorkHistory;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetWorkHistoryItem;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("candidates/{candidateId}/applications/{applicationId}/work-history")]
    public class WorkHistoryController(IMediator mediator, ILogger<WorkHistoryController> logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetWorkHistories([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, WorkHistoryType? workHistoryType)
        {
            try
            {
                var result = await mediator.Send(new GetApplicationWorkHistoriesQuery
                {
                    CandidateId = candidateId,
                    ApplicationId = applicationId,
                    WorkHistoryType = workHistoryType
                });
                return Ok((GetWorkHistoriesApiResponse)result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetWorkHistories : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, WorkHistoryType? workHistoryType)
        {
            try
            {
                var result = await mediator.Send(new GetWorkHistoryItemQuery
                {
                    CandidateId = candidateId,
                    ApplicationId = applicationId,
                    WorkHistoryType = workHistoryType,
                    Id = id
                });

                if (result is null)
                {
                    return NotFound();
                }

                return Ok((GetWorkHistoryItemApiResponse)result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetWorkHistoryItem : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutWorkHistoryItem([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, PutWorkHistoryItemRequest request)
        {
            try
            {
                var result = await mediator.Send(new UpsertWorkHistoryCommand
                {
                    WorkHistory = new WorkHistory
                    {
                        Id = id,
                        ApplicationId = applicationId,
                        Employer = request.Employer,
                        JobTitle = request.JobTitle,
                        Description = request.Description,
                        EndDate = request.EndDate,
                        StartDate = request.StartDate,
                        WorkHistoryType = request.WorkHistoryType
                    },
                    CandidateId = candidateId
                });

                if (result.IsCreated)
                {
                    return Created($"{result.WorkHistory.Id}", result.WorkHistory);
                }
                return Ok(result.WorkHistory);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Put WorkHistoryItem : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWorkHistory([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id)
        {
            try
            {
                var result = await mediator.Send(new DeleteWorkHistoryCommand
                {
                    ApplicationId = applicationId,
                    JobId = id,
                    CandidateId = candidateId
                });

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "DeleteWorkHistory : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
