using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertEmploymentLocation;
using SFA.DAS.CandidateAccount.Data.EmploymentLocation;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.EmploymentLocations
{
    [TestFixture]
    public class WhenHandlingUpsertEmploymentLocationsCommandHandler
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Request_Is_Handled_And_EmploymentLocation_Created(
            //List<Domain.Application.Address> addresses,
            UpsertEmploymentLocationCommand command,
            EmploymentLocationEntity entity,
            [Frozen] Mock<IEmploymentLocationRepository> repository,
            UpsertEmploymentLocationCommandHandler handler)
        {
            // Arrange
            repository.Setup(x =>
                x.UpsertEmploymentLocation(command.EmploymentLocation, command.CandidateId, CancellationToken.None)).ReturnsAsync(new Tuple<EmploymentLocationEntity, bool>(entity, true));

            // Act
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            actual.EmploymentLocation.Should().NotBeNull();
            actual.IsCreated.Should().BeTrue();
        }
    }
}