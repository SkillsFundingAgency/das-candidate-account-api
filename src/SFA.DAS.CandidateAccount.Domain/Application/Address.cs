using Newtonsoft.Json;

namespace SFA.DAS.CandidateAccount.Domain.Application
{
    public record Address(
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
            return JsonConvert.DeserializeObject<List<Address>>(source);
        }
    }
}