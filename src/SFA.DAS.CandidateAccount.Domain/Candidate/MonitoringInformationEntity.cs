using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class MonitoringInformationEntity
    {
        public Guid Id { get; set; }
        public string Gender { get; set; }
        public string DisabilityStatus { get; set; }
        public string Ethnicity { get; set; }
        public Guid CandidateId { get; set; }
    }
}
