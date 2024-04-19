using AutoFixture;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenDeletingAQualificationByApplicationCandidateAndQualificationReferenceId
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Candidate_And_Application_Exist_Then_Qualifications_Are_Deleted(
        Guid applicationId,
        Guid candidateId,
        Guid qualificationReferenceId,
        List<QualificationEntity> qualifications,
        QualificationEntity qualification,
        [Frozen] Mock<ICandidateAccountDataContext> dataContext,
        QualificationRepository repository)
    {
        qualification.QualificationReferenceId = qualificationReferenceId;
        qualification.QualificationReferenceEntity.Id = qualificationReferenceId;
        qualification.ApplicationId = applicationId;
        qualification.ApplicationEntity.Id = applicationId;
        qualification.ApplicationEntity.CandidateId = candidateId;
        qualifications.Add(qualification);
        dataContext.Setup(x => x.QualificationEntities).ReturnsDbSet(qualifications);

        await repository.DeleteCandidateApplicationQualificationsByReferenceId(candidateId, applicationId, qualificationReferenceId);

        dataContext.Verify(x=>x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}