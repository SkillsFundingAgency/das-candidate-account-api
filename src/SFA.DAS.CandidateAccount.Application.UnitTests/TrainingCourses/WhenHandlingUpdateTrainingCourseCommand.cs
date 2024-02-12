using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpdateTrainingCourse;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses;
public class WhenHandlingUpdateTrainingCourseCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Updated(
        UpdateTrainingCourseCommand request,
        [Frozen] Mock<ITrainingCourseRespository> repository,
        UpdateTrainingCourseCommandHandler handler)
    {
        repository.Setup(x => x.Update(It.Is<TrainingCourseEntity>(c =>
            c.Id == request.Id &&
            c.Title == request.CourseName &&
            c.ToYear == (byte)request.YearAchieved
            ))).Returns(() => Task.CompletedTask);

        await handler.Handle(request, CancellationToken.None);

        repository.Verify(x => x.Update(It.IsAny<TrainingCourseEntity>()), Times.Once);
    }
}
