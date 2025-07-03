namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public sealed record GetAllApplicationsByIdApiRequest
    {
        public required List<Guid> ApplicationIds { get; init; } = [];
        public bool IncludeDetails { get; init; } = false;
    }
}