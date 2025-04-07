using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetEmploymentLocations;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.EmploymentLocations
{
    [TestFixture]
    public class WhenHandlingGetEmploymentLocationsQuery
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Request_Is_Handled_And_Entities_Returned(
            List<Domain.Application.Address> addresses,
            GetEmploymentLocationsQuery request,
            List<EmploymentLocationEntity> entities,
            [Frozen] Mock<IEmploymentLocationRepository> employmentLocationRepository,
            GetEmploymentLocationsQueryHandler handler)
        {
            foreach (var entity in entities)
            {
                entity.Addresses = Domain.Application.Address.ToJson(addresses);
            }
            employmentLocationRepository.Setup(x => x.GetAll(request.ApplicationId, request.CandidateId, CancellationToken.None)).ReturnsAsync(entities);

            var actual = await handler.Handle(request, CancellationToken.None);

            actual.EmploymentLocations.Should().BeEquivalentTo(entities, 
                options => options
                    .Excluding(c => c.ApplicationEntity)
                    .Excluding(c => c.Addresses));
        }
    }
}