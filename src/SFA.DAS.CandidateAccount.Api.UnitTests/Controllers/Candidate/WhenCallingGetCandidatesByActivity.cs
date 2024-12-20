using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByActivity;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate
{
    [TestFixture]
    public class WhenCallingGetCandidatesByActivity
    {
        [Test, MoqAutoData]
        public async Task Then_If_MediatorCall_Returns_Candidates_Then_Ok_Result_Returned(
            DateTime cutOffDateTime,
            GetCandidatesByActivityQueryResult getCandidatesByActivityQueryResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] CandidateController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<GetCandidatesByActivityQuery>(c =>
                    c.CutOffDateTime == cutOffDateTime
                ), CancellationToken.None))
                .ReturnsAsync(getCandidatesByActivityQueryResult);

            //Act
            var actual = await controller.GetCandidatesByActivity(cutOffDateTime);

            //Assert
            var result = actual as OkObjectResult;
            var actualResult = result.Value as GetCandidatesByActivityQueryResult;
            actualResult.Candidates.Should().BeEquivalentTo(getCandidatesByActivityQueryResult.Candidates);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
            DateTime cutOffDateTime,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] CandidateController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetCandidatesByActivityQuery>(),
                CancellationToken.None)).ThrowsAsync(new Exception("Error"));

            //Act
            var actual = await controller.GetCandidatesByActivity(cutOffDateTime);

            //Assert
            var result = actual as StatusCodeResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
