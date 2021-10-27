using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using The_Social_Network.Interfaces;
using The_Social_Network.Models;

namespace The_Social_Network.Services
{
    public class UserService : IUserService, IProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by email)
                    var user = await _userRepository.GetFranchiseUserByUserName(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = GetUserClaims(user);

                        //set issued claims to return
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(true);
        }
        
        private static IEnumerable<Claim> GetUserClaims(FranchiseUser user)
        {
            return new List<Claim>
            {
                new("user_id", user.UserID.ToString() ?? ""),
                new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };
        }
    }
}