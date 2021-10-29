using System.Collections;
using System.Collections.Generic;

namespace The_Social_Network.Models
{
    public class ClientUser
    {
        public int LegacyId { get; set; }
        public IEnumerable<int> LegacyBranchIds { get; set; }
        public IEnumerable<int> LegacyCustomerIds { get; set; }
        public IEnumerable<int> LegacyJobsiteIds { get; set; }
        public string PasswordHash { get; set; }
        public char UserScope { get; set; }
        public char UserPosition { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ApproveTimes { get; set; }
        public bool ViewTimes { get; set; }
    }
}