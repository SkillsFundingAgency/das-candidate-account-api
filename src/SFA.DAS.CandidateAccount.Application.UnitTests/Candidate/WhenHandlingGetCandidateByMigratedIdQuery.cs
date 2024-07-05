using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedId;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingGetCandidateByMigratedIdQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Query_Is_Handled_And_Data_Candidate_Returned(
        GetCandidateByMigratedIdQuery query,
        CandidateEntity entity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByMigratedIdQueryHandler handler)
    {
        candidateRepository.Setup(x => x.GetByMigratedCandidateId(query.MigratedCandidateId)).ReturnsAsync(entity);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Candidate.Should().BeEquivalentTo(entity, options=>options
            .Excluding(c=>c.CandidatePreferences)
            .Excluding(c=>c.Address)
            .Excluding(c=>c.AboutYou)
            .Excluding(c=>c.Applications)
            .Excluding(c=>c.Status)
        );
    }

    [Test, MoqAutoData]
    public async Task Then_If_Not_Found_Then_Null_Returned(
        GetCandidateByMigratedIdQuery query,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByMigratedIdQueryHandler handler)
    {
        candidateRepository.Setup(x => x.GetByMigratedCandidateId(query.MigratedCandidateId)).ReturnsAsync((CandidateEntity?)null);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Candidate.Should().BeNull();
    }
}