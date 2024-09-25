using MediatR;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.RemoveSavedVacancy
{
    public record RemoveSavedVacancyCommandHandler(ISavedVacancyRepository Repository) : IRequestHandler<RemoveSavedVacancyCommand, Unit>
    {
        public async Task<Unit> Handle(RemoveSavedVacancyCommand command, CancellationToken cancellationToken)
        {
            var result = await Repository.Get(command.CandidateId, command.VacancyReference);

            if (result != null)
            {
                await Repository.Delete(result);
            }

            return Unit.Value;
        }
    }
}