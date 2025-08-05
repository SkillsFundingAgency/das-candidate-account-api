using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Qualification;

public interface IQualificationRepository
{
    Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualifications(Guid candidateId, Guid applicationId);
    Task<QualificationEntity?> GetCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id);
    Task DeleteCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id);
    Task DeleteCandidateApplicationQualificationsByReferenceId(Guid candidateId, Guid applicationId, Guid qualificationReferenceId);
    Task<Tuple<QualificationEntity, bool>> Upsert(Domain.Application.Qualification qualificationEntity, Guid candidateId, Guid applicationId);
    Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualificationsByQualificationReferenceType(Guid candidateId, Guid applicationId, Guid qualificationReferenceId);
    Task DeleteAllAsync(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);
}
public class QualificationRepository(ICandidateAccountDataContext dataContext) : IQualificationRepository
{
    private const int MaximumItems = 150;

    public async Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualifications(Guid candidateId, Guid applicationId)
    {
        return await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c=> c.ApplicationEntity.CandidateId == candidateId)
            .Include(c=>c.QualificationReferenceEntity)
            .OrderBy(c => c.QualificationOrder)
            .ThenBy(c => c.CreatedDate)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualificationsByQualificationReferenceType(Guid candidateId, Guid applicationId, Guid qualificationReferenceId)
    {
        return await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c=> c.ApplicationEntity.CandidateId == candidateId)
            .Where(c=>c.QualificationReferenceId == qualificationReferenceId)
            .Include(c=>c.QualificationReferenceEntity)
            .OrderBy(c => c.QualificationOrder)
            .ThenBy(c => c.CreatedDate)
            .ToListAsync();
    }

    public async Task DeleteAllAsync(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
    {
        var records = await dataContext
            .QualificationEntities
            .Where(x => x.ApplicationId == applicationId && x.ApplicationEntity.CandidateId == candidateId)
            .ToListAsync(cancellationToken);

        if (records is not { Count: > 0 })
        {
            return;
        }

        dataContext.QualificationEntities.RemoveRange(records);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<QualificationEntity?> GetCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id)
    {
        return await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c=> c.ApplicationEntity.CandidateId == candidateId)
            .Where(c=> c.Id == id)
            .Include(c=>c.QualificationReferenceEntity)
            .SingleOrDefaultAsync();
    }

    public async Task DeleteCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id)
    {
        var result = await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c => c.ApplicationEntity.CandidateId == candidateId)
            .Where(c => c.Id == id)
            .SingleOrDefaultAsync();

        if (result != null)
        {
            dataContext.QualificationEntities.Remove(result);
            await dataContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCandidateApplicationQualificationsByReferenceId(Guid candidateId, Guid applicationId, Guid qualificationReferenceId)
    {
        var result = await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c => c.ApplicationEntity.CandidateId == candidateId)
            .Where(c => c.QualificationReferenceId == qualificationReferenceId)
            .ToListAsync();
        
        if (result.Count == 0)
        {
            throw new InvalidOperationException("No qualifications found for deletion");
        }

        if (result.Count > 0)
        {
            dataContext.QualificationEntities.RemoveRange(result);
            await dataContext.SaveChangesAsync();
        }
    }

    public async Task<Tuple<QualificationEntity, bool>> Upsert(Domain.Application.Qualification qualificationEntity, Guid candidateId, Guid applicationId)
    {
        var existingQualification = await dataContext
            .QualificationEntities
            .FirstOrDefaultAsync(c => c.Id == qualificationEntity.Id 
                                      && c.ApplicationId == applicationId
                                      && c.ApplicationEntity.CandidateId == candidateId
                                      );

        if (existingQualification == null)
        {
            var itemCount = await dataContext.QualificationEntities
                .Where(fil => fil.ApplicationId == applicationId)
                .Where(fil => fil.QualificationReferenceId == qualificationEntity.QualificationReference.Id)
                .CountAsync();

            if (itemCount >= MaximumItems)
            {
                throw new InvalidOperationException($"Cannot insert a new qualification for application {applicationId}; maximum reached.");
            }

            var newQualification = (QualificationEntity)qualificationEntity;
            newQualification.ApplicationId = applicationId;
            await dataContext.QualificationEntities.AddAsync(newQualification);
            await dataContext.SaveChangesAsync();
            return new Tuple<QualificationEntity, bool>(newQualification, true);
        }
        
        existingQualification.Grade = qualificationEntity.Grade ?? existingQualification.Grade;
        existingQualification.IsPredicted = qualificationEntity.IsPredicted ?? existingQualification.IsPredicted;
        existingQualification.Subject = qualificationEntity.Subject ?? existingQualification.Subject;
        existingQualification.AdditionalInformation = qualificationEntity.AdditionalInformation ?? existingQualification.AdditionalInformation;
        existingQualification.ToYear = qualificationEntity.ToYear ?? existingQualification.ToYear;
        existingQualification.QualificationOrder = qualificationEntity.QualificationOrder ?? existingQualification.QualificationOrder;
        dataContext.QualificationEntities.Update(existingQualification);
        await dataContext.SaveChangesAsync();
    
        return new Tuple<QualificationEntity, bool>(existingQualification, false);
    }
}