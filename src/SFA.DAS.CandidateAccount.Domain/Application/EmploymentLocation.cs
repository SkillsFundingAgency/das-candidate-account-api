namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class EmploymentLocation
    {
        public Guid Id { get; set; }
        public List<Address>? Addresses { get; set; } = [];
        public short EmployerLocationOption { get; set; }
        public string? EmploymentLocationInformation { get; set; }
        public Guid ApplicationId { get; set; }

        public static implicit operator EmploymentLocation(EmploymentLocationEntity source)
        {
            return new EmploymentLocation
            {
                Addresses = Address.ToList(source.Addresses),
                EmployerLocationOption = source.EmployerLocationOption,
                EmploymentLocationInformation = source.EmploymentLocationInformation,
                ApplicationId = source.ApplicationId,
                Id = source.Id
            };
        }
    }
}