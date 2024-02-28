﻿using SFA.DAS.CandidateAccount.Application.Application.Queries.GetAdditionalQuestion;

namespace SFA.DAS.CandidateAccount.Api.ApiResponses;

public class GetAdditionalQuestionItemApiResponse
{
    public Guid Id { get; set; }
    public string? QuestionId { get; set; }
    public string? Answer { get; set; }
    public Guid ApplicationId { get; set; }

    public static implicit operator GetAdditionalQuestionItemApiResponse(GetAdditionalQuestionItemQueryResult source)
    {
        return new GetAdditionalQuestionItemApiResponse
        {
            Id = source.Id,
            ApplicationId = source.ApplicationId,
            QuestionId = source.QuestionId,
            Answer = source.Answer,
        };
    }
}