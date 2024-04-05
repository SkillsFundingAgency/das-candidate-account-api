using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate;
public interface ICandidateRepository
{
    Task<CandidateEntity> Insert(CandidateEntity candidate);
    Task<CandidateEntity?> GetCandidateByEmail(string email);
    Task<CandidateEntity?> GetById(Guid id);
    Task<CandidateEntity?> GetByGovIdentifier(string id);
    Task<Tuple<CandidateEntity,bool>> UpsertCandidate(Domain.Candidate.Candidate candidate);
}
public class CandidateRepository(ICandidateAccountDataContext dataContext) : ICandidateRepository
{
    public async Task<CandidateEntity> Insert(CandidateEntity candidate)
    {
        await dataContext.CandidateEntities.AddAsync(candidate);

        await dataContext.SaveChangesAsync();
        return candidate;
    }

    public async Task<CandidateEntity?> GetCandidateByEmail(string email)
    {
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.Email == email);

        return result;
    }

    public async Task<CandidateEntity?> GetById(Guid id)
    {
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.Id == id);

        return result;
    }

    public async Task<CandidateEntity?> GetByGovIdentifier(string id)
    {
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.GovUkIdentifier == id);

        return result;
    }

    public async Task<Tuple<CandidateEntity,bool>> UpsertCandidate(Domain.Candidate.Candidate candidate)
    {
        var existingCandidate = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.GovUkIdentifier == candidate.GovUkIdentifier);

        if (existingCandidate == null)
        {
            var newCandidate = (CandidateEntity)candidate;
            newCandidate.CreatedOn = DateTime.UtcNow;
            newCandidate.UpdatedOn = null;
            await dataContext.CandidateEntities.AddAsync(newCandidate);
            await dataContext.SaveChangesAsync();
            return new Tuple<CandidateEntity, bool>(newCandidate, true);
        }
        
        existingCandidate.FirstName = candidate.FirstName ?? existingCandidate.FirstName;
        existingCandidate.LastName = candidate.LastName ?? existingCandidate.LastName;
        existingCandidate.Email = candidate.Email;
        existingCandidate.UpdatedOn = DateTime.UtcNow;
        existingCandidate.DateOfBirth = candidate.DateOfBirth ?? existingCandidate.DateOfBirth;
        existingCandidate.PhoneNumber = candidate.PhoneNumber ?? existingCandidate.PhoneNumber;
        existingCandidate.Status = candidate.Status.HasValue ? (short)candidate.Status : existingCandidate.Status;
        dataContext.CandidateEntities.Update(existingCandidate);
        await dataContext.SaveChangesAsync();
    
        return new Tuple<CandidateEntity, bool>(existingCandidate, false);
    }
}

