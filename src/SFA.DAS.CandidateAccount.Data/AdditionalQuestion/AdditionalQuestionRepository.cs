using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.AdditionalQuestion;

public interface IAdditionalQuestionRepository
{
    Task<AdditionalQuestionEntity?> Get(Guid applicationId, Guid candidateId, Guid id, CancellationToken cancellationToken);
    Task<List<AdditionalQuestionEntity>> GetAll(Guid applicationId, Guid candidateId, CancellationToken cancellationToken);
    Task<Tuple<AdditionalQuestionEntity, bool>> UpsertAdditionalQuestion(Domain.Application.AdditionalQuestion additionalQuestion, Guid candidateId);
}

public class AdditionalQuestionRepository(ICandidateAccountDataContext dataContext) : IAdditionalQuestionRepository
{
    public async Task<AdditionalQuestionEntity?> Get(Guid applicationId, Guid candidateId, Guid id, CancellationToken cancellationToken)
    {
        var query = from question in dataContext.AdditionalQuestionEntities
                .Where(fil => fil.ApplicationId == applicationId)
                .Where(fil => fil.Id == id)
            join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                on question.ApplicationId equals application.Id
            select question;

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<AdditionalQuestionEntity>> GetAll(Guid applicationId, Guid candidateId, CancellationToken cancellationToken)
    {
        var query = from question in dataContext.AdditionalQuestionEntities
                .Where(fil => fil.ApplicationId == applicationId)
            join application in dataContext.ApplicationEntities
                    .Where(fil => fil.CandidateId == candidateId && fil.Id == applicationId)
                on question.ApplicationId equals application.Id
            select question;

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Tuple<AdditionalQuestionEntity, bool>> UpsertAdditionalQuestion(Domain.Application.AdditionalQuestion additionalQuestion, Guid candidateId)
    {
        var query = from question in dataContext.AdditionalQuestionEntities
                .Where(fil => fil.ApplicationId == additionalQuestion.ApplicationId)
                .Where(fil => fil.QuestionId == additionalQuestion.QuestionId)
            join application in dataContext.ApplicationEntities
                    .Where(fil => fil.CandidateId == candidateId && fil.Id == additionalQuestion.ApplicationId)
                on question.ApplicationId equals application.Id
            select question;

        var additionalQuestionEntity = await query.SingleOrDefaultAsync();

        if (additionalQuestionEntity == null)
        {
            await dataContext.AdditionalQuestionEntities.AddAsync(additionalQuestion);
            await dataContext.SaveChangesAsync();
            return new Tuple<AdditionalQuestionEntity, bool>(additionalQuestion, true);
        }

        additionalQuestionEntity.Answer = additionalQuestion.Answer;

        dataContext.AdditionalQuestionEntities.Update(additionalQuestionEntity);

        await dataContext.SaveChangesAsync();
        return new Tuple<AdditionalQuestionEntity, bool>(additionalQuestionEntity, false);
    }
}