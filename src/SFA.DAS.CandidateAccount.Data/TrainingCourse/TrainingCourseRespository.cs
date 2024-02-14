using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.TrainingCourse
{
    public interface ITrainingCourseRespository
    {
        Task<Tuple<TrainingCourseEntity, bool>> UpsertTrainingCourse(Domain.Application.TrainingCourse trainingCourseEntity, Guid candidateId);
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

        public async Task<Tuple<TrainingCourseEntity, bool>> UpsertTrainingCourse(Domain.Application.TrainingCourse trainingCourseEntity, Guid candidateId)
        {
            var query = from course in dataContext.TrainingCourseEntities
                    .Where(tc => tc.Id == trainingCourseEntity.Id)
                    .Where(tc => tc.ApplicationId == trainingCourseEntity.ApplicationId)
                        join application in dataContext.ApplicationEntities.Where(app => app.CandidateId == candidateId && app.Id == trainingCourseEntity.ApplicationId)
                        on course.ApplicationId equals application.Id
                        select course;

            var trainingCourse = await query.SingleOrDefaultAsync();

            if (trainingCourse is null)
            {
                await dataContext.TrainingCourseEntities.AddAsync((TrainingCourseEntity)trainingCourseEntity);
                await dataContext.SaveChangesAsync();
                return new Tuple<TrainingCourseEntity, bool>(trainingCourseEntity, true);
            }

            trainingCourse.ToYear = trainingCourseEntity.ToYear;
            trainingCourse.Title = trainingCourseEntity.Title;
            dataContext.TrainingCourseEntities.Update(trainingCourse);

            await dataContext.SaveChangesAsync();
            return new Tuple<TrainingCourseEntity, bool>(trainingCourse, false);
        }
    }
}
