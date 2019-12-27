using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Controle_Ponto_API.Controller
{
    public class ApiController : ControllerBase
    {
        protected int GetUserId()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                List<Claim> claims = identity.Claims.ToList();
                var identityClaim = claims.FirstOrDefault(x => x.Properties.Where(x => x.Value == JwtRegisteredClaimNames.NameId).ToList().Any());

                if (identityClaim != null && int.TryParse(identityClaim.Value, out int id))
                {
                    return id;
                }

                throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}