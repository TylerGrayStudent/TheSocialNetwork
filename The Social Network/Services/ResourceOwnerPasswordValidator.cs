using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using The_Social_Network.Interfaces;
using The_Social_Network.Models;

namespace The_Social_Network.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator, IdentityServer4.Validation.IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        private static string CreatePasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password, salt, KeyDerivationPrf.HMACSHA512, 100000, 32));
        }

        public static IEnumerable<Claim> GetUserClaims(FranchiseUser user)
        {
            return new List<Claim>
            {
                new("user_id", user.UserID.ToString() ?? ""),
                new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };
        }
        
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userRepository.GetFranchiseUserByUserName(context.UserName);
                if (user != null)
                {
                    var encodedSalt = user.SecurePassword[..24];
                    var decodedSalt = Convert.FromBase64String(encodedSalt);
                    var inputHash = CreatePasswordHash(context.Password, decodedSalt);
                    var savedHash = user.SecurePassword.Substring(24, 44);
                    if (Equals(inputHash, savedHash))
                    {
                        context.Result = new GrantValidationResult(subject: user.UserID.ToString(),
                            authenticationMethod: "custom", claims: GetUserClaims(user));
                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception e)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}