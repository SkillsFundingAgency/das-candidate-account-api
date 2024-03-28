using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.CandidatePreferences
{
    public interface ICandidatePreferencesRepository
    {
        Task<List<CandidatePreferencesEntity>> Create(Guid candidateId);
        Task<List<CandidatePreferencesEntity?>> GetAllByCandidate(Guid candidateId);
    }

    public class CandidatePreferencesRepository(ICandidateAccountDataContext dataContext) : ICandidatePreferencesRepository
    {
        public async Task<List<CandidatePreferencesEntity?>> GetAllByCandidate(Guid candidateId)
        {
            var query = from item in dataContext.CandidatePreferencesEntities
                    .Where(tc => tc.CandidateId == candidateId)
                        select item;

            return await query.ToListAsync();
        }

        public async Task<List<CandidatePreferencesEntity>> Create(Guid candidateId)
        {
            var preferences = await dataContext.PreferenceEntities.ToListAsync();

            foreach (var item in preferences)
            {
                var textCandidatePreference = new CandidatePreferencesEntity
                {
                    Id = Guid.NewGuid(),
                    CandidateId = candidateId,
                    PreferenceId = item.PreferenceId,
                    ContactMethod = "Text",
                    Status = null,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = null
                };
                dataContext.CandidatePreferencesEntities.Add(textCandidatePreference);

                var emailCandidatePreference = new CandidatePreferencesEntity
                {
                    Id = Guid.NewGuid(),
                    CandidateId = candidateId,
                    PreferenceId = item.PreferenceId,
                    ContactMethod = "Email",
                    Status = null,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = null
                };
                dataContext.CandidatePreferencesEntities.Add(emailCandidatePreference);
            }

            await dataContext.SaveChangesAsync();

            return await dataContext.CandidatePreferencesEntities.Where(x => x.CandidateId == candidateId).ToListAsync();
        }
    }
}
