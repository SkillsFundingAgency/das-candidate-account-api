using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.AdditionalQuestion;

public interface IAdditionalQuestionRepository
{
    Task<Tuple<WorkHistoryEntity, bool>> UpsertWorkHistory(AdditionalQuestion workHistoryEntity, Guid candidateId);
}

public class AdditionalQuestionRepository : IAdditionalQuestionRepository
{
    
}