using Microsoft.EntityFrameworkCore;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.AdditionalQuestion;

public interface IAdditionalQuestionRepository
{
    Task<Tuple<AdditionalQuestionEntity, bool>> UpsertAdditionalQuestion(Domain.Application.AdditionalQuestion additionalQuestion, Guid candidateId);
}

public class AdditionalQuestionRepository(ICandidateAccountDataContext dataContext) : IAdditionalQuestionRepository
{
    public async Task<Tuple<AdditionalQuestionEntity, bool>> UpsertAdditionalQuestion(Domain.Application.AdditionalQuestion additionalQuestion, Guid candidateId)
    {
        var query = from question in dataContext.AdditionalQuestionEntities
                .Where(fil => fil.ApplicationId == additionalQuestion.ApplicationId)
                .Where(fil => fil.Id == additionalQuestion.Id)
            join application in dataContext.ApplicationEntities.Where(fil => fil.CandidateId == candidateId && fil.Id == additionalQuestion.ApplicationId)
                on question.ApplicationId equals application.Id
            select question;

        var additionalQuestionEntity = await query.SingleOrDefaultAsync();

        if (additionalQuestionEntity == null)
        {
            await dataContext.AdditionalQuestionEntities.AddAsync((AdditionalQuestionEntity)additionalQuestion);
            await dataContext.SaveChangesAsync();
            return new Tuple<AdditionalQuestionEntity, bool>(additionalQuestion, true);
        }

        additionalQuestionEntity.Answer = additionalQuestion.Answer;

        dataContext.AdditionalQuestionEntities.Update(additionalQuestionEntity);

        await dataContext.SaveChangesAsync();
        return new Tuple<AdditionalQuestionEntity, bool>(additionalQuestionEntity, false);
    }
}