using FluentAssertions;
using SFA.DAS.CandidateAccount.Domain.Application;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CandidateAccount.Domain.UnitTests.Application;

public class WhenConvertingFromApplicationEntityToModel
{
    [Test, RecursiveMoqAutoData]
    public void Then_The_Fields_Are_Mapped(ApplicationEntity source)
    {
        source.JobsStatus = 3;
        source.QualificationsStatus = 2;
        source.WorkExperienceStatus = 1;
        source.TrainingCoursesStatus = 0;
        source.DisabilityConfidenceStatus = 1;
        source.Status = 2;
        source.SkillsAndStrengthStatus = 0;
        source.InterviewAdjustmentsStatus = 0;
        source.AdditionalQuestion2Status = 0;
        source.AdditionalQuestion1Status = 0;
        source.InterestsStatus = 0;

        var actual = (Domain.Application.Application)source;

        actual.Id.Should().Be(source.Id);
        actual.VacancyReference.Should().Be(source.VacancyReference);
        actual.CandidateId.Should().Be(source.CandidateId);
        actual.DisabilityStatus.Should().Be(source.DisabilityStatus);
        actual.JobsStatus.Should().Be(SectionStatus.NotRequired);
        actual.QualificationsStatus.Should().Be(SectionStatus.Completed);
        actual.WorkExperienceStatus.Should().Be(SectionStatus.InProgress);
        actual.TrainingCoursesStatus.Should().Be(SectionStatus.NotStarted);
        actual.DisabilityConfidenceStatus.Should().Be(SectionStatus.InProgress);
        actual.DisabilityConfidenceStatus.Should().Be(SectionStatus.InProgress);
        actual.Status.Should().Be(ApplicationStatus.Withdrawn);
        actual.SkillsAndStrengthStatus.Should().Be(SectionStatus.NotStarted);
        actual.InterviewAdjustmentsStatus.Should().Be(SectionStatus.NotStarted);
        actual.AdditionalQuestion2Status.Should().Be(SectionStatus.NotStarted);
        actual.AdditionalQuestion1Status.Should().Be(SectionStatus.NotStarted);
        actual.InterestsStatus.Should().Be(SectionStatus.NotStarted);
        actual.WhatIsYourInterest.Should().Be(source.WhatIsYourInterest);
        actual.ApplyUnderDisabilityConfidentScheme.Should().Be(source.ApplyUnderDisabilityConfidentScheme);
    }

    [TestCase(SectionStatus.NotStarted, SectionStatus.NotStarted, SectionStatus.NotStarted)]
    [TestCase(SectionStatus.InProgress, SectionStatus.NotStarted, SectionStatus.InProgress)]
    [TestCase(SectionStatus.NotRequired, SectionStatus.NotStarted, SectionStatus.InProgress)]
    [TestCase(SectionStatus.NotStarted, SectionStatus.InProgress, SectionStatus.InProgress)]
    [TestCase(SectionStatus.NotStarted, SectionStatus.NotRequired, SectionStatus.InProgress)]
    [TestCase(SectionStatus.NotRequired, SectionStatus.NotRequired, SectionStatus.Completed)]
    [TestCase(SectionStatus.Completed, SectionStatus.NotRequired, SectionStatus.Completed)]
    [TestCase(SectionStatus.NotRequired, SectionStatus.Completed, SectionStatus.Completed)]
    public void Then_The_Section_States_Are_Calculated_For_Education_History(SectionStatus qualificationStatus, SectionStatus trainingCourseStatus, SectionStatus expectedStatus)
    {
        var source = new ApplicationEntity
        {
            QualificationsStatus = (short)qualificationStatus,
            TrainingCoursesStatus = (short)trainingCourseStatus
        };

        var actual = (Domain.Application.Application)source;

        actual.EducationHistorySectionStatus.Should().Be(expectedStatus);
    }
    
    [TestCase(SectionStatus.NotStarted, SectionStatus.NotStarted, SectionStatus.NotStarted,SectionStatus.NotStarted,SectionStatus.NotStarted)]
    [TestCase(SectionStatus.NotRequired, SectionStatus.Completed, SectionStatus.NotRequired,SectionStatus.NotRequired,SectionStatus.Completed)]
    [TestCase(SectionStatus.Completed, SectionStatus.Completed, SectionStatus.Completed,SectionStatus.Completed,SectionStatus.Completed)]
    public void Then_The_Section_States_Are_Calculated_For_Application_Questions(SectionStatus skillsAndStrengthsStatus, SectionStatus interestsStatus, SectionStatus additionalQuestion1Status, SectionStatus additionalQuestion2Status, SectionStatus expectedStatus)
    {
        var source = new ApplicationEntity
        {
            SkillsAndStrengthStatus = (short)skillsAndStrengthsStatus,
            InterestsStatus = (short)interestsStatus,
            AdditionalQuestion1Status = (short)additionalQuestion1Status,
            AdditionalQuestion2Status = (short)additionalQuestion2Status,
        };

        var actual = (Domain.Application.Application)source;

        actual.ApplicationQuestionsSectionStatus.Should().Be(expectedStatus);
    }
}