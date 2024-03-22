using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Data.Address
{
    public interface IAddressRepository
    {
        Task<AddressEntity> Create(AddressEntity addressEntity);
    }

    public class AddressRepository(ICandidateAccountDataContext dataContext) : IAddressRepository
    {
        public async Task<AddressEntity> Create(AddressEntity addressEntity)
        {
            await dataContext.AddressEntities.AddAsync(addressEntity);
            await dataContext.SaveChangesAsync();
            return addressEntity;
        }
    }
}
