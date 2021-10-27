using System.Collections.Generic;
using System.Security.Claims;

namespace The_Social_Network.QuickStart.Models
{
    public class TestUser
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}