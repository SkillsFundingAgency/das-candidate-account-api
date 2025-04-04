﻿using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Data.Candidate;
public interface ICandidateRepository
{
    Task<Tuple<CandidateEntity, bool>> Insert(CandidateEntity candidate);
    Task<CandidateEntity?> GetCandidateByEmail(string email);
    Task<CandidateEntity?> GetById(Guid id);
    Task<CandidateEntity?> GetByGovIdentifier(string id);
    Task<Tuple<CandidateEntity,bool>> UpsertCandidate(Domain.Candidate.Candidate candidate);
    Task<Tuple<CandidateEntity?, bool>> DeleteCandidate(Guid id);
    Task<PaginatedList<CandidateEntity>> GetCandidatesByActivity(DateTime cutOffDateTime, int pageNumber, int pageSize,
        CancellationToken token);
}
public class CandidateRepository(ICandidateAccountDataContext dataContext) : ICandidateRepository
{
    public async Task<Tuple<CandidateEntity, bool>> Insert(CandidateEntity candidate)
    {
        var existingCandidate = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => 
                c.GovUkIdentifier == candidate.GovUkIdentifier);

        if (existingCandidate != null)
        {
            existingCandidate.UpdatedOn = DateTime.UtcNow;
            dataContext.CandidateEntities.Update(candidate);
            await dataContext.SaveChangesAsync();

            return new Tuple<CandidateEntity, bool>(existingCandidate, false);
        }

        await dataContext.CandidateEntities.AddAsync(candidate);
        await dataContext.SaveChangesAsync();
        return new Tuple<CandidateEntity, bool>(candidate, true); ;
    }

    public async Task<CandidateEntity?> GetCandidateByEmail(string email)
    {
        var result = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => 
                c.Email == email && 
                c.Status != (short)CandidateStatus.Deleted);

        return result;
    }

    public async Task<CandidateEntity?> GetById(Guid id)
    {
        var result = await dataContext
            .CandidateEntities
                .Include(c=>c.Address)
            .FirstOrDefaultAsync(c => c.Id == id);

        return result;
    }

    public async Task<Tuple<CandidateEntity?, bool>> DeleteCandidate(Guid id)
    {
        var candidate = await dataContext
            .CandidateEntities
            .FirstOrDefaultAsync(c => c.Id == id);

        if (candidate is null) return new Tuple<CandidateEntity?, bool>(null, false);

        candidate.Status = (short)CandidateStatus.Deleted;
        candidate.GovUkIdentifier = null;
        candidate.UpdatedOn = DateTime.UtcNow;
        dataContext.CandidateEntities.Update(candidate);
        await dataContext.SaveChangesAsync();

        return new Tuple<CandidateEntity?, bool>(candidate, true);
    }

    public async Task<PaginatedList<CandidateEntity>> GetCandidatesByActivity(DateTime cutOffDateTime,
        int pageNumber,
        int pageSize,
        CancellationToken token)
    {
        // Query
        var query = dataContext
            .CandidateEntities
            .AsNoTracking()
            .Where(fil => 
                fil.UpdatedOn < cutOffDateTime && 
                fil.Status == (short)CandidateStatus.Completed)
            .OrderByDescending(fil => fil.UpdatedOn);

        // Count
        var count = await query.CountAsync(token);

        // Pagination
        query = (IOrderedQueryable<CandidateEntity>)query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await PaginatedList<CandidateEntity>.CreateAsync(query, count, pageNumber, pageSize);
    }

    public async Task<CandidateEntity?> GetByGovIdentifier(string id)
    {
        var result = await dataContext
            .CandidateEntities
                .Include(c => c.Address)
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

