using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAllApplicationsById;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Application
{
    [TestFixture]
    public class WhenCallingGetAllApplications
    {
        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Returned_As_Expected(
            GetAllApplicationsByIdApiRequest request,
            GetAllApplicationsByIdQueryResult byCandidateIdQueryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] ApplicationController controller)
        {
            mediator.Setup(x => x.Send(It.Is<GetAllApplicationsByIdQuery>(query =>
                        query.ApplicationIds == request.ApplicationIds &&
                        query.IncludeDetails == request.IncludeDetails),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(byCandidateIdQueryResult);
            var result = await controller.GetApplications(request) as OkObjectResult;
            result!.Value.Should().BeEquivalentTo(byCandidateIdQueryResult);
        }

        [Test, MoqAutoData]
        public async Task Then_The_Response_Is_Exception_Returned_As_InternalServerException(
            GetAllApplicationsByIdApiRequest request,
            GetAllApplicationsByIdQueryResult byCandidateIdQueryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] ApplicationController controller)
        {
            mediator.Setup(x => x.Send(It.Is<GetAllApplicationsByIdQuery>(query =>
                        query.ApplicationIds == request.ApplicationIds &&
                        query.IncludeDetails == request.IncludeDetails),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            var actual = await controller.GetApplications(request);
            actual.Should().BeOfType<StatusCodeResult>();
            var result = actual as StatusCodeResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
