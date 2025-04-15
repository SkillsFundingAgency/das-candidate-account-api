using Newtonsoft.Json;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public record Address(
        Guid Id,
        string FullAddress,
        bool IsSelected,
        short AddressOrder)
    {
        public static string ToJson(List<Address> source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static List<Address>? ToList(string source)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Address>>(source);
            }
            catch (JsonException ex)
            {
                // Log the exception or handle it as needed
                return null;
            }
        }
    }
}