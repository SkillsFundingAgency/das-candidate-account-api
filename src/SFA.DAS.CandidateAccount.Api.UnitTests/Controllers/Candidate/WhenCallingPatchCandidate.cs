using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Commands.UpsertCandidate;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidate;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

public class WhenCallingPatchCandidate
{
    [Test, MoqAutoData]
    public async Task Then_If_The_Candidate_Does_Not_Exist_Not_Found_Is_Returned(
        Guid candidateId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController sut)
    {
        // arrange
        mediator
            .Setup(x => x.Send(It.Is<GetCandidateQuery>(c => c.Id.Equals(candidateId)), CancellationToken.None))
            .ReturnsAsync(new GetCandidateQueryResult());

        // act
        var result = await sut.PatchCandidate(candidateId.ToString(), new JsonPatchDocument<Domain.Candidate.Candidate>());

        // assert
        result.Should().BeOfType<NotFound>();
    }
    
    [Test, MoqAutoData]
    public async Task Then_The_Candidate_Is_Patched(
        string newGovUkIdentifier,
        Domain.Candidate.Candidate candidate,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController sut)
    {
        // arrange
        mediator
            .Setup(x => x.Send(It.IsAny<GetCandidateQuery>(), CancellationToken.None))
            .ReturnsAsync(new GetCandidateQueryResult { Candidate = candidate });

        Domain.Candidate.Candidate? capturedCandidate = null;
        mediator
            .Setup(x => x.Send(It.IsAny<UpsertCandidateCommand>(), CancellationToken.None))
            .Callback<IRequest<UpsertCandidateCommandResponse>, CancellationToken>((command, _) => capturedCandidate = (command as UpsertCandidateCommand)?.Candidate)
            .ReturnsAsync(new UpsertCandidateCommandResponse { Candidate = candidate });

        var patchDocument = new JsonPatchDocument<Domain.Candidate.Candidate>();
        patchDocument.Replace(x => x.GovUkIdentifier, newGovUkIdentifier);
        
        // act
        var result = await sut.PatchCandidate(candidate.Id.ToString(), patchDocument);

        // assert
        result.Should().BeOfType<Ok<Domain.Candidate.Candidate>>();
        capturedCandidate!.GovUkIdentifier.Should().Be(newGovUkIdentifier);
    }
}