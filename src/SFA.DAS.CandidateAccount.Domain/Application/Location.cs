namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public record Location
    {
        public List<Address>? Addresses { get; set; } = null;
        public short EmployerLocationOption { get; set; }
        public string? EmploymentLocationInformation { get; set; }
    }
}