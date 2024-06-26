﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetTrainingCourseItem;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.TrainingCourses;
public class WhenHandlingGetTrainingCourseItemQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Returned(
        GetTrainingCourseItemQuery request,
        TrainingCourseEntity entity,
        [Frozen] Mock<ITrainingCourseRepository> trainingCoursesRepository,
        GetTrainingCourseItemQueryHandler handler)
    {
        trainingCoursesRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId, request.Id, CancellationToken.None)).ReturnsAsync(entity);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(entity, options => options
            .Excluding(ctx => ctx.FromYear)
            .Excluding(ctx => ctx.Provider)
            .Excluding(ctx => ctx.ApplicationEntity)
        );
    }
}
