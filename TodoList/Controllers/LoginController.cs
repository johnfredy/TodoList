using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entidades;
using TodoList.Services;
using TodoList.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService authService;

        public LoginController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public ActionResult Token(UserLoginDTO credenciales)
        {
            var result = authService.ValidateLoginAsync(credenciales.Correo, credenciales.Password);
            if (result.Result)
            {
                System.Console.WriteLine("Authorization..");
                var fechaActual = DateTime.UtcNow;
                var validez = TimeSpan.FromHours(5);
                var fechaExpiracion = fechaActual.Add(validez);

                var token = authService.GenerateToken(fechaActual, credenciales.Correo, validez);
                return Ok(new
                {
                    Token = token,
                    ExpireAt = fechaExpiracion
                });
            }
            return StatusCode(401);
        }
    }
}
