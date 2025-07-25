using System.ComponentModel.DataAnnotations;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetApplication;
using SFA.DAS.CandidateAccount.Data.AdditionalQuestion;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application;

public class WhenHandlingGetApplicationQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Found_By_Id_And_Returned(
        List<Domain.Application.Address> addresses,
        GetApplicationQuery query,
        ApplicationEntity entity,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        entity.EmploymentLocationEntity = new EmploymentLocationEntity
        {
            Addresses = Domain.Application.Address.ToJson(addresses.ToList()),
            EmploymentLocationInformation = entity.EmploymentLocationEntity.EmploymentLocationInformation,
            EmployerLocationOption = entity.EmploymentLocationEntity.EmployerLocationOption,
        };
        query.CandidateId = entity.CandidateId;
        query.IncludeDetail = false;
        repository.Setup(x => x.GetById(query.ApplicationId, query.IncludeDetail)).ReturnsAsync(entity);
        
        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Application.Should().BeEquivalentTo((Domain.Application.Application)entity, options => options.Excluding(prop => prop.AdditionalQuestions));
        ((Domain.Application.Application)actual.Application!).AdditionalQuestions.Should().BeEquivalentTo(entity.AdditionalQuestionEntities, options => options
                .Excluding(prop => prop.Answer)
                .Excluding(prop => prop.ApplicationId)
                .Excluding(prop => prop.QuestionText)
                .Excluding(prop => prop.ApplicationEntity)
        );
    }
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Found_By_Id_And_Returns_Detail_If_In_Query(
        List<Domain.Application.Address> addresses,
        GetApplicationQuery query,
        ApplicationEntity entity,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        entity.EmploymentLocationEntity = new EmploymentLocationEntity
        {
            Addresses = Domain.Application.Address.ToJson(addresses.ToList()),
            EmploymentLocationInformation = entity.EmploymentLocationEntity.EmploymentLocationInformation,
            EmployerLocationOption = entity.EmploymentLocationEntity.EmployerLocationOption,
        };
        query.CandidateId = entity.CandidateId;
        query.IncludeDetail = true;
        repository.Setup(x => x.GetById(query.ApplicationId, query.IncludeDetail)).ReturnsAsync(entity);
        

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Application.Should().BeEquivalentTo((ApplicationDetail)entity, options => options.Excluding(prop => prop.AdditionalQuestions));
        ((ApplicationDetail)actual.Application!).AdditionalQuestions.Should().BeEquivalentTo(entity.AdditionalQuestionEntities, options => options
            .Excluding(prop => prop.Answer)
            .Excluding(prop => prop.ApplicationId)
            .Excluding(prop => prop.QuestionText)
            .Excluding(prop => prop.ApplicationEntity)
        );
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Exist_Then_Null_Returned(
        GetApplicationQuery query,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        repository.Setup(x => x.GetById(query.ApplicationId, query.IncludeDetail)).ReturnsAsync((ApplicationEntity)null!);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.Application.Should().BeNull();
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_The_Application_Does_Not_Belong_To_The_Candidate_Then_Null_Returned(
        GetApplicationQuery query,
        ApplicationEntity entity,
        [Frozen] Mock<IApplicationRepository> repository,
        GetApplicationQueryHandler handler)
    {
        repository.Setup(x => x.GetById(query.ApplicationId, query.IncludeDetail)).ReturnsAsync(entity);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}