using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate;
public class PreferenceEntity
{
    public Guid PreferenceId { get; set; }
    public string PreferenceMeaning { get; set; }
    public string PreferenceHint { get; set; }
}