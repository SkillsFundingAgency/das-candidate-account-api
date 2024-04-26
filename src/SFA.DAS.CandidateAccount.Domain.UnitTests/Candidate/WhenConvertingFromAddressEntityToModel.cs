using AutoFixture.NUnit3;
using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Candidate;

public class WhenConvertingFromAddressEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(AddressEntity source)
    {
        var actual = (Address)source;

        actual.Should().BeEquivalentTo(source, options=> options.Excluding(c=>c.Candidate));
    }
}