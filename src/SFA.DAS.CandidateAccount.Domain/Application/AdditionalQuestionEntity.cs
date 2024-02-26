namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class AdditionalQuestionEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public Guid ApplicationId { get; set; }

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
