using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenGettingQualificationByApplicationAndId
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Candidate_And_Application_Exist_Qualifications_Returned(
        Guid applicationId,
        Guid candidateId,
        Guid id,
        QualificationEntity qualification,
        List<QualificationEntity> qualifications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        QualificationRepository repository)
    {
        qualification.Id = id;
        qualification.ApplicationId = applicationId;
        qualification.ApplicationEntity.Id = applicationId;
        qualification.ApplicationEntity.CandidateId = candidateId;
        qualifications.Add(qualification);
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        var actual = await repository.GetCandidateApplicationQualificationById(candidateId, applicationId, id);

        actual.Should().BeEquivalentTo(qualification);
    }
}