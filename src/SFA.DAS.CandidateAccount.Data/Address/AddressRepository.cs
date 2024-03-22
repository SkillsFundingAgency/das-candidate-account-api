using SFA.DAS.CandidateAccount.Domain.Candidate;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Address;

namespace SFA.DAS.CandidateAccount.Data.Address
{
    public interface IAddressRepository
    {
        Task<AddressEntity> Create(AddressEntity addressEntity);
        Task<Domain.Candidate.Address?> Get(Guid candidateId);
    }

    public class AddressRepository(ICandidateAccountDataContext dataContext) : IAddressRepository
    {
        public async Task<AddressEntity> Create(AddressEntity addressEntity)
    {
        var query = from item in dataContext.AddressEntities
                .Where(tc => tc.CandidateId == candidateId)
                    select item;

        var addressEntity = await query.SingleOrDefaultAsync();

        return addressEntity is null ? null : (Domain.Candidate.Address)addressEntity;
    }
    
            public async Task<AddressEntity> Create(AddressEntity addressEntity)
        {
            await dataContext.AddressEntities.AddAsync(addressEntity);
            await dataContext.SaveChangesAsync();
            return addressEntity;
        }
}