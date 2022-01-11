﻿using ApartmentsApp.WebUI.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.WebUI.Infrastructure
{
    public class JwtService
    {
        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateToken(int id, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var writedToken = tokenHandler.WriteToken(token);
            return writedToken;
            //rol veriyorum
            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Role, isAdmin?"Admin":"User"),
            //    new Claim(ClaimTypes.Name,id.ToString())
            //};

            //var symmetricSKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //var credentials = new SigningCredentials(symmetricSKey, SecurityAlgorithms.HmacSha256Signature);
            //var header = new JwtHeader(credentials);

            //var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            //var token = new JwtSecurityToken(header, payload);
            //return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public JwtSecurityToken Verify(string jwt)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["Token:SecurityKey"]);
        //    tokenHandler.ValidateToken(jwt, new TokenValidationParameters
        //    {
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuerSigningKey = true,
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //    }, out SecurityToken validatedToken);

        //    return (JwtSecurityToken)validatedToken;
        //}
    }
}
