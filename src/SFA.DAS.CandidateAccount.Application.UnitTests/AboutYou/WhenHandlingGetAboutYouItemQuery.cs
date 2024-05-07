using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAboutYouItem;
using SFA.DAS.CandidateAccount.Data.AboutYou;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.AboutYou;
public class WhenHandlingGetAboutYouItemQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Returned(
    GetAboutYouItemQuery request,
    Domain.Candidate.AboutYou entity,
    [Frozen] Mock<IAboutYouRespository> aboutYouRepository,
    GetAboutYouItemQueryHandler handler)
    {
        aboutYouRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId)).ReturnsAsync(entity);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.AboutYou.Should().BeEquivalentTo(entity, options => options
            .Excluding(ctx => ctx.Id)
        );
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_is_Handled_And_Empty_Object_Returned(
        GetAboutYouItemQuery request,
        [Frozen] Mock<IAboutYouRespository> aboutYouRepository,
        GetAboutYouItemQueryHandler handler)
    {
        aboutYouRepository.Setup(x => x.Get(request.ApplicationId, request.CandidateId)).ReturnsAsync((Domain.Candidate.AboutYou)null);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(new GetAboutYouItemQueryResult());
    }

}
