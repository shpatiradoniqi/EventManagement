using EventManagement.Server.Application.Interfaces;
using EventManagement.Server.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace EventManagement.Server.Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            //gjenerimi i signing credentials pa pasur nevoj te shkoj ne DB
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
