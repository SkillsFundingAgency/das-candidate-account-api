using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.CandidatePreferences
{
    public interface ICandidatePreferencesRepository
    {
        Task<List<CandidatePreferencesEntity>> Create(Guid candidateId);
        Task<List<CandidatePreferencesEntity?>> GetAllByCandidate(Guid candidateId);
        Task<List<Tuple<bool, CandidatePreferencesEntity>>> Upsert(List<CandidatePreference> candidatePreferences);
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

        public async Task<List<Tuple<bool, CandidatePreferencesEntity>>> Upsert(List<CandidatePreference> candidatePreferences)
        {
            var result = new List<Tuple<bool, CandidatePreferencesEntity>>();

            foreach (var candidatePreference in candidatePreferences)
            {
                var existingCandidatePreference = await dataContext.CandidatePreferencesEntities
                    .Where(cp => cp.CandidateId.Equals(candidatePreference.CandidateId) &&
                    cp.PreferenceId.Equals(candidatePreference.PreferenceId) &&
                    cp.ContactMethod == candidatePreference.ContactMethod)
                    .FirstOrDefaultAsync();

                if (existingCandidatePreference != null)
                {
                    existingCandidatePreference.Status = candidatePreference.Status;
                    existingCandidatePreference.UpdatedOn = DateTime.UtcNow;
                    dataContext.CandidatePreferencesEntities.Update(existingCandidatePreference);
                    await dataContext.SaveChangesAsync();
                    result.Add(new Tuple<bool, CandidatePreferencesEntity>(false, existingCandidatePreference));
                }
                else
                {
                    var newCandidatePreference = (CandidatePreferencesEntity)candidatePreference;
                    newCandidatePreference.CreatedOn = DateTime.UtcNow;
                    await dataContext.CandidatePreferencesEntities.AddAsync(newCandidatePreference);
                    await dataContext.SaveChangesAsync();
                    result.Add(new Tuple<bool, CandidatePreferencesEntity>(true, newCandidatePreference));
                }
            }
            return result;
        }
    }
}
