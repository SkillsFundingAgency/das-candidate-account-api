using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Address;

public interface IAddressRepository
{
    Task<Domain.Candidate.Address?> Get(Guid candidateId);
}
public class AddressRepository(ICandidateAccountDataContext dataContext) : IAddressRepository
{
    public async Task<Domain.Candidate.Address?> Get(Guid candidateId)
    {
        var query = from item in dataContext.AddressEntities
                .Where(tc => tc.CandidateId == candidateId)
                    select item;

        var addressEntity = await query.SingleOrDefaultAsync();

        return addressEntity is null ? null : (Domain.Candidate.Address)addressEntity;
    }
}