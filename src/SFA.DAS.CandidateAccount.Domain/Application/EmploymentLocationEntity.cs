using Newtonsoft.Json;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public class EmploymentLocationEntity
    {
        public Guid Id { get; set; }
        public string Addresses { get; set; }
        public short EmployerLocationOption { get; set; }
        public string? EmploymentLocationInformation { get; set; }
        public Guid ApplicationId { get; set; }

        public virtual ApplicationEntity ApplicationEntity { get; set; }

        public static implicit operator EmploymentLocationEntity(EmploymentLocation source)
        {
            return new EmploymentLocationEntity
            {
                Addresses = Address.ToJson(source.Addresses),
                EmployerLocationOption = source.EmployerLocationOption,
                EmploymentLocationInformation = source.EmploymentLocationInformation,
                ApplicationId = source.ApplicationId,
                Id = source.Id
            };
        }
    }
}