using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Data.Candidate;
using SFA.DAS.CandidateAccount.Data.UnitTests.DatabaseMock;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Data.UnitTests.Repository.Candidate
{
    [TestFixture]
    public class WhenDeletingCandidate
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_Id_DoesNotExist_Then_Returns_Null(
            CandidateEntity candidate,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

            //Act
            var result = await repository.DeleteCandidate(Guid.NewGuid());

            //Assert
            result.Item1.Should().BeNull();
            result.Item2.Should().BeFalse();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Id_Exist_Then_Returns_Candidate(
            CandidateEntity candidate,
            [Frozen] Mock<ICandidateAccountDataContext> context,
            CandidateRepository repository)
        {
            //Arrange
            context.Setup(x => x.CandidateEntities).ReturnsDbSet(new[] { candidate });

            //Act
            var result = await repository.DeleteCandidate(candidate.Id);

            //Assert
            result.Item1.Should().BeEquivalentTo(candidate, options => options
                    .Excluding(c => c.Status)
                    .Excluding(c => c.GovUkIdentifier));
            result.Item1!.GovUkIdentifier.Should().BeNull();
            result.Item1.Status.Should().Be((short) CandidateStatus.Deleted);
            result.Item2.Should().BeTrue();
        }
    }
}
