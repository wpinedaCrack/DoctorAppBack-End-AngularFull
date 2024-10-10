using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data.Servicios
{
    public class TokenServicio : ITokenServicio
    {
        private readonly SymmetricSecurityKey _key;

        public TokenServicio(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["TokenKey"]) );
        }

        public string crearToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Username)
            };
            var creds = new SigningCredentials( _key, SecurityAlgorithms.HmacSha512Signature );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims ),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
