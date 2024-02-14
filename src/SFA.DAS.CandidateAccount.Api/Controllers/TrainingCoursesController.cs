using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourseItem;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("candidates/{candidateId}/applications/{applicationId}/trainingcourses")]
public class TrainingCoursesController(IMediator mediator, ILogger<WorkHistoryController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTrainingCourses([FromRoute] Guid candidateId, [FromRoute] Guid applicationId)
    {
        try
        {
            var result = await mediator.Send(new GetTrainingCoursesQuery
            {
                CandidateId = candidateId,
                ApplicationId = applicationId
            });
            return Ok((GetTrainingCoursesApiResponse)result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetTrainingCourses : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id)
    {
        try
        {
            var result = await mediator.Send(new GetTrainingCourseItemQuery
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                Id = id
            });
            return Ok((GetTrainingCourseItemApiResponse)result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetTrainingCourseItem : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutTrainingCourseItem([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id, PutTrainingCourseItemRequest request)
    {
        try
        {
            await mediator.Send(new UpdateTrainingCourseCommand
            {
                Id = id,
                ApplicationId = applicationId,
                CandidateId = candidateId,
                CourseName = request.CourseName,
                YearAchieved = (int)request.YearAchieved,
            });
            return Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Put TrainingCourseItem : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostTrainingCourse([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, TrainingCourseRequest request)
    {
        try
        {
            var result = await mediator.Send(new CreateTrainingCourseCommand
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                CourseName = request.CourseName,
                YearAchieved = (int)request.YearAchieved
            });

            return Created($"{result.TrainingCourseId}", result.TrainingCourse);
        }
        catch (Exception e)
        {
            logger.LogError(e, "PostTrainingCourse : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
