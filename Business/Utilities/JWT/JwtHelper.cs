using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AccessToken CreateToken(User user)
        {
            SymmetricSecurityKey securityKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:SecurityKey"]));

            SigningCredentials signingCredentials = new 
                SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);

            _accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["TokenOptions:AccessTokenExpiration"]));

            var jwt = CreateJwtSecurityToken(user, signingCredentials);
            JwtSecurityTokenHandler jwtSecurityTokenHandler=new JwtSecurityTokenHandler();
            var token= jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(User user, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: Configuration["TokenOptions:Issuer"],
                audience: Configuration["TokenOptions:Audience"],
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
                ); ;
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };
            return claims;
        }
    }
}
