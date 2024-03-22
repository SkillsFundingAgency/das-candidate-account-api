using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.UserAccount.Address;
using SFA.DAS.CandidateAccount.Data.Address;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.UserAccount.CreateUserAddress;
public class WhenHandlingCreateUserAddressCommand
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Entity_Created(
        CreateUserAddressCommand command,
        CandidateEntity candidateEntity,
        AddressEntity addressEntity,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        [Frozen] Mock<IAddressRepository> addressRepository,
        [Greedy] CreateUserAddressCommandHandler handler)
    {
        candidateRepository.Setup(x => x.GetByGovIdentifier(command.GovUkIdentifier))
            .ReturnsAsync(candidateEntity);

        addressRepository.Setup(x => x.Create(It.Is<AddressEntity>(x => x.CandidateId == candidateEntity.Id))).ReturnsAsync(addressEntity);

        var actual = await handler.Handle(command, CancellationToken.None);

        actual.Id.Should().Be(addressEntity.Id);
    }

    [Test, RecursiveMoqAutoData]
    public async Task And_No_Candidate_Is_Found_Then_Throw_InvalidOperationException(CreateUserAddressCommand command,
        [Frozen] Mock<ICandidateRepository> candidateRepository,
        CreateUserAddressCommandHandler handler)
    {
        candidateRepository.Setup(x => x.GetByGovIdentifier(command.GovUkIdentifier))
            .ReturnsAsync(() => null);

        Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
