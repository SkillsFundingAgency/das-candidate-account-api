using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.CandidatePreferences.Commands.PutCandidatePreferences;
using SFA.DAS.CandidateAccount.Data.CandidatePreferences;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.NotificationPreferences;
public class WhenHandlingPutNotificationPreferencesCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_And_Upsert_Is_Done(
        PutCandidatePreferencesCommand comand,
        List<Tuple<bool, CandidatePreferencesEntity>> upsertResponse,
        [Frozen] Mock<ICandidatePreferencesRepository> repository,
        PutCandidatePreferencesCommandHandler handler)
    {
        repository.Setup(x =>
            x.Upsert(It.IsAny<List<CandidatePreference>>())).ReturnsAsync(upsertResponse);

        var actual = await handler.Handle(comand, CancellationToken.None);

        actual.CandidatePreferences.Count.Should().Be(comand.CandidatePreferences.Count);
    }
}
