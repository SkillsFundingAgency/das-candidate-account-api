using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Candidate;
public interface ICandidateRepository
{
    Task Insert(Domain.Candidate.CandidateEntity candidate);
    Task<Domain.Candidate.CandidateEntity> GetCandidateByEmail(string email);
    Task UpdateCandidateByEmail(Domain.Candidate.CandidateEntity candidate);
}
public class CandidateRepository(ICandidateAccountDataContext dataContext) : ICandidateRepository
{
    public async Task Insert(Domain.Candidate.CandidateEntity candidate)
    {
        await dataContext.CandidateEntities.AddAsync(candidate);

        await dataContext.SaveChangesAsync();
    }

    public async Task<Domain.Candidate.CandidateEntity> GetCandidateByEmail(string email)
    {
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.Email.Equals(email));

        return result;
    }

    public async Task UpdateCandidateByEmail(Domain.Candidate.CandidateEntity candidate)
    {
        var existingCandidate = await dataContext.CandidateEntities.FirstOrDefaultAsync(c => c.Email.Equals(candidate.Email));

        if (existingCandidate != null)
        {
            candidate.Id = existingCandidate.Id;
            dataContext.CandidateEntities.Update(candidate);
            await dataContext.SaveChangesAsync();
        }
    }
}

