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
        public async Task Handle_ShouldReturnResponseWithCreatedFlag_WhenUpsertIsSuccessful(
            List<Domain.Application.Address> addresses,
            [Frozen] Mock<IEmploymentLocationRepository> mockRepository,
            [Greedy] UpsertEmploymentLocationCommandHandler handler)
        {
            // Arrange
            var command = new UpsertEmploymentLocationCommand
            {
                EmploymentLocation = new EmploymentLocation
                {
                    Id = Guid.NewGuid(),
                    Addresses = addresses,
                    ApplicationId = Guid.NewGuid(),
                    EmployerLocationOption = 1,
                    EmploymentLocationInformation = "Test Info"
                },
                CandidateId = Guid.NewGuid()
            };

            var employmentLocationEntity = new EmploymentLocationEntity
            {
                Id = command.EmploymentLocation.Id,
                Addresses = Domain.Application.Address.ToJson(addresses),
                ApplicationId = command.EmploymentLocation.ApplicationId,
                EmployerLocationOption = command.EmploymentLocation.EmployerLocationOption,
                EmploymentLocationInformation = command.EmploymentLocation.EmploymentLocationInformation
            };

            mockRepository
                .Setup(repo => repo.UpsertEmploymentLocation(It.IsAny<EmploymentLocationEntity>(), command.CandidateId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Tuple<EmploymentLocationEntity, bool>(employmentLocationEntity, true));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.EmploymentLocation.Id.Should().Be(employmentLocationEntity.Id);
            result.IsCreated.Should().BeTrue();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Handle_ShouldReturnResponseWithNotCreatedFlag_WhenUpsertIsNotSuccessful(
            List<Domain.Application.Address> addresses,
            [Frozen] Mock<IEmploymentLocationRepository> mockRepository,
            [Greedy] UpsertEmploymentLocationCommandHandler handler)
        {
            // Arrange
            var command = new UpsertEmploymentLocationCommand
            {
                EmploymentLocation = new EmploymentLocation
                {
                    Id = Guid.NewGuid(),
                    Addresses = addresses,
                    ApplicationId = Guid.NewGuid(),
                    EmployerLocationOption = 1,
                    EmploymentLocationInformation = "Test Info"
                },
                CandidateId = Guid.NewGuid()
            };

            var employmentLocationEntity = new EmploymentLocationEntity
            {
                Id = command.EmploymentLocation.Id,
                Addresses = Domain.Application.Address.ToJson(addresses),
                ApplicationId = command.EmploymentLocation.ApplicationId,
                EmployerLocationOption = command.EmploymentLocation.EmployerLocationOption,
                EmploymentLocationInformation = command.EmploymentLocation.EmploymentLocationInformation
            };

            mockRepository
                .Setup(repo => repo.UpsertEmploymentLocation(It.IsAny<EmploymentLocationEntity>(), command.CandidateId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Tuple<EmploymentLocationEntity, bool>(employmentLocationEntity, false));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.EmploymentLocation.Id.Should().Be(employmentLocationEntity.Id);
            result.IsCreated.Should().BeFalse();
        }

        [Test, RecursiveMoqAutoData]
        public void Handle_ShouldThrowException_WhenRepositoryThrowsException(List<Domain.Application.Address> addresses,
            [Frozen] Mock<IEmploymentLocationRepository> mockRepository,
            [Greedy] UpsertEmploymentLocationCommandHandler handler)
        {
            // Arrange
            var command = new UpsertEmploymentLocationCommand
            {
                EmploymentLocation = new EmploymentLocation
                {
                    Id = Guid.NewGuid(),
                    Addresses = addresses,
                    ApplicationId = Guid.NewGuid(),
                    EmployerLocationOption = 1,
                    EmploymentLocationInformation = "Test Info"
                },
                CandidateId = Guid.NewGuid()
            };

            mockRepository
                .Setup(repo => repo.UpsertEmploymentLocation(It.IsAny<EmploymentLocationEntity>(), command.CandidateId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
            act.Should().ThrowAsync<Exception>().WithMessage("Repository error");
        }
    }
}