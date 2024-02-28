namespace SFA.DAS.CandidateAccount.Domain.Application;

public record Question
{
    public Guid Id { get; set; }
    public required string QuestionText { get; set; }

    public static implicit operator Question(AdditionalQuestionEntity source)
    {
        return new Question
        {
            Id = source.Id,
            QuestionText = source.QuestionId
        };
    }
}