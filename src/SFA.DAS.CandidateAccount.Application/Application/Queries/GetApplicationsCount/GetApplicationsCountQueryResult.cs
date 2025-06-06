﻿using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplicationsCount
{
    public record GetApplicationsCountQueryResult
    {
            public List<Guid> ApplicationIds { get; set; } = [];
            public ApplicationStatus Status { get; set; }
            public int Count { get; set; }
    }
}