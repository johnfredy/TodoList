using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Context;
using TodoList.DTO;
using TodoList.Entidades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        
        public RegistroController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // POST api/<RegistroController>
        [HttpPost("registrar", Name = "registrarUsuario")]
        public async Task<ActionResult> Post([FromBody] UserLoginDTO userDTO)
        {
            var existe = await context.UserLogin.AnyAsync(x => x.Correo == userDTO.Correo);

            if (existe)
                return BadRequest($"Usuario ya se encuentra registrado");

            var userLogin = mapper.Map<UserLogin>(userDTO);
            context.Add(userLogin);
            await context.SaveChangesAsync();
            //return new CreatedAtRouteResult("ObtenerAutor", new { id = userLogin.Id }, userLogin);
            return Ok();

        }
    }
}
