using MediatR;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourseItem;
public class GetTrainingCourseItemQueryHandler(ITrainingCourseRepository TrainingCourseRespository) : IRequestHandler<GetTrainingCourseItemQuery, GetTrainingCourseItemQueryResult?>
{
    public async Task<GetTrainingCourseItemQueryResult?> Handle(GetTrainingCourseItemQuery request, CancellationToken cancellationToken)
    {
        var result = await TrainingCourseRespository.Get(request.ApplicationId, request.CandidateId, request.Id, cancellationToken);
        return result == null ? null : (GetTrainingCourseItemQueryResult) result;
    }
}
