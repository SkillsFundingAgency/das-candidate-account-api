using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingGetCandidateByEmailQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Query_Is_Handled_And_Data_Candidate_Returned(
        GetCandidateByEmailQuery query,
        CandidateEntity entity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByEmailQueryHandler handler)
    {
        // arrange
        candidateRepository
            .Setup(x => x.GetCandidateByEmail(query.EmailAddress))
            .ReturnsAsync(entity);

        // act
        var actual = await handler.Handle(query, CancellationToken.None);

        // assert
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
        GetCandidateByEmailQuery query,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        GetCandidateByEmailQueryHandler handler)
    {
        // arrange
        candidateRepository
            .Setup(x => x.GetCandidateByEmail(query.EmailAddress))
            .ReturnsAsync((CandidateEntity?)null);

        // act
        var actual = await handler.Handle(query, CancellationToken.None);

        // assert
        actual.Candidate.Should().BeNull();
    }
}