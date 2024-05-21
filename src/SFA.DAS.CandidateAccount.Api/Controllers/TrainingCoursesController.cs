using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteTrainingCourse;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertTrainingCourse;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourseItem;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/candidates/{candidateId}/applications/{applicationId}/trainingcourses")]
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

            if (result is null)
            {
                return NotFound();
            }

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
            var result = await mediator.Send(new UpsertTrainingCourseCommand
            {
                ApplicationId = applicationId,
                TrainingCourse = new Domain.Application.TrainingCourse
                {
                    Id = id,
                    CandidateId = candidateId,
                    ApplicationId = applicationId,
                    Title = request.CourseName,
                    ToYear = (int)request.YearAchieved
                },
                CandidateId = candidateId
            });

            if (result.IsCreated)
            {
                return Created($"{result.TrainingCourse.Id}", result.TrainingCourse);
            }
            return Ok(result.TrainingCourse);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Put TrainingCourseItem : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTrainingCourse([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id)
    {
        try
        {
            var result = await mediator.Send(new DeleteTrainingCourseCommand
            {
                ApplicationId = applicationId,
                Id = id,
                CandidateId = candidateId
            });

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "DeleteJob : An error occurred");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
