using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.CandidatePreferences
{
    public interface ICandidatePreferencesRepository
    {
        Task<CandidatePreferencesEntity> Create(CandidatePreferencesEntity candidatePreferenceEntity);
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

        public async Task<CandidatePreferencesEntity> Create(CandidatePreferencesEntity candidatePreferenceEntity)
        {
            await dataContext.CandidatePreferencesEntities.AddAsync(candidatePreferenceEntity);
            await dataContext.SaveChangesAsync();
            return candidatePreferenceEntity;
        }
    }
}
