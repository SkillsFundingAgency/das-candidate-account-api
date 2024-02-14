using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.TrainingCourses;
public class WhenCallingPost
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid candidateId,
        Guid applicationId,
        TrainingCourseRequest trainingCourseRequest,
        CreateTrainingCourseCommandResponse createTrainingCourseCommandResponse,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] TrainingCoursesController controller)
    {
        mediator.Setup(x => x.Send(It.Is<CreateTrainingCourseCommand>(c =>
                c.CandidateId.Equals(candidateId) &&
                c.ApplicationId.Equals(applicationId) &&
                c.CourseName.Equals(trainingCourseRequest.CourseName)
                && c.YearAchieved.Equals((int)trainingCourseRequest.YearAchieved)
            ), CancellationToken.None))
            .ReturnsAsync(createTrainingCourseCommandResponse);

        var actual = await controller.PostTrainingCourse(candidateId, applicationId, trainingCourseRequest);

        var result = actual as CreatedResult;
        var actualResult = result.Value as Domain.Application.TrainingCourseEntity;
        actualResult.Should().BeEquivalentTo(createTrainingCourseCommandResponse.TrainingCourse);
    }

    [Test, MoqAutoData]
    public async Task Then_If_Error_Then_InternalServerError_Response_Returned(
        Guid candidateId,
        Guid applicationId,
        TrainingCourseRequest trainingCourseRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] TrainingCoursesController controller)
    {
        mediator.Setup(x => x.Send(It.IsAny<CreateTrainingCourseCommand>(),
            CancellationToken.None)).ThrowsAsync(new Exception("Error"));

        var actual = await controller.PostTrainingCourse(candidateId, applicationId, trainingCourseRequest);

        var result = actual as StatusCodeResult;
        result?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
