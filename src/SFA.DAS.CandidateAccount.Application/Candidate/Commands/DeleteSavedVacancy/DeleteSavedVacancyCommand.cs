using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteSavedVacancy
{
    public record DeleteSavedVacancyCommand(Guid CandidateId, string VacancyId) : IRequest<Unit>;
}
