using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Api.ApiRequests
{
    public record PutEmploymentLocationApiRequest
    {
        public List<Address> Addresses { get; set; } = [];
        public short EmployerLocationOption { get; set; }
        public string? EmploymentLocationInformation { get; set; }
    }
}