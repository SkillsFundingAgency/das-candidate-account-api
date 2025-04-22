using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteSavedVacancy;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.SavedVacancies
{
    [TestFixture]
    public class WhenHandlingDeleteSavedVacancyCommand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Entity_Deleted(
            DeleteSavedVacancyCommand command,
            SavedVacancy repositoryResult,
            [Frozen] Mock<ISavedVacancyRepository> repository,
            DeleteSavedVacancyCommandHandler handler)
        {
            repository.Setup(x =>
                    x.Get(command.CandidateId, command.VacancyId, null))
                .ReturnsAsync(repositoryResult);

            var actual = await handler.Handle(command, CancellationToken.None);

            actual.Should().Be(Unit.Value);

            repository.Verify(x => x.Delete(repositoryResult), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Entity_Not_Deleted(
            DeleteSavedVacancyCommand command,
            SavedVacancy repositoryResult,
            [Frozen] Mock<ISavedVacancyRepository> repository,
            DeleteSavedVacancyCommandHandler handler)
        {
            repository.Setup(x =>
                    x.Get(command.CandidateId, command.VacancyId, null))
                .ReturnsAsync(() => null);

            var actual = await handler.Handle(command, CancellationToken.None);

            actual.Should().Be(Unit.Value);
            repository.Verify(x => x.Delete(repositoryResult), Times.Never);
        }
    }
}
