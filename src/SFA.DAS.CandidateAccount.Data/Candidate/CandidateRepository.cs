using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Candidate;
public interface ICandidateRepository
{
    Task<CandidateEntity> Insert(CandidateEntity candidate);
    Task<CandidateEntity?> GetCandidateByEmail(string email);
    Task<CandidateEntity?> GetById(Guid id);
    Task<CandidateEntity?> GetByGovIdentifier(string id);
    Task UpdateCandidateByEmail(CandidateEntity candidate);
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

    public async Task UpdateCandidateByEmail(CandidateEntity candidate)
    {
        var existingCandidate = await dataContext.CandidateEntities.FirstOrDefaultAsync(c => c.Email== candidate.Email);

        if (existingCandidate != null)
        {
            candidate.Id = existingCandidate.Id;
            dataContext.CandidateEntities.Update(candidate);
            await dataContext.SaveChangesAsync();
        }
    }
    // public async Task<Tuple<CandidateEntity,bool>> Upsert(CandidateEntity candidate)
    // {
    //     var candidateEntity = await dataContext.CandidateEntities.SingleOrDefaultAsync(c =>
    //         c.Email== candidate.Email);
    //
    //     if (candidateEntity == null)
    //     {
    //         await dataContext.CandidateEntities.AddAsync(candidate);
    //         await dataContext.SaveChangesAsync();
    //         return new Tuple<CandidateEntity, bool>(candidate, true);    
    //     }
    //     
    //     candidateEntity.FirstName = candidate.FirstName;
    //     candidateEntity.LastName = candidate.LastName;
    //     candidateEntity.GovUkIdentifier = candidate.GovUkIdentifier;
    //     
    //     await dataContext.SaveChangesAsync();
    //     
    //     return new Tuple<CandidateEntity, bool>(candidateEntity, false);
    // }
}

