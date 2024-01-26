using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateWorkHistory;

namespace SFA.DAS.CandidateAccount.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("candidates/{candidateId}/applications/{applicationId}/work-history")]
    public class WorkHistoryController(IMediator mediator, ILogger<ApplicationController> logger) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> PostWorkHistory([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, WorkHistoryRequest request)
        {
            try
            {
                var result = await mediator.Send(new CreateWorkHistoryCommand
                {
                    WorkHistoryType = request.WorkHistoryType,
                    CandidateId = candidateId,
                    ApplicationId = applicationId,
                    EmployerName = request.EmployerName,
                    JobDescription = request.JobDescription,
                    JobTitle = request.JobTitle,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                });

                return Created($"{result.WorkHistoryId}", result.WorkHistory);
            }
            catch (Exception e)
            {
                logger.LogError(e, "PostJob : An error occurred");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
