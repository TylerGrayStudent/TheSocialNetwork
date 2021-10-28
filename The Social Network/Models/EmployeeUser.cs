using System.Collections.Generic;

namespace The_Social_Network.Models
{
    public class EmployeeUser
    {
        public List<int> LegacyEmployeeIds { get; set; }
        public int LegacyBranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
    }
}