using AutoFixture;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Application.Commands.AddLegacyApplication;
using SFA.DAS.CandidateAccount.Data.Application;
using SFA.DAS.CandidateAccount.Data.ReferenceData;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.Application
{
    [TestFixture]
    public class WhenHandlingAddLegacyApplicationCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Application_Is_Created_And_The_Application_Id_Is_Returned(
            AddLegacyApplicationCommand command,
            [Frozen] Mock<IApplicationRepository> applicationRepository,
            [Frozen] Mock<IQualificationReferenceRepository> qualificationReferenceRepository,
            AddLegacyApplicationCommandHandler handler)
        {
            var applicationEntity = new ApplicationEntity
            {
                Id = Guid.NewGuid()
            };
            
            var qualificationReferenceEntities = new List<QualificationReferenceEntity>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "GCSE"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "BTEC"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Other"
                }
            };

            for (var i = 0; i < command.LegacyApplication.Qualifications.Count; i++)
            {
                var qualification = command.LegacyApplication.Qualifications[i];
                qualification.QualificationType = qualificationReferenceEntities.ToArray()[i].Id.ToString();
            }

            qualificationReferenceRepository.Setup(x => x.GetAll())
                .ReturnsAsync(qualificationReferenceEntities);

            applicationRepository.Setup(x =>
                    x.Upsert(It.IsAny<ApplicationEntity>()))
                .ReturnsAsync(new Tuple<ApplicationEntity, bool>(applicationEntity, true));

            var result = await handler.Handle(command, CancellationToken.None);

            result.Id.Should().Be(applicationEntity.Id);
        }
    }
}
