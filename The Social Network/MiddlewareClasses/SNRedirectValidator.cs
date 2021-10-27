using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace The_Social_Network.MiddlewareClasses
{
    public class SNRedirectValidator : IRedirectUriValidator
    {
        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(true);
        }
    }
}