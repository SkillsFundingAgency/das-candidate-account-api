using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenGettingQualificationByApplicationCandidateAndQualificationType
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Candidate_And_Application_Exist_Qualifications_Returned_For_That_Type(
        Guid applicationId,
        Guid candidateId,
        Guid qualificationReferenceId,
        QualificationEntity qualification,
        QualificationEntity qualification2,
        List<QualificationEntity> qualifications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        QualificationRepository repository)
    {
        qualification.ApplicationId = applicationId;
        qualification.ApplicationEntity.Id = applicationId;
        qualification.ApplicationEntity.CandidateId = candidateId;
        qualification2.ApplicationId = applicationId;
        qualification2.ApplicationEntity.Id = applicationId;
        qualification2.ApplicationEntity.CandidateId = candidateId;
        qualification2.QualificationReferenceId = qualificationReferenceId;
        qualifications.Add(qualification);
        qualifications.Add(qualification2);
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        var actual = await repository.GetCandidateApplicationQualificationsByQualificationReferenceType(candidateId, applicationId, qualificationReferenceId);

        actual.Should().BeEquivalentTo(new List<QualificationEntity>{qualification2});
    }
    
    
    
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Qualifications_Are_Returned_In_Order(
        Guid applicationId,
        Guid candidateId,
        Guid qualificationReferenceId,
        QualificationEntity qualification,
        QualificationEntity qualification2,
        List<QualificationEntity> qualifications,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        QualificationRepository repository)
    {
        qualification.ApplicationId = applicationId;
        qualification.ApplicationEntity.Id = applicationId;
        qualification.ApplicationEntity.CandidateId = candidateId;
        qualification.QualificationReferenceId = qualificationReferenceId;
        qualification.CreatedDate = DateTime.UtcNow.AddDays(-1);
        qualification2.ApplicationId = applicationId;
        qualification2.ApplicationEntity.Id = applicationId;
        qualification2.ApplicationEntity.CandidateId = candidateId;
        qualification2.QualificationReferenceId = qualificationReferenceId;
        qualification2.CreatedDate = DateTime.UtcNow.AddDays(-2);
        qualifications.Add(qualification);
        qualifications.Add(qualification2);
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        var actual = await repository.GetCandidateApplicationQualificationsByQualificationReferenceType(candidateId, applicationId, qualificationReferenceId);

        actual.Should().BeEquivalentTo(new List<QualificationEntity>{qualification2, qualification}).And.BeInAscendingOrder(c=>c.CreatedDate);
    }
}