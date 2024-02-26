using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.UpsertAdditionalQuestion;

public class UpsertAdditionalQuestionCommand : IRequest<UpsertAdditionalQuestionCommandResponse>
{
    public required Domain.Application.AdditionalQuestion AdditionalQuestion { get; set; }
    public required Guid CandidateId { get; set; }
}