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

public class WhenHandlingUpsertApplicationRequest
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_Candidate_Retrieved_And_Application_Created(
        UpsertApplicationRequest request,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.DisabilityStatus.Equals(request.DisabilityStatus)
                && c.Status.Equals((short)request.Status)
                && c.IsApplicationQuestionsComplete.Equals((short)request.IsApplicationQuestionsComplete)
                && c.IsDisabilityConfidenceComplete.Equals((short)request.IsDisabilityConfidenceComplete)
                && c.IsEducationHistoryComplete.Equals((short)request.IsEducationHistoryComplete)
                && c.IsWorkHistoryComplete.Equals((short)request.IsWorkHistoryComplete)
                && c.IsInterviewAdjustmentsComplete.Equals((short)request.IsInterviewAdjustmentsComplete)
                ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, true));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Application.Id.Should().Be(applicationEntity.Id);
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public void Then_If_The_Candidate_Does_Not_Exist_Then_Error_Returned(
        UpsertApplicationRequest request,
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email))!.ReturnsAsync((CandidateEntity)null!);
        
        Assert.ThrowsAsync<ValidationException>(()=> handler.Handle(request, CancellationToken.None));
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Candidate_And_Application_Exist_It_Is_Updated(
        UpsertApplicationRequest request,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        UpsertApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.DisabilityStatus.Equals(request.DisabilityStatus)
                && c.Status.Equals((short)request.Status)
                && c.IsApplicationQuestionsComplete.Equals((short)request.IsApplicationQuestionsComplete)
                && c.IsDisabilityConfidenceComplete.Equals((short)request.IsDisabilityConfidenceComplete)
                && c.IsEducationHistoryComplete.Equals((short)request.IsEducationHistoryComplete)
                && c.IsWorkHistoryComplete.Equals((short)request.IsWorkHistoryComplete)
                && c.IsInterviewAdjustmentsComplete.Equals((short)request.IsInterviewAdjustmentsComplete)
            ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, false));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Application.Id.Should().Be(applicationEntity.Id);
        actual.IsCreated.Should().BeFalse();
    }
}