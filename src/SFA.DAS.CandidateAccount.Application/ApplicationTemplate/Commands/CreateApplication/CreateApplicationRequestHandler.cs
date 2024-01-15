using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.ApplicationTemplate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.ApplicationTemplate.Commands.CreateApplication;

public class CreateApplicationRequestHandler(
    ICandidateRepository candidateRepository,
    IApplicationTemplateRepository applicationTemplateRepository)
    : IRequestHandler<CreateApplicationRequest, CreateApplicationResponse>
{
    public async Task<CreateApplicationResponse> Handle(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetCandidateByEmail(request.Email);

        var validationResult = new ValidationResult();
        if (candidate == null)
        {
            validationResult.AddError(nameof(candidate), "CandidateAccount does not exist");
        }
        
        if (!validationResult.IsValid())
        {
            throw new ValidationException(validationResult.DataAnnotationResult,null, null);
        }
        var applicationTemplate = await applicationTemplateRepository.Upsert(new ApplicationTemplateEntity
        {
            VacancyReference = request.VacancyReference,
            CandidateId = candidate.Id,
            Status = request.Status
        });

        return new CreateApplicationResponse
        {
            ApplicationTemplate = applicationTemplate.Item1,
            IsCreated = applicationTemplate.Item2
        };
    }
}