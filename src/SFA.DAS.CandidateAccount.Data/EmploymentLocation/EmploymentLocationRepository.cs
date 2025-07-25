﻿using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.EmploymentLocation
{
    public interface IEmploymentLocationRepository
    {
        Task<EmploymentLocationEntity?> Get(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);
        Task<Tuple<EmploymentLocationEntity, bool>> UpsertEmploymentLocation(EmploymentLocationEntity employmentLocation, Guid candidateId, CancellationToken token = default);
    }

    public class EmploymentLocationRepository(ICandidateAccountDataContext dataContext) : IEmploymentLocationRepository
    {
        public async Task<EmploymentLocationEntity?> Get(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
        {
            var query = from location in dataContext.EmploymentLocationEntities
                    .Where(fil => fil.ApplicationId == applicationId)
                join application in dataContext.ApplicationEntities
                        .Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                    on location.ApplicationId equals application.Id
                select location;

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Tuple<EmploymentLocationEntity, bool>> UpsertEmploymentLocation(EmploymentLocationEntity employmentLocation, Guid candidateId, CancellationToken token = default)
        {
            var query = from location in dataContext.EmploymentLocationEntities.AsNoTracking()
                    .Where(fil => fil.ApplicationId == employmentLocation.ApplicationId)
                    .Where(fil => fil.Id == employmentLocation.Id)
                join application in dataContext.ApplicationEntities.AsNoTracking()
                        .Where(fil => fil.CandidateId == candidateId && fil.Id == employmentLocation.ApplicationId)
                    on location.ApplicationId equals application.Id
                select location;

            var employmentLocationEntity = await query.SingleOrDefaultAsync(cancellationToken: token);

            if (employmentLocationEntity == null)
            {
                await dataContext.EmploymentLocationEntities.AddAsync(employmentLocation, token);
                await dataContext.SaveChangesAsync(token);
                return new Tuple<EmploymentLocationEntity, bool>(employmentLocation, true);
            }

            employmentLocationEntity.Addresses = employmentLocation.Addresses;

            dataContext.EmploymentLocationEntities.Update(employmentLocation);

            await dataContext.SaveChangesAsync(token);
            return new Tuple<EmploymentLocationEntity, bool>(employmentLocationEntity, false);
        }
    }
}
