using SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteApplication;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.TrainingCourse;
using SFA.DAS.CandidateAccount.Data.WorkExperience;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingDeleteApplicationCommand
{
    [Test, MoqAutoData]
    public async Task Handles_No_Application(
        DeleteApplicationCommand request,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync((ApplicationEntity?)null);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        result.Should().Be(DeleteApplicationCommandResult.None);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Handles_Application_For_A_Different_Candidate(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        result.Should().Be(DeleteApplicationCommandResult.None);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Deletes_Work_Histories(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Frozen] Mock<IWorkHistoryRepository> workHistoryRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        application.CandidateId = request.CandidateId;
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        workHistoryRepository.Verify(x => x.DeleteAllAsync(request.ApplicationId, request.CandidateId, It.IsAny<CancellationToken>()), Times.Once);
        result.ApplicationId.Should().Be(request.ApplicationId);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Deletes_Training_Courses(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Frozen] Mock<ITrainingCourseRepository> trainingCourseRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        application.CandidateId = request.CandidateId;
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        trainingCourseRepository.Verify(x => x.DeleteAllAsync(request.ApplicationId, request.CandidateId, It.IsAny<CancellationToken>()), Times.Once);
        result.ApplicationId.Should().Be(request.ApplicationId);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Deletes_Qualifications(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        application.CandidateId = request.CandidateId;
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        qualificationRepository.Verify(x => x.DeleteAllAsync(request.ApplicationId, request.CandidateId, It.IsAny<CancellationToken>()), Times.Once);
        result.ApplicationId.Should().Be(request.ApplicationId);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Deletes_Additional_Questions(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Frozen] Mock<IAdditionalQuestionRepository> additionalQuestionRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        application.CandidateId = request.CandidateId;
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        additionalQuestionRepository.Verify(x => x.DeleteAllAsync(request.ApplicationId, request.CandidateId, It.IsAny<CancellationToken>()), Times.Once);
        result.ApplicationId.Should().Be(request.ApplicationId);
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Deletes_The_Application(
        DeleteApplicationCommand request,
        ApplicationEntity application,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        [Greedy] DeleteApplicationCommandHandler sut)
    {
        // arrange
        application.CandidateId = request.CandidateId;
        applicationRepository
            .Setup(x => x.GetById(request.ApplicationId, false))
            .ReturnsAsync(application);

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        applicationRepository.Verify(x => x.DeleteAsync(request.ApplicationId, request.CandidateId, It.IsAny<CancellationToken>()), Times.Once);
        result.ApplicationId.Should().Be(request.ApplicationId);
    }
}