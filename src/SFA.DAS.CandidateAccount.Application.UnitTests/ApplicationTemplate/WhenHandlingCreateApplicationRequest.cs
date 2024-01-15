using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.CreateApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingCreateApplicationRequest
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_Candidate_Retrieved_And_Application_Created(
        CreateApplicationRequest request,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        CreateApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.Status.Equals(request.Status)
                ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, true));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Application.Should().BeEquivalentTo(applicationEntity, options=> options
            .Excluding(c=>c.CandidateEntity)
            .Excluding(c=>c.CandidateId)
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
        );
        actual.IsCreated.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public void Then_If_The_Candidate_Does_Not_Exist_Then_Error_Returned(
        CreateApplicationRequest request,
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        CreateApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email))!.ReturnsAsync((CandidateEntity)null!);
        
        Assert.ThrowsAsync<ValidationException>(()=> handler.Handle(request, CancellationToken.None));
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Candidate_And_Application_Exist_It_Is_Updated(
        CreateApplicationRequest request,
        ApplicationEntity applicationEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationRepository> applicationRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        CreateApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.Status.Equals(request.Status)
            ))).ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, false));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Application.Should().BeEquivalentTo(applicationEntity, options=> options
            .Excluding(c=>c.CandidateEntity)
            .Excluding(c=>c.CandidateId)
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
        );
        actual.IsCreated.Should().BeFalse();
    }
}