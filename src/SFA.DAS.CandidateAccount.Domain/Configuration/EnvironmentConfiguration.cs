namespace SFA.DAS.CandidateAccount.Domain.Configuration;

public class EnvironmentConfiguration(string environmentName)
{
    public string EnvironmentName { get;} = environmentName;
}