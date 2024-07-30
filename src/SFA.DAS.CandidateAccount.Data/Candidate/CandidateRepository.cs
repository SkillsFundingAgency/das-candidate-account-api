using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate;
public interface ICandidateRepository
{
    Task<Tuple<CandidateEntity, bool>> Insert(CandidateEntity candidate);
    Task<CandidateEntity?> GetCandidateByEmail(string email);
    Task<CandidateEntity?> GetById(Guid id);
    Task<CandidateEntity?> GetByGovIdentifier(string id);
    Task<Tuple<CandidateEntity,bool>> UpsertCandidate(Domain.Candidate.Candidate candidate);
    Task<CandidateEntity?> GetByMigratedCandidateId(Guid? id);
    Task<CandidateEntity?> GetByMigratedCandidateEmail(string email);
}
public class CandidateRepository(ICandidateAccountDataContext dataContext) : ICandidateRepository
{
    public async Task<Tuple<CandidateEntity, bool>> Insert(CandidateEntity candidate)
    {
        var existingCandidate = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.GovUkIdentifier == candidate.GovUkIdentifier);

        if (existingCandidate != null) return new Tuple<CandidateEntity, bool>(existingCandidate, false);

        await dataContext.CandidateEntities.AddAsync(candidate);
        await dataContext.SaveChangesAsync();
        return new Tuple<CandidateEntity, bool>(candidate, true); ;
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

    public async Task<CandidateEntity?> GetByMigratedCandidateId(Guid? id)
    {
        if (!id.HasValue)
        {
            return null;
        }
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.MigratedCandidateId == id);

        return result;
    }

    public async Task<CandidateEntity?> GetByMigratedCandidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.MigratedEmail == email);

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
            .FirstOrDefaultAsync(c => c.Id == candidate.Id);

        //TODO look at why we are doing this - and not doing all the fields
        if (existingCandidate == null)
        {
            var newCandidate = (CandidateEntity)candidate;
            newCandidate.CreatedOn = DateTime.UtcNow;
            newCandidate.UpdatedOn = null;
            newCandidate.FirstName = candidate.FirstName;
            newCandidate.LastName = candidate.LastName;
            newCandidate.DateOfBirth = candidate.DateOfBirth;
            await dataContext.CandidateEntities.AddAsync(newCandidate);
            await dataContext.SaveChangesAsync();
            return new Tuple<CandidateEntity, bool>(newCandidate, true);
        }
        
        existingCandidate.FirstName = candidate.FirstName ?? existingCandidate.FirstName;
        existingCandidate.LastName = candidate.LastName ?? existingCandidate.LastName;
        existingCandidate.Email = candidate.Email ?? existingCandidate.Email;
        existingCandidate.UpdatedOn = DateTime.UtcNow;
        existingCandidate.DateOfBirth = candidate.DateOfBirth ?? existingCandidate.DateOfBirth;
        existingCandidate.PhoneNumber = candidate.PhoneNumber ?? existingCandidate.PhoneNumber;
        existingCandidate.Status = candidate.Status.HasValue ? (short)candidate.Status : existingCandidate.Status;
        existingCandidate.MigratedEmail = candidate.MigratedEmail ?? existingCandidate.MigratedEmail;
        existingCandidate.MigratedCandidateId = candidate.MigratedCandidateId ?? existingCandidate.MigratedCandidateId;
        dataContext.CandidateEntities.Update(existingCandidate);
        await dataContext.SaveChangesAsync();
    
        return new Tuple<CandidateEntity, bool>(existingCandidate, false);
    }
}

