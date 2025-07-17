using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.DeleteApplication;

public record DeleteApplicationCommand(Guid CandidateId, Guid ApplicationId) : IRequest<DeleteApplicationCommandResult>;