using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingCreateCandidateRequest
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateCandidateRequest request,
        CandidateEntity entity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        CreateCandidateRequestHandler handler)
    {
        candidateRepository.Setup(x => x.Insert(It.Is<CandidateEntity>(c => 
                c.Email.Equals(request.Email)
                && c.FirstName.Equals(request.FirstName)
                && c.LastName.Equals(request.LastName)
                && c.GovUkIdentifier.Equals(request.GovUkIdentifier)
                )))
            .ReturnsAsync(entity);
        
        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Candidate.Should().BeEquivalentTo(entity, options => options.Excluding(c=>c.Applications));
    }
}