using Domain;
using Microsoft.IdentityModel.Tokens;
using Repository.EntitiesRepository;
using Service.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Service.Service
{
    public class Service_User
    {
        readonly Repository_User _repository = new Repository_User();

        public object Login(User usuario, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.UserName))
            {
                var usuarioBase = _repository.Login(usuario);

                if (usuarioBase != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.NameId, usuarioBase.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.UserName)
                        }
                    );

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                        TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    return new
                    {
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        message = "OK"
                    };
                }
            }
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }
    }
}
