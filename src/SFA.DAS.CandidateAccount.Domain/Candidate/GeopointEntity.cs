using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CandidateAccount.Domain.Candidate
{
    public class GeopointEntity
    {
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Easting { get; set; }
        public string Northing { get; set; }
        public Guid AddressId { get; set; }
    }
}
