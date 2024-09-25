using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.RemoveSavedVacancy
{
    public record RemoveSavedVacancyCommand(Guid CandidateId, string VacancyReference) : IRequest<Unit>;
}
