using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using The_Social_Network.Interfaces;
using The_Social_Network.Models;

namespace The_Social_Network.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<FranchiseUser> GetFranchiseUserByLoginCredentials(LoginCredential credential)
        {
            try
            {
                using var http = new HttpClient();
                var url = $"{Environment.GetEnvironmentVariable("WEB_SERVICES_LOCATION")}/users/login/portal/idp";
                var content = new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");
                using var response = await http.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<FranchiseUser>(responseContent);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        
        private static IEnumerable<Claim> GetUserClaims(FranchiseUser user)
        {
            return new List<Claim>
            {
                new("user_id", user.UserId.ToString() ?? ""),
                new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };
        }
    }
}