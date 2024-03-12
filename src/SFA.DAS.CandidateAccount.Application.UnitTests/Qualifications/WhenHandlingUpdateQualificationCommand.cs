using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertQualification;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Qualifications;

public class WhenHandlingUpdateQualificationCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_And_TrainingCourse_Created(
        UpsertQualificationCommand command,
        QualificationEntity qualificationEntity,
        ApplicationEntity applicationEntity,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        UpsertQualificationCommandHandler handler)
    {
        applicationEntity.CandidateId = command.CandidateId;
        applicationEntity.TrainingCoursesStatus = (short)SectionStatus.InProgress;

        qualificationRepository.Setup(x =>
            x.Upsert(command.Qualification, command.CandidateId, command.ApplicationId)).ReturnsAsync(new Tuple<QualificationEntity, bool>(qualificationEntity, true));

        applicationRepository.Setup(x => x.GetById(command.ApplicationId))
            .ReturnsAsync(applicationEntity);

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Qualification.Id.Should().Be(qualificationEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_TrainingCourse_Exists_It_Is_Updated(
        UpsertQualificationCommand command,
        QualificationEntity qualificationEntity,
        ApplicationEntity applicationEntity,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        UpsertQualificationCommandHandler handler)
    {
        applicationEntity.CandidateId = command.CandidateId;
        applicationEntity.TrainingCoursesStatus = (short)SectionStatus.InProgress;

        qualificationRepository.Setup(x => x.Upsert(command.Qualification, command.CandidateId, command.ApplicationId))
            .ReturnsAsync(new Tuple<QualificationEntity, bool>(qualificationEntity, false));

        applicationRepository.Setup(x => x.GetById(command.ApplicationId))
            .ReturnsAsync(applicationEntity);

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Qualification.Id.Should().Be(qualificationEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_SectionStatus_Is_NotStarted_Then_It_Is_Updated_To_InProgress(
        UpsertQualificationCommand command,
        QualificationEntity qualificationEntity,
        ApplicationEntity applicationEntity,
        [Frozen] Mock<IQualificationRepository> qualificationRepository,
        [Frozen] Mock<IApplicationRepository> applicationRepository,
        UpsertQualificationCommandHandler handler)
    {
        applicationEntity.CandidateId = command.CandidateId;
        applicationEntity.Id = command.ApplicationId;
        applicationEntity.QualificationsStatus = (short)SectionStatus.NotStarted;

        qualificationRepository.Setup(x => x.Upsert(command.Qualification, command.CandidateId, command.ApplicationId))
            .ReturnsAsync(new Tuple<QualificationEntity, bool>(qualificationEntity, false));

        applicationRepository.Setup(x => x.GetById(command.ApplicationId))
            .ReturnsAsync(applicationEntity);

        applicationRepository.Setup(x => x.Update(It.IsAny<ApplicationEntity>())).ReturnsAsync(applicationEntity);

        await handler.Handle(command, CancellationToken.None);

        applicationRepository.Verify(x => x.Update(It.Is<ApplicationEntity>(a => a.QualificationsStatus == (short)SectionStatus.InProgress)));
    }
}