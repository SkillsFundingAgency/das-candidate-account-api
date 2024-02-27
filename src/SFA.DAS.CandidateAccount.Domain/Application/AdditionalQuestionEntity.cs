namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class AdditionalQuestionEntity
    {
        public Guid Id { get; init; }
        public string QuestionId { get; init; }
        public string Answer { get; set; }
        public Guid ApplicationId { get; init; }

        public static implicit operator AdditionalQuestionEntity(AdditionalQuestion source)
        {
            return new AdditionalQuestionEntity
            {
                Answer = source.Answer,
                QuestionId = source.QuestionId,
                ApplicationId = source.ApplicationId,
                Id = source.Id
            };
        }
    }
}
