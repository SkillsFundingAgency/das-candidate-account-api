using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetAboutYouItemApiResponse
{
    public AboutYouItem AboutYou { get; set; }
    
    public class AboutYouItem
    {
        public Guid Id { get; set; }
        public GenderIdentity? Sex { get; set; }
        public EthnicGroup? EthnicGroup { get; set; }
        public EthnicSubGroup? EthnicSubGroup { get; set; }
        public string? IsGenderIdentifySameSexAtBirth { get; set; }
        public string? OtherEthnicSubGroupAnswer { get; set; }

    }

    public static implicit operator GetAboutYouItemApiResponse(GetAboutYouItemQueryResult source)
    {
        if (source.AboutYou == null) return new GetAboutYouItemApiResponse();

        return new GetAboutYouItemApiResponse
        {
            AboutYou = new AboutYouItem
            {
                Id = source.AboutYou.Id,
                EthnicGroup = source.AboutYou.EthnicGroup,
                EthnicSubGroup = source.AboutYou.EthnicSubGroup,
                IsGenderIdentifySameSexAtBirth = source.AboutYou.IsGenderIdentifySameSexAtBirth,
                OtherEthnicSubGroupAnswer = source.AboutYou.OtherEthnicSubGroupAnswer,
                Sex = source.AboutYou.Sex
            }
        };
    }
}


