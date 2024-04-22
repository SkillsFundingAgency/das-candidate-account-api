namespace SFA.DAS.CandidateAccount.Domain.Application;

public record Question
{
    public required Guid Id { get; init; }
    public required string QuestionText { get; init; }

    public static implicit operator Question?(AdditionalQuestionEntity? source)
    {
        if (source == null)
        {
            return null;
        }
        return new Question
        {
            Id = source.Id,
            QuestionText = source.QuestionText
        };
    }
}