using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.CreateCandidate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Candidate;

public class WhenHandlingCreateCandidateCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateCandidateCommand command,
        CandidateEntity entity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        CreateCandidateCommandHandler handler)
    {
        candidateRepository.Setup(x => x.Insert(It.Is<CandidateEntity>(c => 
                c.Email.Equals(command.Email)
                && c.FirstName.Equals(command.FirstName)
                && c.LastName.Equals(command.LastName)
                && c.GovUkIdentifier.Equals(command.GovUkIdentifier)
                )))
            .ReturnsAsync(entity);
        
        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Candidate.Should().BeEquivalentTo(entity, options => options
            .Excluding(c=>c.Applications)
            .Excluding(c => c.Status)
            .Excluding(c => c.Address)
        );

        actual.Candidate.Address.Should().BeEquivalentTo(entity.Address, options=>options.Excluding(c=>c.Candidate));
    }
}