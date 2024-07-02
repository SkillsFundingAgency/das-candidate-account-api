using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.Candidate.Queries.GetSavedVacancies;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.SavedVacancies
{
    [TestFixture]
    public class WhenHandlingGetSavedVacanciesByCandidateIdQuery
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Query_Is_Handled_And_Data_Returned(
            GetSavedVacanciesByCandidateIdQuery query,
            List<SavedVacancy> savedVacancies,
            [Frozen] Mock<ISavedVacancyRepository> repository,
            GetSavedVacanciesByCandidateIdQueryHandler handler)
        {
            repository.Setup(x =>
                    x.GetByCandidateId(query.CandidateId))
                .ReturnsAsync(savedVacancies);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.SavedVacancies.Should().BeEquivalentTo(savedVacancies, options => options.ExcludingMissingMembers());
        }
    }
}
