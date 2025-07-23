namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public record Location
    {
        public List<Address>? Addresses { get; set; } = null;
        public AvailableWhere EmployerLocationOption { get; set; }
        public string? EmploymentLocationInformation { get; set; }
    }
}