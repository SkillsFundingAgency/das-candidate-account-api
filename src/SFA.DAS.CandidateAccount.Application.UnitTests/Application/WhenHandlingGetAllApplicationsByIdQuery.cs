using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAllApplicationsById;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application
{
    [TestFixture]
    public class WhenHandlingGetAllApplicationsByIdQuery
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            List<Domain.Application.Address> addresses,
            List<ApplicationEntity> applications,
            GetAllApplicationsByIdQuery query,
            [Frozen] Mock<IApplicationRepository> applicationRepository,
            GetAllApplicationsByIdQueryHandler handler)
        {
            foreach (var applicationEntity in applications)
            {
                applicationEntity.EmploymentLocationEntity!.Addresses = Domain.Application.Address.ToJson(addresses);
            }
            applicationRepository.Setup(x => x.GetAllById(query.ApplicationIds, query.IncludeDetails, CancellationToken.None))
                .ReturnsAsync(applications);

            var result = await handler.Handle(query, CancellationToken.None);

            var getAllApplicationsByIdQueryResult = new GetAllApplicationsByIdQueryResult(applications
                .Select(x => query.IncludeDetails
                    ? (ApplicationDetail)x
                    : (Domain.Application.Application)x)
                .ToList());

            result.Should().BeEquivalentTo(getAllApplicationsByIdQueryResult);
        }
    }
}
