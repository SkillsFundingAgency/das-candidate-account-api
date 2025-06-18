using System.Text.Json.Serialization;

namespace SFA.DAS.CandidateAccount.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApprenticeshipTypes
{
    Standard,
    Foundation,
}