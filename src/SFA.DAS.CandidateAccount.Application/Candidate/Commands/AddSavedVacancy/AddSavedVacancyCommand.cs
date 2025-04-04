using MediatR;
using SFA.DAS.CandidateAccount.Data.SavedVacancy;
using SFA.DAS.CandidateAccount.Domain.Candidate;

namespace SFA.DAS.CandidateAccount.Application.Candidate.Commands.AddSavedVacancy
{
    public class AddSavedVacancyCommand : IRequest<AddSavedVacancyCommandResult>
    {
        public Guid CandidateId { get; set; }
        public string VacancyReference { get; set; }
        public string VacancyId { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class AddSavedVacancyCommandResult
    {
        public SavedVacancy SavedVacancy { get; set; }
        public bool Created { get; set; }
    }

    public class AddSavedVacancyCommandHandler(ISavedVacancyRepository savedVacancyRepository) : IRequestHandler<AddSavedVacancyCommand, AddSavedVacancyCommandResult>
    {
        public async Task<AddSavedVacancyCommandResult> Handle(AddSavedVacancyCommand request, CancellationToken cancellationToken)
        {
            var savedVacancy = new SavedVacancy
            {
                CandidateId = request.CandidateId,
                VacancyReference = request.VacancyReference,
                VacancyId = request.VacancyId,
                CreatedOn = request.CreatedOn
            };

            var result = await savedVacancyRepository.Upsert(savedVacancy);

            return new AddSavedVacancyCommandResult
            {
                SavedVacancy = result.Item1,
                Created = result.Item2
            };
        }
    }
}
