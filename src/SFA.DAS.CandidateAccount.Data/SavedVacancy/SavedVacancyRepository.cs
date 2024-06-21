using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.SavedVacancy
{
    public interface ISavedVacancyRepository
    {
        Task<List<Domain.Candidate.SavedVacancy>> GetByCandidateId(Guid candidateId);
        Task<Domain.Candidate.SavedVacancy> Upsert(Domain.Candidate.SavedVacancy savedVacancy, Guid candidateId);
        Task<Domain.Candidate.SavedVacancy> Delete(Domain.Candidate.SavedVacancy savedVacancy, Guid candidateId);
    }

    public class SavedVacancyRepository(ICandidateAccountDataContext dataContext) : ISavedVacancyRepository
    {
        public async Task<List<Domain.Candidate.SavedVacancy>> GetByCandidateId(Guid candidateId)
        {
            var savedVacancyItems = await dataContext.SavedVacancyEntities
                .AsNoTracking()
                .Where(x => x.CandidateId == candidateId)
                .ToListAsync();

            return savedVacancyItems.Select(x => (Domain.Candidate.SavedVacancy)x).ToList();
        }

        public Task<Domain.Candidate.SavedVacancy> Delete(Domain.Candidate.SavedVacancy savedVacancy, Guid candidateId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Candidate.SavedVacancy> Upsert(Domain.Candidate.SavedVacancy savedVacancy, Guid candidateId)
        {
            throw new NotImplementedException();
        }
    }
}
