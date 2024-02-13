using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateTrainingCourse;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses;
public class WhenHandlingCreateTrainingCourseCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateTrainingCourseCommand request,
        TrainingCourseEntity entity,
        [Frozen] Mock<ITrainingCourseRespository> trainingCourseRepository,
        CreateTrainingCourseCommandHandler handler)
    {
        trainingCourseRepository.Setup(x => x.Insert(It.Is<TrainingCourseEntity>(c =>
            c.ApplicationId == request.ApplicationId &&
            c.Title == request.CourseName &&
            c.ToYear == request.YearAchieved
            ))).ReturnsAsync(entity);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.TrainingCourse.Should().BeEquivalentTo(entity, options => options.Excluding(c => c.Id));
    }
}
