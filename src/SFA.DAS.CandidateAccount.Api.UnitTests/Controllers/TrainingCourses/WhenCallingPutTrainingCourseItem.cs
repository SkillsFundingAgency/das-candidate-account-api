using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiRequests;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.TrainingCourses;
public class WhenCallingPutTrainingCourseItem
{
    [Test, MoqAutoData]
    public async Task Then_If_MediatorCall_Returns_Created_Then_Created_Result_Returned(
        Guid id,
        Guid candidateId,
        Guid applicationId,
        PutTrainingCourseItemRequest trainingCourseItemRequest,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] TrainingCoursesController controller)
    {
        mediator.Setup(x => x.Send(It.Is<UpdateTrainingCourseCommand>(c =>
            c.Id.Equals(id)
            && c.CandidateId.Equals(candidateId)
            && c.ApplicationId.Equals(applicationId)
            && c.CourseName.Equals(trainingCourseItemRequest.CourseName)
            && c.YearAchieved.Equals(trainingCourseItemRequest.YearAchieved)
            ), CancellationToken.None));

        var actual = await controller.PutTrainingCourseItem(candidateId, applicationId, id, trainingCourseItemRequest);

        actual.Should().BeOfType<OkResult>();
    }
}
