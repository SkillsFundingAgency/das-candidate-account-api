using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;

public class GetApplicationQueryHandler(
    IApplicationRepository applicationRepository,
    IAdditionalQuestionRepository additionalQuestionRepository
    ) : IRequestHandler<GetApplicationQuery, GetApplicationQueryResult>
{
    public async  Task<GetApplicationQueryResult> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.ApplicationId);

        if (application == null)
        {
            return new GetApplicationQueryResult();
        }

        if (application.CandidateId != request.CandidateId)
        {
            var validationResult = new ValidationResult();
            validationResult.AddError(nameof(application.CandidateId),"Application does not belong to candidate");
            throw new ValidationException(validationResult.DataAnnotationResult,null, null);
        }

        var questions = await additionalQuestionRepository.GetAll(request.ApplicationId, request.CandidateId, cancellationToken);
        
        return new GetApplicationQueryResult
        {
            Application = application,
            AdditionalQuestions = questions.Select(q => (Question)q).ToList()
        };
    }
}