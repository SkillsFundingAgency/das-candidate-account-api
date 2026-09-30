using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetInactiveCandidates;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

[TestFixture]
public class WhenCallingGetCandidatesByActivity
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Candidates_Then_Ok_Result_Returned(
        DateTime cutOffDateTime,
        GetInactiveCandidatesQueryResult getCandidatesByActivityQueryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.Is<GetInactiveCandidatesQuery>(c =>
                c.CutOffDateTime == cutOffDateTime
            ), CancellationToken.None))
            .ReturnsAsync(getCandidatesByActivityQueryResult);

        //Act
        var actual = await controller.GetInactiveCandidates(cutOffDateTime);

        //Assert
        var result = actual as Ok<GetInactiveCandidatesQueryResult>;
        result!.Value!.Candidates.Should().BeEquivalentTo(getCandidatesByActivityQueryResult.Candidates);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        DateTime cutOffDateTime,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        //Arrange
        mediator.Setup(x => x.Send(It.IsAny<GetInactiveCandidatesQuery>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        //Act
        var actual = await controller.GetInactiveCandidates(cutOffDateTime);

        //Assert
        var result = actual as StatusCodeHttpResult;
        result?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}