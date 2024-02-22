using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.AboutYou
{
    public interface IAboutYouRespository
    {
        Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId);
    }

    public class AboutYouRepository(ICandidateAccountDataContext dataContext) : IAboutYouRespository
    {
        public async Task<Tuple<Domain.Candidate.AboutYou, bool>> Upsert(Domain.Candidate.AboutYou aboutYouEntity, Guid candidateId)
        {
            var query = from item in dataContext.AboutYouEntities
                    .Where(tc => tc.Id == aboutYouEntity.Id)
                    .Where(tc => tc.ApplicationId == aboutYouEntity.ApplicationId)
                        join application in dataContext.ApplicationEntities.Where(app => app.CandidateId == candidateId && app.Id == aboutYouEntity.ApplicationId)
                        on item.ApplicationId equals application.Id
                        select item;

            var aboutYouItem = await query.SingleOrDefaultAsync();

            if (aboutYouItem is null)
            {
                await dataContext.AboutYouEntities.AddAsync((AboutYouEntity)aboutYouEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<Domain.Candidate.AboutYou, bool>(aboutYouEntity, true);
            }

            aboutYouItem.Strengths = aboutYouEntity.Strengths;
            aboutYouItem.Improvements = aboutYouEntity.Improvements;
            aboutYouItem.HobbiesAndInterests = aboutYouEntity.HobbiesAndInterests;
            aboutYouItem.Support = aboutYouEntity.Support;

            dataContext.AboutYouEntities.Update(aboutYouItem);

            await dataContext.SaveChangesAsync();
            return new Tuple<Domain.Candidate.AboutYou, bool>(aboutYouItem, false);
        }
    }
}
