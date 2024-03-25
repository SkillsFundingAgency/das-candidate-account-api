using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualification;
using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteQualificationsByReferenceId;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertQualification;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationQualificationsByType;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualification;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetQualifications;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/candidates/{candidateId}/applications/{applicationId}/[controller]s/")]
public class QualificationController(IMediator mediator, ILogger<QualificationController> logger) : Controller
{
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid candidateId, [FromRoute] Guid applicationId,[FromRoute]Guid id)
    {
        try
        {
            var result = await mediator.Send(new GetQualificationQuery
            {
                ApplicationId = applicationId,
                CandidateId = candidateId,
                Id = id
            });

            if (result.Qualification == null)
            {
                return NotFound();
            }

            return Ok(new GetQualificationApiResponse{Qualification = result.Qualification});
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting qualification");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> DeleteByReferenceId([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromQuery]Guid qualificationReferenceId)
    {
        try
        {
            await mediator.Send(new DeleteQualificationsByReferenceIdCommand
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                QualificationReferenceId = qualificationReferenceId
            });
            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting qualifications");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromRoute] Guid id)
    {
        try
        {
            await mediator.Send(new DeleteQualificationCommand
            {
                CandidateId = candidateId,
                ApplicationId = applicationId,
                Id = id
            });
            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting qualification");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromQuery]Guid? qualificationTypeId = null)
    {
        try
        {
            List<Qualification> response;
            if (qualificationTypeId == null)
            {
                var result = await mediator.Send(new GetApplicationQualificationsQuery
                {
                    ApplicationId = applicationId,
                    CandidateId = candidateId
                });
                response = result.Qualifications;
            }
            else
            {
                var result = await mediator.Send(new GetApplicationQualificationsByTypeQuery
                {
                    ApplicationId = applicationId,
                    CandidateId = candidateId,
                    QualificationReferenceId = qualificationTypeId.Value
                });
                response = result.Qualifications;
            }

            return Ok(new GetQualificationsApiResponse{Qualifications = response});
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting qualification");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromRoute] Guid candidateId, [FromRoute] Guid applicationId, [FromBody] QualificationRequest request)
    {
        try
        {
            var result = await mediator.Send(new UpsertQualificationCommand
            {
                ApplicationId = applicationId,
                CandidateId = candidateId,
                QualificationReferenceId = request.QualificationReferenceId,
                Qualification = new Qualification
                {
                    Id = request.Id,
                    ToYear = request.ToYear,
                    Grade = request.Grade,
                    Subject = request.Subject,
                    IsPredicted = request.IsPredicted,
                    AdditionalInformation = request.AdditionalInformation
                }
            });

            if (result.IsCreated)
            {
                return Created($"api/candidates/{candidateId}/applications/{applicationId}/qualifications/{result.Qualification.Id}", result.Qualification);
            }

            return Ok(new GetQualificationApiResponse{Qualification = result.Qualification});

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting qualification");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}