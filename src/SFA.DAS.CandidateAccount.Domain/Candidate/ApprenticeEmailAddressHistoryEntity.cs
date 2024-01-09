using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class ApprenticeEmailAddressHistoryEntity
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ChangedOn { get; set; }
        public Guid CandidateId { get; set; }
    }
}
