using SFA.DAS.CandidateAccount.Domain.Candidate;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CandidateAccount.Data.Address;
public interface IAddressRepository
{
    Task<AddressEntity> Upsert(AddressEntity addressEntity);
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

    public async Task<AddressEntity> Upsert(AddressEntity addressEntity)
    {
        var existingAddress = dataContext.AddressEntities.Where(x => x.CandidateId == addressEntity.CandidateId).SingleOrDefault();

        if (existingAddress != null)
        {
            existingAddress.AddressLine1 = addressEntity.AddressLine1;
            existingAddress.AddressLine2 = addressEntity.AddressLine2;
            existingAddress.Town = addressEntity.Town;
            existingAddress.Postcode = addressEntity.Postcode;
            existingAddress.County = addressEntity.County;
            existingAddress.Latitude = addressEntity.Latitude;
            existingAddress.Longitude = addressEntity.Longitude;

            dataContext.AddressEntities.Update(existingAddress);
            await dataContext.SaveChangesAsync();
        }
        else
        {
            await dataContext.AddressEntities.AddAsync(addressEntity);
            await dataContext.SaveChangesAsync();
        }

        return addressEntity;
    }
}