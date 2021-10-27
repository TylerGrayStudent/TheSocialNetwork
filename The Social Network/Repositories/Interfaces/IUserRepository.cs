using System.Threading.Tasks;
using The_Social_Network.Models;

namespace The_Social_Network.Interfaces
{
    public interface IUserRepository
    {
        Task<string> GetFranchiseUserPasswordByUserName(string username);
        Task<string> GetEmployeeUserPasswordBySsn(string ssn);
        Task<string> GetCustomerUserPasswordByEmail(string email);
        Task<FranchiseUser> GetFranchiseUserByUserName(string username);
    }
}