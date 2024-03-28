using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Preference
{
    public interface IPreferencesRepository
    {
        Task<List<PreferenceEntity>> GetAll();
    }

    public class PreferencesRepository(ICandidateAccountDataContext dataContext) : IPreferencesRepository
    {
        public async Task<List<PreferenceEntity>> GetAll()
        {
            return await dataContext.PreferenceEntities.ToListAsync();
        }
    }
}
