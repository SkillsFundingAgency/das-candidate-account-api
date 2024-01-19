using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.CandidateAccount.Data.Application;
using ValidationResult = SFA.DAS.CandidateAccount.Domain.RequestHandlers.ValidationResult;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.PatchApplication;

public class PatchApplicationCommandHandler (IApplicationRepository applicationRepository): IRequestHandler<PatchApplicationCommand,PatchApplicationCommandResponse>
{
    public async Task<PatchApplicationCommandResponse> Handle(PatchApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.GetById(request.Id);

        if (application == null)
        {
            return new PatchApplicationCommandResponse();
        }

        var patchedDoc = (Domain.Application.PatchApplication)application;
        
        request.Patch.ApplyTo(patchedDoc);

        application.Status = (short)patchedDoc.Status;
        application.TrainingCoursesStatus = (short)patchedDoc.TrainingCoursesStatus;
        application.QualificationsStatus = (short)patchedDoc.QualificationsStatus;
        application.JobsStatus = (short)patchedDoc.JobsStatus;
        application.DisabilityConfidenceStatus = (short)patchedDoc.DisabilityConfidenceStatus;
        application.SkillsAndStrengthStatus = (short)patchedDoc.SkillsAndStrengthStatus;
        application.InterviewAdjustmentsStatus = (short)patchedDoc.InterviewAdjustmentsStatus;
        application.AdditionalQuestion1Status = (short)patchedDoc.AdditionalQuestion1Status;
        application.AdditionalQuestion2Status = (short)patchedDoc.AdditionalQuestion2Status;
        application.InterestsStatus = (short)patchedDoc.InterestsStatus;
        application.WorkExperienceStatus = (short)patchedDoc.WorkExperienceStatus;
        application.UpdatedDate = DateTime.UtcNow;

        var updatedApplication = await applicationRepository.Update(application);
        
        return new PatchApplicationCommandResponse
        {
            Application = updatedApplication
        };
    }
}