namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertAdditionalQuestion;

public record UpsertAdditionalQuestionCommandResponse
{
    public required Domain.Application.AdditionalQuestion AdditionalQuestion { get; init; }
    public bool IsCreated { get; init; }
}