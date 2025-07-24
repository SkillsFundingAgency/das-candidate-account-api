namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation
{
    public record UpsertEmploymentLocationCommandResponse
    {
        public required Domain.Application.EmploymentLocation EmploymentLocation { get; init; }
        public bool IsCreated { get; set; }
    }
}
