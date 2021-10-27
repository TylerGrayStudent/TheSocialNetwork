using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using The_Social_Network.Interfaces;
using The_Social_Network.Models;
using The_Social_Network.Utilities.Interfaces;

namespace The_Social_Network
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> GetFranchiseUserPasswordByUserName(string username)
        {
            const string sql = "Select SecurePassword from tblUsers where username = @username where InactiveUser = 0";
            using var conn = _connectionFactory.CreateConnection();
            var passwordsEnum = await conn.QueryAsync<string>(sql, new {username});
            var passwords = passwordsEnum.ToList();
            return passwords.Count switch
            {
                > 1 => throw new Exception($"Found more than one user with username {username}"),
                0 => throw new Exception($"No user was found for that username {username}"),
                _ => passwords.First()
            };
        }
        
        public async Task<string> GetEmployeeUserPasswordBySsn(string ssn)
        {
            const string sql = "Select Password from ewp_user where SSN = @ssn";
            using var conn = _connectionFactory.CreateConnection();
            var passwordsEnum = await conn.QueryAsync<string>(sql, new {ssn});
            var passwords = passwordsEnum.ToList();
            return passwords.Count switch
            {
                > 1 => throw new Exception($"Found more than one employee with that given information"),
                0 => throw new Exception($"No employee was found for that given information"),
                _ => passwords.First()
            };
        }

        public async Task<string> GetCustomerUserPasswordByEmail(string email)
        {
            const string sql = "select Password from portal_user where LOWER(Email) like @email";
            using var conn = _connectionFactory.CreateConnection();
            var passwordsEnum = await conn.QueryAsync<string>(sql, new {email = email.ToLower()});
            var passwords = passwordsEnum.ToList();
            return passwords.Count switch
            {
                > 1 => throw new Exception($"Found more than one customer user with the email {email}"),
                0 => throw new Exception($"No customer user was found for with the email {email}"),
                _ => passwords.First()
            };
        }

        public async Task<FranchiseUser> GetFranchiseUserByUserName(string username)
        {
            const string sql = "select * from tblUsers where username = @username and InactiveUser = 0";
            using var conn = _connectionFactory.CreateConnection();
            var usersEnum = await conn.QueryAsync<FranchiseUser>(sql, new {username});
            var users = usersEnum.ToList();
            return users.Count switch
            {
                > 1 => throw new Exception($"Found more than one user with username {username}"),
                0 => throw new Exception("No user was found for that username {username}"),
                _ => users.First()
            };
        }
        
    }
}