using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class AboutYouEntity
    {
        public Guid Id { get; set; }
        public string Strengths { get; set; }
        public string Improvements { get; set; }
        public string HobbiesAndInterests { get; set; }
        public string Support { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
