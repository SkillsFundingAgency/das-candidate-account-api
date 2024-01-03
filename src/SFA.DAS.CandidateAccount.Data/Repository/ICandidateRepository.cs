using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Data.Repository
{
    public interface ICandidateRepository
    {
        Task Insert(Domain.Candidate.CandidateEntity candidate);
        Task<Domain.Candidate.CandidateEntity> GetCandidateByEmail(string email);
    }
}
