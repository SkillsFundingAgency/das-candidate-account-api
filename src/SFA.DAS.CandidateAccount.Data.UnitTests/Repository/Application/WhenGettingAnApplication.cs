using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenGettingAnApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Returned_By_Id(
        ApplicationEntity entity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities.FindAsync(entity.Id)).ReturnsAsync(entity);

        var actual = await repository.GetById(entity.Id);

        actual.Should().BeEquivalentTo(entity);
    }
}