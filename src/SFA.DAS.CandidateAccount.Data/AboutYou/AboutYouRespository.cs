using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.AboutYou
{
    public interface IAboutYouRespository
    {
        Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId);
        Task<Domain.Candidate.AboutYou?> Get(Guid applicationId, Guid candidateId);
    }

    public class AboutYouRepository(ICandidateAccountDataContext dataContext) : IAboutYouRespository
    {
        public async Task<Domain.Candidate.AboutYou?> Get(Guid applicationId, Guid candidateId)
        {
            var aboutYouItem = await dataContext.AboutYouEntities
                .Include(c=>c.ApplicationEntity).AsNoTracking().SingleOrDefaultAsync(x =>
                x.ApplicationId == applicationId && x.ApplicationEntity.CandidateId == candidateId);

            return aboutYouItem is null ? null : (Domain.Candidate.AboutYou)aboutYouItem;
        }

        public async Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId)
        {
            var aboutYouItem = await Get(aboutYouEntity.ApplicationId!.Value, candidateId);
            
            if (aboutYouItem is null)
            {
                await dataContext.AboutYouEntities.AddAsync((AboutYouEntity)aboutYouEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<Domain.Candidate.AboutYou, bool>(aboutYouEntity, true);
            }

            aboutYouItem.Id = aboutYouEntity.Id;
            aboutYouItem.Strengths = aboutYouEntity.Strengths;
            aboutYouItem.Support = aboutYouEntity.Support;
            aboutYouItem.Sex = aboutYouEntity.Sex;
            aboutYouItem.EthnicGroup = aboutYouEntity.EthnicGroup; 
            aboutYouItem.EthnicSubGroup = aboutYouEntity.EthnicSubGroup; 
            aboutYouItem.IsGenderIdentifySameSexAtBirth = aboutYouEntity.IsGenderIdentifySameSexAtBirth;
            aboutYouItem.OtherEthnicSubGroupAnswer = aboutYouEntity.OtherEthnicSubGroupAnswer;

            dataContext.AboutYouEntities.Update(aboutYouItem);

            await dataContext.SaveChangesAsync();
            return new Tuple<Domain.Candidate.AboutYou, bool>(aboutYouItem, false);
        }
    }
}
