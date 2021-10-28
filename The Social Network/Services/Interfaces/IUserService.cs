using System.Threading.Tasks;
using The_Social_Network.Models;

namespace The_Social_Network.Services
{
    public interface IUserService
    {
        Task<FranchiseUser> GetFranchiseUserByLoginCredentials(LoginCredential credential);
    }
}