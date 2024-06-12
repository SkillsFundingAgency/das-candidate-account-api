using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.AboutYou
{
    public interface IAboutYouRespository
    {
        Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId);
        Task<Domain.Candidate.AboutYou?> Get(Guid candidateId);
    }

    public class AboutYouRepository(ICandidateAccountDataContext dataContext) : IAboutYouRespository
    {
        public async Task<Domain.Candidate.AboutYou?> Get(Guid candidateId)
        {
            var aboutYouItem = await dataContext.AboutYouEntities
                .Include(c=>c.CandidateEntity).AsNoTracking().SingleOrDefaultAsync(x =>
                x.CandidateId == candidateId);

            return aboutYouItem is null ? null : (Domain.Candidate.AboutYou)aboutYouItem;
        }

        public async Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId)
        {
            var aboutYouItem = await Get(candidateId);
            
            if (aboutYouItem is null)
            {
                var newEntity = (AboutYouEntity) aboutYouEntity;
                newEntity.CandidateId = candidateId;
                newEntity.Id = Guid.NewGuid();

                await dataContext.AboutYouEntities.AddAsync(newEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<Domain.Candidate.AboutYou, bool>(newEntity, true);
            }

            aboutYouItem.Id = aboutYouEntity.Id;
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
