using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.SavedVacancy
{
    public interface ISavedVacancyRepository
    {
        Task<List<Domain.Candidate.SavedVacancy?>> GetByCandidateId(Guid candidateId);
        Task<Domain.Candidate.SavedVacancy?> Get(Guid candidateId, string? vacancyId, string? vacancyReference);
        Task<List<Domain.Candidate.SavedVacancy?>> GetAllByVacancyReference(Guid candidateId, string vacancyReference);
        Task<Tuple<Domain.Candidate.SavedVacancy, bool>> Upsert(Domain.Candidate.SavedVacancy savedVacancy);
        Task Delete(Domain.Candidate.SavedVacancy savedVacancy);
    }

    public class SavedVacancyRepository(ICandidateAccountDataContext dataContext) : ISavedVacancyRepository
    {
        public async Task<List<Domain.Candidate.SavedVacancy?>> GetByCandidateId(Guid candidateId)
        {
            var savedVacancyItems = await dataContext.SavedVacancyEntities
                .AsNoTracking()
                .Where(x => x.CandidateId == candidateId)
                .ToListAsync();

            return savedVacancyItems.Select(x => (Domain.Candidate.SavedVacancy?)x).ToList();
        }

        public async Task<Domain.Candidate.SavedVacancy?> Get(Guid candidateId, string? vacancyId, string? vacancyReference)
        {
            var query = dataContext.SavedVacancyEntities
                .AsNoTracking()
                .Where(x => x.CandidateId == candidateId);

            SavedVacancyEntity? result = null;

            if (!string.IsNullOrEmpty(vacancyId))
            {
                query = query.Where(x => x.VacancyId == vacancyId);
                result = await query.SingleOrDefaultAsync();
            }
            else if (!string.IsNullOrEmpty(vacancyReference))
            {
                query = query.Where(x => x.VacancyReference == vacancyReference);
                result = await query.FirstOrDefaultAsync();
            }

            return result;
        }

        public async Task<List<Domain.Candidate.SavedVacancy?>> GetAllByVacancyReference(Guid candidateId, string vacancyReference)
        {
            var query = dataContext.SavedVacancyEntities
                .AsNoTracking()
                .Where(x => x.CandidateId == candidateId && x.VacancyReference == vacancyReference);

            var result = await query.ToListAsync();

            return result.Select(x => (Domain.Candidate.SavedVacancy?)x).ToList();
        }

        public async Task Delete(Domain.Candidate.SavedVacancy savedVacancy)
        {
             dataContext.SavedVacancyEntities.Remove(savedVacancy!);
             await dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<Domain.Candidate.SavedVacancy, bool>> Upsert(Domain.Candidate.SavedVacancy savedVacancy)
        {
            var existing = await Get(savedVacancy.CandidateId, savedVacancy.VacancyId, null);

            if (existing == null)
            {
                var newEntity = new SavedVacancyEntity
                {
                    Id = Guid.NewGuid(),
                    CandidateId = savedVacancy.CandidateId,
                    VacancyReference = savedVacancy.VacancyReference,
                    VacancyId = savedVacancy.VacancyId,
                    CreatedOn = savedVacancy.CreatedOn
                };

                dataContext.SavedVacancyEntities.Add(newEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<Domain.Candidate.SavedVacancy, bool>(newEntity, true);
            }

            return new Tuple<Domain.Candidate.SavedVacancy, bool>(existing, false);
        }
    }
}
