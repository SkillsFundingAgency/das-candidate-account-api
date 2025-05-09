using MediatR;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.DeleteSavedVacancy
{
    public record DeleteSavedVacancyCommandHandler(ISavedVacancyRepository Repository) : IRequestHandler<DeleteSavedVacancyCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteSavedVacancyCommand command, CancellationToken cancellationToken)
        {
            var vacancyReference = command.VacancyId?.Split('-')[0];

            if (command.DeleteAllByReference)
            {
                var savedVacancies = await Repository.GetAllByVacancyReference(command.CandidateId, vacancyReference!);

                foreach(var savedVacancy in savedVacancies)
                {
                    await Repository.Delete(savedVacancy!);
                }

                return Unit.Value;
            }

            var result = command.VacancyId.Contains('-') ?
                await Repository.Get(command.CandidateId, command.VacancyId, null) :
                await Repository.Get(command.CandidateId, null, vacancyReference);

            if (result != null)
            {
                await Repository.Delete(result);
            }

                return Unit.Value;
        }
    }
}