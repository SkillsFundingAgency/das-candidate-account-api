using AutoFixture.NUnit3;
using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromAddressEntityToModel
{
    [Test, AutoData]
    public void Then_The_Fields_Are_Mapped(AddressEntity source)
    {
        var actual = (Address)source;

        actual.Should().BeEquivalentTo(source);
    }
}