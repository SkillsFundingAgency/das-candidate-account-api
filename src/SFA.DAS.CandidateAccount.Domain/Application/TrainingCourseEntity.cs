namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class TrainingCourseEntity
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public Guid ApplicationId { get; set; }
        public string Title { get; set; }
    }
}
