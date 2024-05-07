using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;

public class GetApplicationQueryHandler(IApplicationRepository applicationRepository) : IRequestHandler<GetApplicationQuery, GetApplicationQueryResult>
{
    public async  Task<GetApplicationQueryResult> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var applicationEntity = await applicationRepository.GetById(request.ApplicationId, request.IncludeDetail);

        if (applicationEntity == null)
        {
            return new GetApplicationQueryResult();
        }

        if (applicationEntity.CandidateId != request.CandidateId)
        {
            var validationResult = new ValidationResult();
            validationResult.AddError(nameof(applicationEntity.CandidateId),"Application does not belong to candidate");
            throw new ValidationException(validationResult.DataAnnotationResult,null, null);
        }


        var application = request.IncludeDetail ? (ApplicationDetail)applicationEntity : (Domain.Application.Application)applicationEntity;
        
        return new GetApplicationQueryResult
        {
            Application = application
        };
    }
}