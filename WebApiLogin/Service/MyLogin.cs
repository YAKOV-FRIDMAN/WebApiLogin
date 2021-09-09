using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiLogin.Service
{
    public class MyLogin : IMyLogin
    {
        #region Filds

        private readonly Dictionary<string, string> users =
            new Dictionary<string, string>()
            {
                {"user1","pass1" },
                {"user2","pass2" },
                {"user3","pass3" },
                {"user4","pass4" },
            };

        private string secretKey;
        #endregion



        public MyLogin(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public string Login(string userName, string password)
        {
            if (!users.Any(u => u.Key == userName && u.Value == password))
            {
                return null;
            }
            else
            {
                // 1) SecretKey ==> Bytes
                var token = Encoding.ASCII.GetBytes(secretKey);

                //2) Creat

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();

                //  פג תוקף בתאריך   
                tokenDescriptor.Expires = DateTime.UtcNow.AddDays(2);

                ClaimsIdentity claims = new ClaimsIdentity();
                //  תְבִיעָה
                claims.AddClaim(new Claim(ClaimTypes.Name, userName));
                claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                tokenDescriptor.Subject = claims;
                //Token
                tokenDescriptor.SigningCredentials = new SigningCredentials(
                      new SymmetricSecurityKey(token), SecurityAlgorithms.HmacSha256);

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                var tokenObj = tokenHandler.CreateToken(tokenDescriptor);

                string tokenText = tokenHandler.WriteToken(tokenObj);


                return tokenText;
            }
        }
    }
}
