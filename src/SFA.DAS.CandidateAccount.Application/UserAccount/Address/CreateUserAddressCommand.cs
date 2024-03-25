using MediatR;

namespace SFA.DAS.CandidateAccount.Application.UserAccount.Address;
public class CreateUserAddressCommand : IRequest<CreateUserAddressCommandResult>
{
    public Guid CandidateId { get; set; }
    public string Email { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string AddressLine4 { get; set; }
    public string Postcode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
