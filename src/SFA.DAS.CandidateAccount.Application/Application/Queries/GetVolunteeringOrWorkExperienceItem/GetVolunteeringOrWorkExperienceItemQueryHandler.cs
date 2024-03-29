﻿using MediatR;
using SFA.DAS.CandidateAccount.Data.WorkExperience;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetVolunteeringOrWorkExperienceItem;
public record GetVolunteeringOrWorkExperienceItemQueryHandler(IWorkHistoryRepository WorkHistoryRepository) : IRequestHandler<GetVolunteeringOrWorkExperienceItemQuery, GetVolunteeringOrWorkExperienceItemQueryResult?>
{
    public async Task<GetVolunteeringOrWorkExperienceItemQueryResult?> Handle(GetVolunteeringOrWorkExperienceItemQuery request, CancellationToken cancellationToken)
    {
        var result = await WorkHistoryRepository.Get(request.ApplicationId, request.CandidateId, request.Id, Domain.Application.WorkHistoryType.WorkExperience, cancellationToken);
        return result == null ? null : (GetVolunteeringOrWorkExperienceItemQueryResult)result;
    }
}
