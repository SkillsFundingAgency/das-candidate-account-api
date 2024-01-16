namespace SFA.DAS.CandidateAccount.Domain.Application;

public class Application
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public string? DisabilityStatus { get; set; }
    public required string VacancyReference { get; set; }
    public ApplicationStatus Status { get; set; }
    public SectionStatus IsWorkHistoryComplete { get; set; }
    public SectionStatus IsInterviewAdjustmentsComplete { get; set; }
    public SectionStatus IsEducationHistoryComplete { get; set; }
    public SectionStatus IsApplicationQuestionsComplete { get; set; }
    public SectionStatus IsDisabilityConfidenceComplete { get; set; }

    public static implicit operator Application(ApplicationEntity source)
    {
        return new Application
        {
            Id = source.Id,
            CandidateId = source.CandidateId,
            DisabilityStatus = source.DisabilityStatus,
            VacancyReference = source.VacancyReference,
            Status = ParseValue<ApplicationStatus>(source.Status),
            IsDisabilityConfidenceComplete = ParseValue<SectionStatus>(source.IsDisabilityConfidenceComplete),
            IsApplicationQuestionsComplete = ParseValue<SectionStatus>(source.IsApplicationQuestionsComplete),
            IsEducationHistoryComplete = ParseValue<SectionStatus>(source.IsEducationHistoryComplete),
            IsInterviewAdjustmentsComplete = ParseValue<SectionStatus>(source.IsInterviewAdjustmentsComplete),
            IsWorkHistoryComplete = ParseValue<SectionStatus>(source.IsWorkHistoryComplete),
        };
    }

    private static T ParseValue<T>(short status) where T : struct, Enum
    {
        Enum.TryParse<T>(status.ToString(), true, out var sectionStatus);
        return sectionStatus;
    }
}