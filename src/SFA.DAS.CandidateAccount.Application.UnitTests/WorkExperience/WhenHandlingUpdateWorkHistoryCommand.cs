using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateWorkHistory;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.WorkExperience;

[TestFixture]
public class WhenHandlingUpdateWorkHistoryCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_And_WorkHistory_Created(
        UpsertWorkHistoryCommand command,
        WorkHistoryEntity workHistoryEntity,
        [Frozen] Mock<IWorkHistoryRepository> workHistoryRepository,
        UpsertWorkHistoryCommandHandler handler)
    {
        workHistoryRepository.Setup(x =>
            x.UpsertWorkHistory(command.WorkHistory, command.CandidateId)).ReturnsAsync(new Tuple<WorkHistoryEntity, bool>(workHistoryEntity, true));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.WorkHistory.Id.Should().Be(workHistoryEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_WorkHistory_Exists_It_Is_Updated(
        UpsertWorkHistoryCommand command,
        WorkHistoryEntity workHistoryEntity,
        [Frozen] Mock<IWorkHistoryRepository> workHistoryRepository,
        UpsertWorkHistoryCommandHandler handler)
    {
        workHistoryRepository.Setup(x => x.UpsertWorkHistory(command.WorkHistory, command.CandidateId))
            .ReturnsAsync(new Tuple<WorkHistoryEntity, bool>(workHistoryEntity, false));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.WorkHistory.Id.Should().Be(workHistoryEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }
}