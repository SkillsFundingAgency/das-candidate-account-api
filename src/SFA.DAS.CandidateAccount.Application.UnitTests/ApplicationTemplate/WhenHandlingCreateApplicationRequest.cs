using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.ApplicationTemplate.Commands.CreateApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.ApplicationTemplate;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.ApplicationTemplate;

public class WhenHandlingCreateApplicationRequest
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Request_Is_Handled_Candidate_Retrieved_And_ApplicationTemplate_Created(
        CreateApplicationRequest request,
        ApplicationTemplateEntity applicationTemplateEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationTemplateRepository> applicationTemplateRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        CreateApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationTemplateRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationTemplateEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.Status.Equals(request.Status)
                ))).ReturnsAsync(new Tuple<ApplicationTemplateEntity, bool>(applicationTemplateEntity, true));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.ApplicationTemplate.Should().BeEquivalentTo(applicationTemplateEntity, options=> options
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
    public async Task Then_If_The_Candidate_And_ApplicationTemplate_Exist_It_Is_Updated(
        CreateApplicationRequest request,
        ApplicationTemplateEntity applicationTemplateEntity,
        CandidateEntity candidateEntity,
        [Frozen] Mock<IApplicationTemplateRepository> applicationTemplateRepository, 
        [Frozen] Mock<ICandidateRepository> candidateRepository, 
        CreateApplicationRequestHandler handler)
    {
        candidateRepository.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(candidateEntity);
        applicationTemplateRepository.Setup(x =>
            x.Upsert(It.Is<ApplicationTemplateEntity>(c => 
                c.VacancyReference.Equals(request.VacancyReference)
                && c.CandidateId.Equals(candidateEntity.Id)
                && c.Status.Equals(request.Status)
            ))).ReturnsAsync(new Tuple<ApplicationTemplateEntity, bool>(applicationTemplateEntity, false));

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.ApplicationTemplate.Should().BeEquivalentTo(applicationTemplateEntity, options=> options
            .Excluding(c=>c.CandidateEntity)
            .Excluding(c=>c.CandidateId)
            .Excluding(c=>c.CreatedDate)
            .Excluding(c=>c.UpdatedDate)
        );
        actual.IsCreated.Should().BeFalse();
    }
}