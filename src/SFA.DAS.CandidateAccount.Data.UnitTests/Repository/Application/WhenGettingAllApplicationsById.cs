using AutoFixture.NUnit3;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application
{
    [TestFixture]
    public class WhenGettingAllApplicationsById
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Data_Is_Returned_By_Id(
            Guid applicationId,
            ApplicationEntity entity,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            ApplicationRepository repository)
        {
            entity.Id = applicationId;
            context.Setup(x => x.ApplicationEntities)
                .ReturnsDbSet(new List<ApplicationEntity> { entity });

            var actual = await repository.GetAllById(new EditableList<Guid>{ applicationId });

            actual.Should().BeEquivalentTo(new List<ApplicationEntity> { entity });
        }
    }
}
