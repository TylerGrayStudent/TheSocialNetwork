using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace The_Social_Network.Services
{
    public interface IResourceOwnerPasswordValidator
    {
        Task ValidateAsync(ResourceOwnerPasswordValidationContext context);
    }
}