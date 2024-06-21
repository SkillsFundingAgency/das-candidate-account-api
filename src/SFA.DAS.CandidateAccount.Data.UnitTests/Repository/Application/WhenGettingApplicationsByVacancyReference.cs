using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Application;

public class WhenGettingApplicationsByVacancyReference
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Data_Is_Returned_By_VacancyReference(
        string vacancyReference,
        ApplicationEntity entity1,
        ApplicationEntity entity2,
        ApplicationEntity entity3,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        entity1.VacancyReference = vacancyReference;
        entity3.VacancyReference = vacancyReference;
        context.Setup(x => x.ApplicationEntities)
            .ReturnsDbSet(new List<ApplicationEntity> { entity1, entity2, entity3 });

        var actual = await repository.GetApplicationsByVacancyReference(vacancyReference);

        actual.Should().BeEquivalentTo(new List<ApplicationEntity> { entity1, entity3 });
    }
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Data_Is_Returned_By_VacancyReference_And_Filtered_By_EmailContact(
        string vacancyReference,
        short statusId,
        Guid preferenceId,
        ApplicationEntity entity1,
        ApplicationEntity entity2,
        ApplicationEntity entity3,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        ApplicationRepository repository)
    {
        entity1.VacancyReference = vacancyReference;
        entity3.VacancyReference = vacancyReference;
        entity3.Status = statusId;
        entity3.CandidateEntity.CandidatePreferences.FirstOrDefault()!.ContactMethod = "email";
        entity3.CandidateEntity.CandidatePreferences.FirstOrDefault()!.PreferenceId = preferenceId;
        context.Setup(x => x.CandidateEntities).ReturnsDbSet(new List<CandidateEntity> { entity1.CandidateEntity, entity2.CandidateEntity, entity3.CandidateEntity});
        context.Setup(x => x.ApplicationEntities)
            .ReturnsDbSet(new List<ApplicationEntity> { entity1, entity2, entity3 });
        

        var actual = await repository.GetApplicationsByVacancyReference(vacancyReference, statusId, preferenceId, true);

        actual.Should().BeEquivalentTo(new List<ApplicationEntity> { entity3 });
    }
}