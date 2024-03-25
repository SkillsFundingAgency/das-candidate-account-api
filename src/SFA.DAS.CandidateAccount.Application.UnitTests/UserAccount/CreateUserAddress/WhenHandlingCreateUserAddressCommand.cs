using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.UserAccount.Address;
using SFA.DAS.CandidateAccount.Data.Address;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.UserAccount.CreateUserAddress;
public class WhenHandlingCreateUserAddressCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateUserAddressCommand command,
        AddressEntity addressEntity,
        [Frozen] Mock<IAddressRepository> addressRepository,
        [Greedy] CreateUserAddressCommandHandler handler)
    {
        addressRepository.Setup(x => x.Create(It.Is<AddressEntity>(x => x.CandidateId == command.CandidateId))).ReturnsAsync(addressEntity);

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Id.Should().Be(addressEntity.Id);
    }
}
