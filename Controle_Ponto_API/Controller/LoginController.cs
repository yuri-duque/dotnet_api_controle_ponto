using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Identity;
using Service.Service;
using System;

namespace Controle_Ponto_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Service_User _service = new Service_User();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(
            [FromBody]User usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                var resultLogin = _service.Login(usuario, signingConfigurations, tokenConfigurations);

                return Ok(resultLogin);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
