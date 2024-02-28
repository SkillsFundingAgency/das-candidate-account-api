using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetAdditionalQuestion;

public class GetAdditionalQuestionItemQueryResult
{
    public Guid Id { get; set; }
    public string? QuestionId { get; set; }
    public string? Answer { get; set; }
    public Guid ApplicationId { get; set; }

    public static implicit operator GetAdditionalQuestionItemQueryResult(AdditionalQuestionEntity source)
    {
        return new GetAdditionalQuestionItemQueryResult
        {
            Id = source.Id,
            ApplicationId = source.ApplicationId,
            QuestionId = source.QuestionId,
            Answer = source.Answer,
        };
    }
}