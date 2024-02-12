﻿using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
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
}
