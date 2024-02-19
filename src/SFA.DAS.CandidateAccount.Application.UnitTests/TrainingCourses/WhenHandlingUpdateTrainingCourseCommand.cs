using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses;
public class WhenHandlingUpdateTrainingCourseCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_And_TrainingCourse_Created(
        UpsertTrainingCourseCommand command,
        TrainingCourseEntity trainingCourseEntity,
        [Frozen] Mock<ITrainingCourseRespository> trainingCourseRepository,
        UpsertTrainingCourseCommandHandler handler)
    {
        trainingCourseRepository.Setup(x =>
            x.UpsertTrainingCourse(command.TrainingCourse, command.CandidateId)).ReturnsAsync(new Tuple<TrainingCourseEntity, bool>(trainingCourseEntity, true));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.TrainingCourse.Id.Should().Be(trainingCourseEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_TrainingCourse_Exists_It_Is_Updated(
        UpsertTrainingCourseCommand command,
        TrainingCourseEntity trainingCourseEntity,
        [Frozen] Mock<ITrainingCourseRespository> trainingCourseRepository,
        UpsertTrainingCourseCommandHandler handler)
    {
        trainingCourseRepository.Setup(x => x.UpsertTrainingCourse(command.TrainingCourse, command.CandidateId))
            .ReturnsAsync(new Tuple<TrainingCourseEntity, bool>(trainingCourseEntity, false));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.TrainingCourse.Id.Should().Be(trainingCourseEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }
}
