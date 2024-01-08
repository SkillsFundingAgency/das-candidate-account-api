using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Repository;
public interface ICandidateRepository
{
    Task Insert(Domain.Candidate.CandidateEntity candidate);
    Task<Domain.Candidate.CandidateEntity> GetCandidateByEmail(string email);
    Task UpdateCandidateByEmail(Domain.Candidate.CandidateEntity candidate);
}
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

    public async Task UpdateCandidateByEmail(Domain.Candidate.CandidateEntity candidate)
    {
        var existingCandidate = await _dataContext.CandidateEntities.FirstOrDefaultAsync(c => c.Email.Equals(candidate.Email));

        if (existingCandidate != null)
        {
            candidate.Id = existingCandidate.Id;
            _dataContext.CandidateEntities.Update(candidate);
            await _dataContext.SaveChangesAsync();
        }
    }
}

