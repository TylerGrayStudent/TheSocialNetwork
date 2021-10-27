using System.Threading.Tasks;
using IdentityServer4.Services;

namespace The_Social_Network.MiddlewareClasses
{
    public class SNCorsPolicy : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(true);
        }
    }
}