using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ICandidateAccountDataContext _dataContext;

        public CandidateRepository(ICandidateAccountDataContext dataContext)
        {
                _dataContext = dataContext;
        }

        public async Task Insert(Domain.Candidate.CandidateEntity candidate)
        {
            await _dataContext.CandidateEntities.AddAsync(candidate);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<Domain.Candidate.CandidateEntity> GetCandidateByEmail(string email)
        {
            var result = await _dataContext
                .CandidateEntities
                .FirstOrDefaultAsync(c => c.Email.Equals(email));

            return result;
        }
    }
}
