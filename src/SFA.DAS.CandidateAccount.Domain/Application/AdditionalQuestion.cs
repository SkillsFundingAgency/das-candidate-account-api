namespace SFA.DAS.CandidateAccount.Domain.Application;

public class AdditionalQuestion
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? Answer { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid QuestionId { get; set; }

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