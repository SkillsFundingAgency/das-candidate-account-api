using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourses;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses;
public class WhenHandlingGetTrainingCoursesQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entities_Returned(
        GetTrainingCoursesQuery request,
        List<TrainingCourseEntity> entities,
        [Frozen] Mock<ITrainingCourseRepository> trainingCoursesRepository,
        GetTrainingCoursesQueryHandler handler)
    {
        trainingCoursesRepository.Setup(x => x.GetAll(request.ApplicationId, request.CandidateId, CancellationToken.None)).ReturnsAsync(entities);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.TrainingCourses.Should().BeEquivalentTo(entities, options => options
            .Excluding(ctx => ctx.FromYear)
            .Excluding(ctx => ctx.Provider)
            .Excluding(ctx => ctx.ApplicationEntity)
        );
    }
}
