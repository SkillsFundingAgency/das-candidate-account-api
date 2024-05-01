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
    Withdrawn = 2,
    Successful = 3,
    UnSuccessful = 4,
}

public enum CandidateStatus
{
    Incomplete = 0,
    Completed = 1
}