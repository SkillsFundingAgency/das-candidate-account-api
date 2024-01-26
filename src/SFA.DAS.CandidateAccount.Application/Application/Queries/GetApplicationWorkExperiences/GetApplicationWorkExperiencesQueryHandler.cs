using MediatR;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationWorkExperiences;

public record GetApplicationWorkExperiencesQueryHandler(Iwork applicationRepository) : IRequestHandler<GetApplicationQuery, GetApplicationQueryResult>
{
    public async Task<GetApplicationQueryResult> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.ApplicationId);

        if (application == null)
        {
            return new GetApplicationQueryResult();
        }

        if (application.CandidateId != request.CandidateId)
        {
            var validationResult = new ValidationResult();
            validationResult.AddError(nameof(application.CandidateId), "Application does not belong to candidate");
            throw new ValidationException(validationResult.DataAnnotationResult, null, null);
        }

        return new GetApplicationQueryResult
        {
            Application = application
        };
    }
}