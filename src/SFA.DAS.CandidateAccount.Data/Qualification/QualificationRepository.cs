using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.Qualification;

public interface IQualificationRepository
{
    Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualifications(Guid candidateId, Guid applicationId);
    Task<QualificationEntity?> GetCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id);
    Task DeleteCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id);
    Task<Tuple<QualificationEntity, bool>> Upsert(Domain.Application.Qualification qualificationEntity);
}
public class QualificationRepository(ICandidateAccountDataContext dataContext) : IQualificationRepository
{
    public async Task<IEnumerable<QualificationEntity>> GetCandidateApplicationQualifications(Guid candidateId, Guid applicationId)
    {
        return await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c=> c.ApplicationEntity.CandidateId == candidateId)
            .ToListAsync();
    }

    public async Task<QualificationEntity?> GetCandidateApplicationQualificationById(Guid candidateId, Guid applicationId, Guid id)
    {
        return await dataContext.QualificationEntities
            .Where(c => c.ApplicationId == applicationId)
            .Where(c=> c.ApplicationEntity.CandidateId == candidateId)
            .Where(c=> c.Id == id)
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

    public async Task<Tuple<QualificationEntity, bool>> Upsert(Domain.Application.Qualification qualificationEntity)
    {
        var existingQualification = await dataContext
            .QualificationEntities
            .FirstOrDefaultAsync(c => c.Id == qualificationEntity.Id 
                                      && c.ApplicationId == qualificationEntity.Application.Id
                                      && c.ApplicationEntity.CandidateId == qualificationEntity.Application.CandidateId
                                      );

        if (existingQualification == null)
        {
            var newQualification = (QualificationEntity)qualificationEntity;
            
            await dataContext.QualificationEntities.AddAsync(newQualification);
            await dataContext.SaveChangesAsync();
            return new Tuple<QualificationEntity, bool>(newQualification, true);
        }
        
        existingQualification.Grade = qualificationEntity.Grade ?? existingQualification.Grade;
        existingQualification.IsPredicted = qualificationEntity.IsPredicted ?? existingQualification.IsPredicted;
        existingQualification.Subject = qualificationEntity.Subject ?? existingQualification.Subject;
        existingQualification.ToYear = qualificationEntity.ToYear ?? existingQualification.ToYear;
        dataContext.QualificationEntities.Update(existingQualification);
        await dataContext.SaveChangesAsync();
    
        return new Tuple<QualificationEntity, bool>(existingQualification, false);
    }
}