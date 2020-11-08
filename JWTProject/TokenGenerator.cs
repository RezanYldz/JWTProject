using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject
{
    public class TokenGenerator
    {
        public string Generate()
        {
            var bytes = Encoding.UTF8.GetBytes("rezanrezanrezan1");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // issuer: Üreten kim?, auidience : tüketen kim?
            JwtSecurityToken token = new JwtSecurityToken(issuer:"http://localhost", audience:"http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddSeconds(30),signingCredentials:credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
        public string AdminTokenGenerate()
        {
            var bytes = Encoding.UTF8.GetBytes("rezanrezanrezan1");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Member"),
                new Claim(ClaimTypes.Name, "Rezan"),
                new Claim("Sehir", "İstanbul") //İlla claimtypes kullanmamız gerekmez, özel olarak da atayabiliyoruz.
            };

            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", audience: "http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials, claims: claims);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
