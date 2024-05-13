using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application
{
    [TestFixture]
    public class WhenHandlingAddLegacyApplicationCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Application_Is_Created(
            AddLegacyApplicationCommand command,
            [Frozen] Mock<IApplicationRepository> applicationRepository,
            AddLegacyApplicationCommandHandler handler)
        {
            var applicationEntity = new ApplicationEntity
            {
                Id = Guid.NewGuid()
            };

            applicationRepository.Setup(x =>
                    x.InsertLegacyApplication(It.Is<LegacyApplication>(a => a == command.LegacyApplication)))
                .ReturnsAsync(applicationEntity);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Id.Should().Be(applicationEntity.Id);
        }
    }
}
