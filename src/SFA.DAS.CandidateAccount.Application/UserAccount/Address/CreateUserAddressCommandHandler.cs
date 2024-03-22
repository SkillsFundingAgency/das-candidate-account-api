using MediatR;
using SFA.DAS.CandidateAccount.Data.Address;
using SFA.DAS.CandidateAccount.Data.Candidate;

namespace SFA.DAS.CandidateAccount.Application.UserAccount.Address;
public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, CreateUserAddressCommandResult>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IAddressRepository _addressRepository;

    public CreateUserAddressCommandHandler(ICandidateRepository candidateRepository, IAddressRepository addressRepository)
    {
        _candidateRepository = candidateRepository;
        _addressRepository = addressRepository;
    }

    public async Task<CreateUserAddressCommandResult> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByGovIdentifier(request.GovUkIdentifier) ?? throw new InvalidOperationException();

        var result = await _addressRepository.Create(new Domain.Candidate.AddressEntity()
        {
            Id = Guid.NewGuid(),
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            AddressLine3 = request.AddressLine3,
            AddressLine4 = request.AddressLine4,
            Postcode = request.Postcode,
            Uprn = request.Uprn,
            CandidateId = candidate.Id
        });

        return new CreateUserAddressCommandResult()
        {
            Id = result.Id,
            CandidateId = result.CandidateId
        };
    }
}
