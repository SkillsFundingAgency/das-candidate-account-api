using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplications;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingGetApplicationsQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Applications_For_The_Candidate_Are_Returned(
        List<Domain.Application.Address> addresses,
        Guid candidateId,
        ApplicationStatus status,
        GetApplicationsQuery query,
        List<ApplicationEntity> entities,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationsQueryHandler handler)
    {

        foreach (var applicationEntity in entities)
        {
            applicationEntity.EmploymentLocationEntities = applicationEntity.EmploymentLocationEntities!
                .Select(entityEmploymentLocationEntity => new EmploymentLocationEntity
                {
                    Addresses = Domain.Application.Address.ToJson(addresses.ToList()),
                    EmploymentLocationInformation = entityEmploymentLocationEntity.EmploymentLocationInformation,
                    EmployerLocationOption = entityEmploymentLocationEntity.EmployerLocationOption,
                }).ToList();
        }
        query.CandidateId = candidateId;
        query.Status = status;
        entities.ForEach(x => x.CandidateId = candidateId);
        entities.ForEach(x => x.Status = (short)status);
        repository.Setup(x => x.GetByCandidateId(query.CandidateId, (short)status)).ReturnsAsync(entities);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Applications.Should().BeEquivalentTo(entities.Select(x => (Domain.Application.Application)x));
    }
}