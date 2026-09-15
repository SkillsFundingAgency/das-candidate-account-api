using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidateByEmail;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Candidate;

public class WhenCallingGetCandidateByEmail
{
    [Test, MoqAutoData]
    public async Task Then_Mediator_Query_Is_Called_And_Candidate_Returned(
        string emailAddress,
        GetCandidateByEmailQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        // arrange
        mediator
            .Setup(x => x.Send(It.Is<GetCandidateByEmailQuery>(c => c.EmailAddress.Equals(emailAddress)), CancellationToken.None))
            .ReturnsAsync(queryResult);
        
        // act
        var actual = await controller.GetCandidateByEmailAddress(emailAddress) as Ok<Domain.Candidate.Candidate>;
        
        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(StatusCodes.Status200OK);
        actual.Value.Should().BeEquivalentTo(queryResult.Candidate);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Null_From_Mediator_Response_Not_Found_Returned(
        string emailAddress,
        GetCandidateByEmailQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        // arrange
        queryResult.Candidate = null;
        mediator
            .Setup(x => x.Send(It.Is<GetCandidateByEmailQuery>(c => c.EmailAddress.Equals(emailAddress)), CancellationToken.None))
            .ReturnsAsync(queryResult);

        // act
        var actual = await controller.GetCandidateByEmailAddress(emailAddress) as NotFound;
        
        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_Multiple_Accounts_Are_Located_Then_A_Problem_Is_Returned(
        string emailAddress,
        GetCandidateByEmailQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CandidateController controller)
    {
        // arrange
        queryResult.Candidate = null;
        mediator
            .Setup(x => x.Send(It.IsAny<GetCandidateByEmailQuery>(), CancellationToken.None))
            .Throws<InvalidOperationException>();

        // act
        var actual = await controller.GetCandidateByEmailAddress(emailAddress) as ProblemHttpResult;
        
        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
}