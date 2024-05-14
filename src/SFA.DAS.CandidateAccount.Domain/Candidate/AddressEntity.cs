namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AddressEntity
    {
        public required Guid Id { get; set; }
        public string? Uprn { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string Town { get; set; } = null!;
        public string? County { get; set; }
        public string Postcode { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required Guid CandidateId { get; set; }
        
        public virtual CandidateEntity Candidate { get; set; }
    }
}
