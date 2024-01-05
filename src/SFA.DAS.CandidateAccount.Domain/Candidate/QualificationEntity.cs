using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class QualificationEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public int ToYear { get; set; }
        public bool IsPredicted { get; set; }
        public Guid ApplicationTemplateId { get; set; }
    }
}
