using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class CandidatePreferencesEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid PreferenceId { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
