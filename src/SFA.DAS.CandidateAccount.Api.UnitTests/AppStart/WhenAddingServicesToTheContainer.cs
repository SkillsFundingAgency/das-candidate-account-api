using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SFA.DAS.CandidateAccount.Api.AppStart;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Configuration;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.AppStart;

public class WhenAddingServicesToTheContainer
{
    [TestCase(typeof(ICandidateRepository))]
    [TestCase(typeof(IApplicationTemplateRepository))]
    public void Then_The_Dependencies_Are_Correctly_Resolved(Type toResolve)
    {
        var serviceCollection = new ServiceCollection();
        SetupServiceCollection(serviceCollection);
        var provider = serviceCollection.BuildServiceProvider();

        var type = provider.GetService(toResolve);
        Assert.That(type, Is.Not.Null);
    }
    
    private static void SetupServiceCollection(ServiceCollection serviceCollection)
    {
        var configuration = GenerateConfiguration();
        var candidateAccountConfiguration = configuration
            .GetSection("CandidateAccountConfiguration")
            .Get<CandidateAccountConfiguration>();
            
        serviceCollection.AddSingleton(Mock.Of<IWebHostEnvironment>());
        serviceCollection.AddSingleton(Mock.Of<IConfiguration>());
        serviceCollection.AddConfigurationOptions(configuration);
        serviceCollection.AddDistributedMemoryCache();
        serviceCollection.AddServiceRegistration();
        serviceCollection.AddDatabaseRegistration(candidateAccountConfiguration,"DEV");
        
    }
    
    private static IConfigurationRoot GenerateConfiguration()
    {
        var configSource = new MemoryConfigurationSource
        {
            InitialData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("FindAnApprenticeshipOuterApi:BaseUrl", "https://test.com/"),
                new KeyValuePair<string, string>("FindAnApprenticeshipOuterApi:Key", "123edc"),
                new KeyValuePair<string, string>("EnvironmentName", "test"),
                new KeyValuePair<string, string>("ConnectionString", "test"),
            }
        };

        var provider = new MemoryConfigurationProvider(configSource);

        return new ConfigurationRoot(new List<IConfigurationProvider> { provider });
    }
}