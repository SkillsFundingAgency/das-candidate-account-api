namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AboutYouEntity
    {
        public Guid Id { get; set; }
        public string? Strengths { get; set; }
        public string? Improvements { get; set; }
        public string? HobbiesAndInterests { get; set; }
        public string? Support { get; set; }
        public Guid ApplicationId { get; set; }

        public static implicit operator AboutYouEntity(AboutYou source)
        {
            return new AboutYouEntity
            {
                Id = source.Id,
                Strengths = source.Strengths,
                Improvements = source.Improvements,
                HobbiesAndInterests = source.HobbiesAndInterests,
                Support = source.Support,
                ApplicationId = source.ApplicationId
            };
        }
    }
}
