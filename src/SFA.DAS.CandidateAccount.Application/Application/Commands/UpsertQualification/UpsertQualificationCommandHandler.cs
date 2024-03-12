using MediatR;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertTrainingCourse;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertQualification;

public class UpsertQualificationCommandHandler(IQualificationRepository qualificationRepository, IApplicationRepository applicationRepository) : IRequestHandler<UpsertQualificationCommand, UpsertQualificationCommandResponse>
{
    public async Task<UpsertQualificationCommandResponse> Handle(UpsertQualificationCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.ApplicationId);
        if (application == null || application.CandidateId != request.CandidateId)
        {
            throw new InvalidOperationException($"Application {request.ApplicationId} not found");
        }

        var result = await qualificationRepository.Upsert(request.Qualification, request.CandidateId, request.ApplicationId);

        if (application.QualificationsStatus == (short)SectionStatus.NotStarted)
        {
            application.QualificationsStatus = (short)SectionStatus.InProgress;
            await applicationRepository.Update(application);
        }
        
        return new UpsertQualificationCommandResponse
        {
            Qualification = result.Item1,
            IsCreated = result.Item2
        };
    }
}