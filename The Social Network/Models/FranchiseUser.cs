using System;

namespace The_Social_Network.Models
{
    public class FranchiseUser
    {
        public int UserId { get; set; }
        public int? UserGroupId { get; set; }
        public int? BranchId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool? InactiveUser { get; set; }
        public bool? BranchContact { get; set; }
        public bool? RequestReversal { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string SecurePassword { get; set; }
        public DateTime? PasswordExpiration { get; set; }
    }
}