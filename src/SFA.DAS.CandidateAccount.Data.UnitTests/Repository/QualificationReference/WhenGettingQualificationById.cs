using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.QualificationReference;

public class WhenGettingQualificationById
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Data_Is_Returned(
        QualificationReferenceEntity data,
        [Frozen]Mock<ICandidateAccountDataContext> context,
        QualificationReferenceRepository repository)
    {
        context.Setup(x => x.QualificationReferenceEntities.FindAsync(data.Id)).ReturnsAsync(data);
        
        var actual = await repository.GetById(data.Id);

        actual.Should().BeEquivalentTo(data);
    }
}