using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateJob;

public class CreateWorkHistoryResponse
{
    public Guid WorkHistoryId { get; set; }
    public WorkExperienceEntity WorkHistory { get; set; }
}