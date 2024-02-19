namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class TrainingCourseEntity
    {
        public Guid Id { get; set; }
        public string? Provider { get; set; }
        public int? FromYear { get; set; }
        public int ToYear { get; set; }
        public Guid ApplicationId { get; set; }
        public string Title { get; set; }

        public static implicit operator TrainingCourseEntity(TrainingCourse source)
        {
            return new TrainingCourseEntity
            {
                Id = source.Id,
                Provider = source.Provider,
                FromYear = source.FromYear,
                ToYear = source.ToYear,
                ApplicationId = source.ApplicationId,
                Title = source.Title
            };

        }
    }
}
