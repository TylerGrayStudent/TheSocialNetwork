using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Test;

namespace The_Social_Network.QuickStart.Data
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Charleston",
                    postal_code = "29414",
                    country = "America"
                };

                return new List<TestUser>
                {
                    new()
                    {
                        SubjectId = "1",
                        Username = "hackerman69",
                        Password = "l33ts3cure420",
                        Claims = new List<Claim>()
                        {
                            new(JwtClaimTypes.Name, "Tyler Gray"),
                            new(JwtClaimTypes.GivenName, "Tyler"),
                            new(JwtClaimTypes.FamilyName, "Gray"),
                            new(JwtClaimTypes.Email, "wtgray@hirequest.com"),
                            new(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new(JwtClaimTypes.WebSite, "https://tylergraydev.com"),
                            new(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                                IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim(JwtClaimTypes.Id, "1")
                        }
                    },
                    new()
                    {
                        SubjectId = "2",
                        Username = "testuser1",
                        Password = "hunter2",
                        Claims = new List<Claim>()
                        {
                            new(JwtClaimTypes.Name, "John Smith"),
                            new(JwtClaimTypes.GivenName, "John"),
                            new(JwtClaimTypes.FamilyName, "Smith"),
                            new(JwtClaimTypes.Email, "it@hirequest.com"),
                            new(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new(JwtClaimTypes.WebSite, "https://hirequest.com"),
                            new(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                                IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim(JwtClaimTypes.Id, "2")
                        }
                    }
                };
            }
        }
    }
}