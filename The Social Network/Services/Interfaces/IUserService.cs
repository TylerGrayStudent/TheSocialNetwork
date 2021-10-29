using System.Threading.Tasks;
using The_Social_Network.Models;

namespace The_Social_Network.Services
{
    public interface IUserService
    {
        Task<FranchiseUser> GetFranchiseUserByLoginCredentials(LoginCredential credential);
        Task<ClientUser> GetClientUserByLoginCredentials(LoginCredential credential);
        Task<EmployeeUser> GetEmployeeUserByLoginCredentials(LoginCredential credential);
    }
}