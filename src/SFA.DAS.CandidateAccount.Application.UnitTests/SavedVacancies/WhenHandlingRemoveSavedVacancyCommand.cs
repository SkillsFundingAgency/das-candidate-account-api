using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.RemoveSavedVacancy;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.SavedVacancies
{
    [TestFixture]
    public class WhenHandlingRemoveSavedVacancyCommand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Entity_Deleted(
            RemoveSavedVacancyCommand command,
            SavedVacancy repositoryResult,
            [Frozen] Mock<ISavedVacancyRepository> repository,
            RemoveSavedVacancyCommandHandler handler)
        {
            repository.Setup(x =>
                    x.Get(command.CandidateId, command.VacancyReference))
                .ReturnsAsync(repositoryResult);

            var actual = await handler.Handle(command, CancellationToken.None);

            actual.Should().Be(Unit.Value);

            repository.Verify(x => x.Delete(repositoryResult), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Entity_Not_Deleted(
            RemoveSavedVacancyCommand command,
            SavedVacancy repositoryResult,
            [Frozen] Mock<ISavedVacancyRepository> repository,
            RemoveSavedVacancyCommandHandler handler)
        {
            repository.Setup(x =>
                    x.Get(command.CandidateId, command.VacancyReference))
                .ReturnsAsync(() => null);

            var actual = await handler.Handle(command, CancellationToken.None);

            actual.Should().Be(Unit.Value);
            repository.Verify(x => x.Delete(repositoryResult), Times.Never);
        }
    }
}
