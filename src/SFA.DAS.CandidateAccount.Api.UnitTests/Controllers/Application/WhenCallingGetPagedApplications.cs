using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetPagedApplications;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application;

[TestFixture]
public class WhenCallingGetPagedApplications
{
    [Test, MoqAutoData]
    public async Task Then_The_Query_Is_Sent_And_Ok_Result_Returned(
        DateTime addedAfter,
        GetPagedApplicationsQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        mediator.Setup(x => x.Send(It.Is<GetPagedApplicationsQuery>(c =>
                c.AddedAfter == addedAfter &&
                c.PageNumber == 2 &&
                c.PageSize == 100), CancellationToken.None))
            .ReturnsAsync(queryResult);

        var actual = await controller.GetPagedApplications(addedAfter, 2, 100);

        var result = actual as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeEquivalentTo(queryResult);
    }

    [Test]
    [MoqInlineAutoData(0, 10)]
    [MoqInlineAutoData(1, 0)]
    [MoqInlineAutoData(1, ApplicationController.MaxPagedApplicationsPageSize + 1)]
    public async Task Then_Invalid_Paging_Returns_BadRequest(
        int pageNumber,
        int pageSize,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        var actual = await controller.GetPagedApplications(null, pageNumber, pageSize);

        actual.Should().BeOfType<BadRequestResult>();
        mediator.Verify(x => x.Send(It.IsAny<GetPagedApplicationsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Returned(
        [Frozen] Mock<IMediator> mediator,
        [Greedy] ApplicationController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPagedApplicationsQuery>(), CancellationToken.None))
            .ThrowsAsync(new Exception("Error"));

        var actual = await controller.GetPagedApplications(null);

        var result = actual as StatusCodeResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
