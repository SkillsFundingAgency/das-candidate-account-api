using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Qualification;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Qualification;

public class WhenDeletingAQualificationByApplicationCandidateAndId
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Candidate_And_Application_Exist_Qualifications_Its_Deleted(
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

        await repository.DeleteCandidateApplicationQualificationById(candidateId, applicationId, id);

        dataContext.Verify(x=>x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}