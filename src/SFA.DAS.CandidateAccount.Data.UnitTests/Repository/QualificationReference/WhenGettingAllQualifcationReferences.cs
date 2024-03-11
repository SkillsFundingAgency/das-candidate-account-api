using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.QualificationReference;

public class WhenGettingAllQualifcationReferences
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_All_The_Data_Is_Returned(
        List<QualificationReferenceEntity> data,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        QualificationReferenceRepository repository)
    {
        context.Setup(x => x.QualificationReferenceEntities).ReturnsDbSet(data);
        
        var actual = await repository.GetAll();

        actual.ToList().Should().BeEquivalentTo(data);
    }
}