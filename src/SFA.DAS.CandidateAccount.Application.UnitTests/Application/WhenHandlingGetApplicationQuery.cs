using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingGetApplicationQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Found_By_Id_And_Returned(
        GetApplicationQuery query,
        ApplicationEntity entity,
        List<AdditionalQuestionEntity> additionalQuestions,
        [Frozen] Mock<IApplicationRepository> repository,
        [Frozen] Mock<IAdditionalQuestionRepository> additionalQuestionRepository,
        GetApplicationQueryHandler handler)
    {
        query.CandidateId = entity.CandidateId;
        repository.Setup(x => x.GetById(query.ApplicationId)).ReturnsAsync(entity);
        additionalQuestionRepository
            .Setup(x => x.GetAll(query.ApplicationId, query.CandidateId, CancellationToken.None))
            .ReturnsAsync(additionalQuestions);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Application.Should().BeEquivalentTo((Domain.Application.Application)entity, options => options.Excluding(prop => prop.AdditionalQuestions));
        actual.Application!.AdditionalQuestions.Should().BeEquivalentTo(additionalQuestions, options => options
                .Excluding(prop => prop.Answer)
                .Excluding(prop => prop.ApplicationId)
                .Excluding(prop => prop.QuestionId)
        );
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Exist_Then_Null_Returned(
        GetApplicationQuery query,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        repository.Setup(x => x.GetById(query.ApplicationId)).ReturnsAsync((ApplicationEntity)null!);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Application.Should().BeNull();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Belong_To_The_Candidate_Then_Error_Returned(
        GetApplicationQuery query,
        ApplicationEntity entity,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        repository.Setup(x => x.GetById(query.ApplicationId)).ReturnsAsync(entity);
        
        Assert.ThrowsAsync<ValidationException>(()=> handler.Handle(query, CancellationToken.None));
    }
}