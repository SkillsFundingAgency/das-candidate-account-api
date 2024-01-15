using MediatR;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateApplication;

public class CreateApplicationRequest : IRequest<CreateApplicationResponse>
{
    public string VacancyReference { get; set; }
    public string Email { get; set; }
    public short Status { get; set; }
}