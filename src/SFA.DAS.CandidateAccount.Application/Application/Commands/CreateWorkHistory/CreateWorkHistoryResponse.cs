﻿using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateWorkHistory;

public class CreateWorkHistoryResponse
{
    public Guid WorkHistoryId { get; set; }
    public WorkHistoryEntity WorkHistory { get; set; }
}