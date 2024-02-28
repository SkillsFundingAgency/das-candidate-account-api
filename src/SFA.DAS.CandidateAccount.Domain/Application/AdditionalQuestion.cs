namespace SFA.DAS.CandidateAccount.Domain.Application;

public class AdditionalQuestion
{
    public Guid Id { get; init; }
    public Guid CandidateId { get; set; }
    public Guid ApplicationId { get; init; }
    public string QuestionId { get; init; }
    public string? Answer { get; init; }

    public static implicit operator AdditionalQuestion(AdditionalQuestionEntity source)
    {
        return new AdditionalQuestion
        {
            Id = source.Id,
            Answer = source.Answer,
            ApplicationId = source.ApplicationId,
            QuestionId = source.QuestionId,
        };
    }
}