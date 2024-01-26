using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingUpsertCandidateCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_And_Candidate_Created(
        UpsertCandidateCommand command,
        CandidateEntity candidateEntity, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertCandidateCommandHandler handler)
    {
        candidateRepository.Setup(x =>
            x.UpsertCandidate(command.Candidate)).ReturnsAsync(new Tuple<CandidateEntity, bool>(candidateEntity, true));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Candidate.Id.Should().Be(candidateEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Candidate_And_Application_Exist_It_Is_Updated(
        UpsertCandidateCommand command,
        CandidateEntity candidateEntity,
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertCandidateCommandHandler handler)
    {
        candidateRepository.Setup(x => x.UpsertCandidate(command.Candidate))
            .ReturnsAsync(new Tuple<CandidateEntity, bool>(candidateEntity, false));
        
        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Candidate.Id.Should().Be(candidateEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }
}