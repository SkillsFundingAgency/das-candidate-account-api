using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AboutYouEntity
    {
        public Guid Id { get; set; }
        public GenderIdentity? Sex { get; set; }
        public EthnicGroup? EthnicGroup { get; set; }
        public EthnicSubGroup? EthnicSubGroup { get; set; }
        public string? IsGenderIdentifySameSexAtBirth { get; set; }
        public string? OtherEthnicSubGroupAnswer { get; set; }
        public Guid CandidateId { get; set; }
        public virtual CandidateEntity CandidateEntity { get; set; }

        public static implicit operator AboutYouEntity(AboutYou source)
        {
            return new AboutYouEntity
            {
                Id = source.Id,
                Sex = source.Sex,
                EthnicGroup = source.EthnicGroup,
                EthnicSubGroup = source.EthnicSubGroup,
                IsGenderIdentifySameSexAtBirth = source.IsGenderIdentifySameSexAtBirth,
                OtherEthnicSubGroupAnswer = source.OtherEthnicSubGroupAnswer,
                CandidateId = (Guid)source.CandidateId
            };
        }
    }
}
