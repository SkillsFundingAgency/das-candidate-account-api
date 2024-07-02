using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries;
using SFA.DAS.CandidateAccount.Application.ReferenceData.Queries.GetAvailableQualifications;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.ReferenceData;

public class WhenHandlingGetAvailableQualifications
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_The_Repository_Is_Called_And_Data_Returned(
        List<QualificationReferenceEntity> data,
        [Frozen]Mock<IQualificationReferenceRepository> repository,
        GetAvailableQualificationsQueryHandler handler)
    {
        repository.Setup(x => x.GetAll()).ReturnsAsync(data);
        
        var actual = await handler.Handle(new GetAvailableQualificationsQuery(), CancellationToken.None);

        actual.QualificationReferences.Should().BeEquivalentTo(data.Select(c => (QualificationReference)c).ToList());
    }
}