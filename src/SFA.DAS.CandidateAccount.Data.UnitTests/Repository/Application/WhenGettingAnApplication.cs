using FluentAssertions;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenGettingAnApplication
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Application_Is_Returned_By_Id(
        ApplicationEntity entity,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        context.Setup(x => x.ApplicationEntities).ReturnsDbSet(new List<ApplicationEntity>{entity});
            
        var actual = await repository.GetById(entity.Id, false);

        actual.Should().BeEquivalentTo(entity);
    }
}