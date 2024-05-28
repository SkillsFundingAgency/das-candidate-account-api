using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.CandidateAccount.Api.ApiResponses;
using SFA.DAS.CandidateAccount.Api.Controllers;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetCandidatesByApplicationVacancy;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Api.UnitTests.Controllers.Vacancies;

public class WhenCallingGetCandidateApplications
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Query_Is_Called_And_ApiResponse_Returned_With_Candidate_Data(
        string vacancyRef,
        ApplicationStatus applicationStatus,
        bool allowEmailContact,
        Guid preferenceId,
        GetCandidatesByApplicationVacancyQueryResult queryResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy]VacanciesController controller)
    {
        mediator.Setup(x =>
                x.Send(
                    It.Is<GetCandidatesByApplicationVacancyQuery>(c =>
                        c.VacancyReference == vacancyRef && c.CanEmailOnly == allowEmailContact &&
                        c.PreferenceId == preferenceId && c.StatusId == (short)applicationStatus),
                    CancellationToken.None))
            .ReturnsAsync(queryResult);
        
        var actual =
            await controller.GetCandidateApplications(vacancyRef, allowEmailContact, preferenceId, applicationStatus) as OkObjectResult;

        actual.Should().NotBeNull();
        var actualModel = actual!.Value as GetCandidatesApiResponse;
        actualModel.Should().NotBeNull();
        actualModel!.Candidates.Should().BeEquivalentTo(queryResult.Candidates, options=> options
            .Excluding(c=>c.Address)
            .Excluding(c=>c.CandidatePreferences)
            .Excluding(c=>c.Applications)
            .Excluding(c=>c.Status)
        );
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Returns_Empty_List_If_No_Results_Returned(
        string vacancyRef,
        ApplicationStatus applicationStatus,
        bool allowEmailContact,
        Guid preferenceId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy]VacanciesController controller)
    {
        mediator.Setup(x =>
                x.Send(
                    It.Is<GetCandidatesByApplicationVacancyQuery>(c =>
                        c.VacancyReference == vacancyRef && c.CanEmailOnly == allowEmailContact &&
                        c.PreferenceId == preferenceId && c.StatusId == (short)applicationStatus),
                    CancellationToken.None))
            .ReturnsAsync(new GetCandidatesByApplicationVacancyQueryResult{Candidates = [] });
        
        var actual =
            await controller.GetCandidateApplications(vacancyRef, allowEmailContact, preferenceId, applicationStatus) as OkObjectResult;

        actual.Should().NotBeNull();
        var actualModel = actual!.Value as GetCandidatesApiResponse;
        actualModel.Should().NotBeNull();
        actualModel!.Candidates.Should().BeEmpty();
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Error_Returns_InternalServerError_Response_Code(
        string vacancyRef,
        ApplicationStatus applicationStatus,
        bool allowEmailContact,
        Guid preferenceId,
        [Frozen] Mock<IMediator> mediator,
        [Greedy]VacanciesController controller)
    {
        mediator.Setup(x =>
                x.Send(
                    It.Is<GetCandidatesByApplicationVacancyQuery>(c =>
                        c.VacancyReference == vacancyRef && c.CanEmailOnly == allowEmailContact &&
                        c.PreferenceId == preferenceId && c.StatusId == (short)applicationStatus),
                    CancellationToken.None))
            .ThrowsAsync(new Exception());
        
        var actual =
            await controller.GetCandidateApplications(vacancyRef, allowEmailContact, preferenceId, applicationStatus) as StatusCodeResult;

        actual.Should().NotBeNull();
        actual!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}