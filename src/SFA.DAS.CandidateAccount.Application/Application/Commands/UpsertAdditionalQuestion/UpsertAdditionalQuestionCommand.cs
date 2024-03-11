using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertAdditionalQuestion;

public record UpsertAdditionalQuestionCommand : IRequest<UpsertAdditionalQuestionCommandResponse>
{
    public required Domain.Application.AdditionalQuestion AdditionalQuestion { get; init; }
    public required Guid CandidateId { get; init; }
}