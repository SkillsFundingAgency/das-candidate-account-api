using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.ReferenceData;

public interface IQualificationReferenceRepository
{
    Task<IEnumerable<QualificationReferenceEntity>> GetAll();
    Task<QualificationReferenceEntity?> GetById(Guid qualificationReferenceId);
}
public class QualificationReferenceRepository(ICandidateAccountDataContext context) : IQualificationReferenceRepository
{
    public async Task<IEnumerable<QualificationReferenceEntity>> GetAll()
    {
        return await context.QualificationReferenceEntities.ToListAsync();
    }

    public async Task<QualificationReferenceEntity?> GetById(Guid qualificationReferenceId)
    {
        return await context.QualificationReferenceEntities.FindAsync(qualificationReferenceId);
    }
}