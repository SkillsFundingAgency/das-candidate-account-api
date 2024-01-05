using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class TrainingCourseEntity
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public Guid ApplicationTemplateId { get; set; }
        public string Title { get; set; }
    }
}
