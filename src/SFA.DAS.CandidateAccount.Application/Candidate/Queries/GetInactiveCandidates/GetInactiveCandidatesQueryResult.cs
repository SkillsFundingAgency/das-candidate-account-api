﻿using SFA.DAS.CandidateAccount.Domain.Models;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetInactiveCandidates
{
    public record GetInactiveCandidatesQueryResult
    {
        public List<Domain.Candidate.Candidate> Candidates { get; private init; } = [];
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public static implicit operator GetInactiveCandidatesQueryResult(PaginatedList<Domain.Candidate.CandidateEntity> source)
        {
            return new GetInactiveCandidatesQueryResult
            {
                TotalCount = source.TotalCount,
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalPages = source.TotalPages,
                Candidates = source.Items.Select(candidate => (Domain.Candidate.Candidate)candidate).ToList()
            };
        }
    }
}