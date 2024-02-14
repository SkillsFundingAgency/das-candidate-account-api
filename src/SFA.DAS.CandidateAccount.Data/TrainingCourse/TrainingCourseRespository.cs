﻿using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.TrainingCourse
{
    public interface ITrainingCourseRespository
    {
        Task Update(TrainingCourseEntity trainingCourseEntity);
        Task<Tuple<TrainingCourseEntity, bool>> UpsertTrainingCourse(Domain.Application.TrainingCourse trainingCourseEntity);
        Task<TrainingCourseEntity?> Get(Guid applicationId, Guid candidateId, Guid id, CancellationToken cancellationToken);
        Task<List<TrainingCourseEntity>> GetAll(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);
    }

    public class TrainingCourseRespository(ICandidateAccountDataContext dataContext) : ITrainingCourseRespository
    {
        public async Task<TrainingCourseEntity?> Get(Guid applicationId, Guid candidateId, Guid id, CancellationToken cancellationToken)
        {
            var query = from course in dataContext.TrainingCourseEntities
                    .Where(fil => fil.ApplicationId == applicationId)
                    .Where(fil => fil.Id == id)
                    .OrderBy(a => a.ToYear)
                        join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                            on course.ApplicationId equals application.Id
                        select course;

            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TrainingCourseEntity>> GetAll(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
        {
            var query = from course in dataContext.TrainingCourseEntities
                    .Where(fil => fil.ApplicationId == applicationId)
                    .OrderBy(a => a.ToYear)
                        join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                            on course.ApplicationId equals application.Id
                        select course;

            return await query.ToListAsync(cancellationToken);
        }

        public async Task Update(TrainingCourseEntity trainingCourseEntity)
        {
            var entity = await dataContext.TrainingCourseEntities.SingleAsync(x => x.Id == trainingCourseEntity.Id && x.ApplicationId == trainingCourseEntity.ApplicationId);

            entity.ToYear = trainingCourseEntity.ToYear;
            entity.Title = trainingCourseEntity.Title;

            await dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<TrainingCourseEntity, bool>> UpsertTrainingCourse(Domain.Application.TrainingCourse trainingCourseEntity)
        {
            var entity = await dataContext.TrainingCourseEntities
                .SingleOrDefaultAsync(x => x.Id == trainingCourseEntity.Id
                && x.ApplicationId == trainingCourseEntity.ApplicationId);

            if (entity is null)
            {
                await dataContext.TrainingCourseEntities.AddAsync((TrainingCourseEntity)trainingCourseEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<TrainingCourseEntity, bool>(trainingCourseEntity, true);
            }

            entity.ToYear = trainingCourseEntity.ToYear;
            entity.Title = trainingCourseEntity.Title;
            dataContext.TrainingCourseEntities.Update(entity);

            await dataContext.SaveChangesAsync();
            return new Tuple<TrainingCourseEntity, bool>(entity, false);
        }
    }
}
