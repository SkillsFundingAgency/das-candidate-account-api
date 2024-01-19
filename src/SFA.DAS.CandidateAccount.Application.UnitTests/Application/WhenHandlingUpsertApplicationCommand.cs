using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingUpsertApplicationCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_Candidate_Retrieved_And_Application_Created(
        UpsertApplicationCommand command,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationCommandHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(command.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(command.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.DisabilityStatus.Equals(command.DisabilityStatus)
                && c.Status.Equals((short)command.Status)
                && c.JobsStatus.Equals((short)command.IsApplicationQuestionsComplete)
                && c.DisabilityConfidenceStatus.Equals((short)command.IsDisabilityConfidenceComplete)
                && c.QualificationsStatus.Equals((short)command.IsEducationHistoryComplete)
                && c.TrainingCoursesStatus.Equals((short)command.IsWorkHistoryComplete)
                && c.WorkExperienceStatus.Equals((short)command.IsInterviewAdjustmentsComplete)
                ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, true));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Application.Id.Should().Be(applicationEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public void Then_If_The_Candidate_Does_Not_Exist_Then_Error_Returned(
        UpsertApplicationCommand command,
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationCommandHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(command.Email))!.ReturnsAsync((CandidateEntity)null!);
        
        Assert.ThrowsAsync<ValidationException>(()=> handler.Handle(command, CancellationToken.None));
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Candidate_And_Application_Exist_It_Is_Updated(
        UpsertApplicationCommand command,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationCommandHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(command.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(command.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.DisabilityStatus.Equals(command.DisabilityStatus)
                && c.Status.Equals((short)command.Status)
                && c.JobsStatus.Equals((short)command.IsApplicationQuestionsComplete)
                && c.DisabilityConfidenceStatus.Equals((short)command.IsDisabilityConfidenceComplete)
                && c.QualificationsStatus.Equals((short)command.IsEducationHistoryComplete)
                && c.TrainingCoursesStatus.Equals((short)command.IsWorkHistoryComplete)
                && c.WorkExperienceStatus.Equals((short)command.IsInterviewAdjustmentsComplete)
            ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, false));

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Application.Id.Should().Be(applicationEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }
}