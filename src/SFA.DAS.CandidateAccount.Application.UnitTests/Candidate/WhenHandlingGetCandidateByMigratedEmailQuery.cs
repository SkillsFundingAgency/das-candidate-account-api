using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByMigratedEmail;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingGetCandidateByMigratedEmailQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Query_Is_Handled_And_Data_Candidate_Returned(
        GetCandidateByMigratedEmailQuery query,
        CandidateEntity entity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByMigratedEmailQueryHandler handler)
    {
        candidateRepository.Setup(x => x.GetByMigratedCandidateEmail(query.Email)).ReturnsAsync(entity);

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
        GetCandidateByMigratedEmailQuery query,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByMigratedEmailQueryHandler handler)
    {
        candidateRepository.Setup(x => x.GetByMigratedCandidateEmail(query.Email)).ReturnsAsync((CandidateEntity?)null);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Candidate.Should().BeNull();
    }
}