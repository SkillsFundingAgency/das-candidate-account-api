using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetPagedCandidates;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

[TestFixture]
public class WhenCallingGetPagedCandidates
{
    [Test, MoqAutoData]
    public async Task Then_The_Query_Is_Sent_And_Ok_Result_Returned(
        DateTime addedSince,
        GetPagedCandidatesQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        mediator.Setup(x => x.Send(It.Is<GetPagedCandidatesQuery>(c =>
                c.AddedSince == addedSince &&
                c.PageNumber == 2 &&
                c.PageSize == 100
            ), CancellationToken.None))
            .ReturnsAsync(queryResult);

        var actual = await controller.GetPagedCandidates(addedSince, 2, 100);

        var result = actual as Ok<GetPagedCandidatesQueryResult>;
        result.Should().NotBeNull();
        result!.Value.Should().BeEquivalentTo(queryResult);
    }

    [Test]
    [MoqInlineAutoData(0, 10)]
    [MoqInlineAutoData(1, 0)]
    [MoqInlineAutoData(1, CandidateController.MaxPagedCandidatesPageSize + 1)]
    public async Task Then_Invalid_Paging_Returns_BadRequest(
        int pageNumber,
        int pageSize,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        var actual = await controller.GetPagedCandidates(null, pageNumber, pageSize);

        actual.Should().BeOfType<BadRequest>();
        mediator.Verify(x => x.Send(It.IsAny<GetPagedCandidatesQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPagedCandidatesQuery>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        var actual = await controller.GetPagedCandidates(null);

        var result = actual as StatusCodeHttpResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}
