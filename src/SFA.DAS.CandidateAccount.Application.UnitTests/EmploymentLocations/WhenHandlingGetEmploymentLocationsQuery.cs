﻿using AutoFixture.NUnit3;
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
            EmploymentLocationEntity entity,
            [Frozen] Mock<IEmploymentLocationRepository> employmentLocationRepository,
            GetEmploymentLocationsQueryHandler handler)
        {
            entity.Addresses = Domain.Application.Address.ToJson(addresses);
            employmentLocationRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId, CancellationToken.None)).ReturnsAsync(entity);

            var actual = await handler.Handle(request, CancellationToken.None);

            actual.EmploymentLocation!.EmployerLocationOption.Should().Be(entity.EmployerLocationOption);
            actual.EmploymentLocation.EmploymentLocationInformation.Should().Be(entity.EmploymentLocationInformation);
            actual.EmploymentLocation.Id.Should().Be(entity.Id);
            actual.EmploymentLocation.ApplicationId.Should().Be(entity.ApplicationId);
        }
    }
}