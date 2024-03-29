namespace SFA.DAS.CandidateAccount.Domain.Application;

public enum SectionStatus
{
    NotStarted = 0,
    InProgress = 1,
    Incomplete = 2,
    Completed = 3,
    NotRequired = 4
}

public enum ApplicationStatus
{
    Draft = 0,
    Submitted = 1,
    Withdrawn = 2
}