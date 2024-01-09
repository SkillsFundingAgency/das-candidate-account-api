using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AddressEntity
    {
        public Guid Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set;}
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
        public string Uprn { get; set; }
        public Guid CandidateId { get; set; }
    }
}
