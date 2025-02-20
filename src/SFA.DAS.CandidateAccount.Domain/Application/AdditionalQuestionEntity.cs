namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class AdditionalQuestionEntity
    {
        public Guid Id { get; init; }
        public required string QuestionText { get; init; }
        public string? Answer { get; set; }
        public Guid ApplicationId { get; init; }
        public short? QuestionOrder { get; init; }
        public virtual ApplicationEntity ApplicationEntity { get; set; }

        public static implicit operator AdditionalQuestionEntity(AdditionalQuestion source)
        {
            return new AdditionalQuestionEntity
            {
                Answer = source.Answer,
                QuestionText = source.QuestionText,
                ApplicationId = source.ApplicationId,
                Id = source.Id,
                QuestionOrder = source.QuestionOrder
            };
        }
    }
}
