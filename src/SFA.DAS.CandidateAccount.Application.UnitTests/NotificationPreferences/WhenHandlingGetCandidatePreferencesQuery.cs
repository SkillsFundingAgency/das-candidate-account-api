﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.CandidateAccount.Application.CandidatePreferences.Queries.GetCandidatePreferences;
using SFA.DAS.CandidateAccount.Data.CandidatePreferences;
using SFA.DAS.CandidateAccount.Data.Preference;
using SFA.DAS.CandidateAccount.Domain.Candidate;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Application.UnitTests.NotificationPreferences;
public class WhenHandlingGetCandidatePreferencesQuery
{
    [Test, RecursiveMoqAutoData]
    public async Task Then_Request_Is_Handled_And_Result_Returned(
        GetCandidatePreferencesQuery request,
        List<PreferenceEntity> preferenceEntities,
        List<CandidatePreferencesEntity> candidatePreferencesEntities,
        [Frozen] Mock<IPreferencesRepository> preferencesRepository,
        [Frozen] Mock<ICandidatePreferencesRepository> candidatePreferencesRepository,
        GetCandidatePreferencesQueryHandler handler)
    {
        preferencesRepository.Setup(x => x.GetAll()).ReturnsAsync(preferenceEntities);
        candidatePreferencesRepository.Setup(x => x.GetAllByCandidate(request.CandidateId)).ReturnsAsync(candidatePreferencesEntities);

        var actual = await handler.Handle(request, CancellationToken.None);

        actual.CandidatePreferences.Count().Should().Be(preferenceEntities.Count());
    }
}
