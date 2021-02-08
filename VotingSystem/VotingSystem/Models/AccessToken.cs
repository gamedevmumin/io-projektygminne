using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace VotingSystem.Models
{
    public class AccessToken
    {
        public string TokenString { get; }

        public static AccessToken ForUser(User user, string secret)
        {
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signingCreds = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signingCreds,
                Claims = new Dictionary<string, object>
                {
                    { ClaimTypes.Role, user.Role.Name },
                    { ClaimTypes.Name, user.Username }
                }
            };

            var tokenHandler = new JsonWebTokenHandler();
            var tokenString = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessToken(tokenString);
        }

        private AccessToken(string tokenString)
        {
            TokenString = tokenString;
        }
    }
}