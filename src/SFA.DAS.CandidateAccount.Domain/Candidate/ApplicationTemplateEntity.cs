using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class ApplicationTemplateEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string DisabilityStatus { get; set; }
    }
}
