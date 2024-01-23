using MediatR;
using SFA.DAS.CandidateAccount.Domain.Application;

namespace SFA.DAS.CandidateAccount.Application.Application.Commands.CreateJob
{
    public class CreateWorkHistoryCommand : IRequest<CreateWorkHistoryResponse>
    {
        public WorkHistoryType WorkHistoryType { get; set; }
        public Guid CandidateId { get; set; }
        public Guid ApplicationId { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
