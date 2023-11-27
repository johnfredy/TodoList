using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Context;
using TodoList.DTO;
using TodoList.Entidades;
using TodoList.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tareaController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public tareaController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TareaDTO>>> Get()
        {
            var tareas = await context.Tarea.ToListAsync();
            return mapper.Map<List<TareaDTO>>(tareas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<TareaUserDTO>>> Get(int id)
        {
            var agente = await context.UserLogin.FirstOrDefaultAsync(x => x.Id == id);
            if (agente == null)
                return NotFound("Usuario no registrado.");

            var tareas = await context.Tarea.Include(x => x.UserLogin).Where(x => x.UserLogin.Id == id).ToListAsync();

            if (tareas.Count == 0)
                return NotFound("El usuario no cuenta con tareas.");

            return mapper.Map<List<TareaUserDTO>>(tareas);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] TareaCracionDTO tareaCreacionDTO)
        {
            if (tareaCreacionDTO.UsuarioId == 0)
                return BadRequest("No se puede crear un libro sin autor");

            var existe = await context.Tarea.AnyAsync(x => x.Nombre == tareaCreacionDTO.Nombre);
            if (existe)
                return BadRequest($"Ya existe una tarea con el nombre {tareaCreacionDTO.Nombre}");

            var usuarioIds = await context.UserLogin.FirstOrDefaultAsync(x => x.Id ==tareaCreacionDTO.UsuarioId);

            if (usuarioIds == null)
                return BadRequest($"Usuario no registrado");
            var tarea = mapper.Map<Tarea>(tareaCreacionDTO);
            //investigar como traer los datos del usuario para poder conectar con la tarea
            //string name = User.getUserId();
            //System.Console.WriteLine("Imprimir name:",name);
            tarea.UserLogin = usuarioIds;
            context.Add(tarea);
            await context.SaveChangesAsync();
            //return new CreatedAtRouteResult("ObtenerTarea", new { id = tarea.Id }, tarea);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TareaDTO tareaDTO, int id)
        {
            var tarea = await context.Tarea.Include(x => x.UserLogin).FirstOrDefaultAsync(x => x.Id == id);
            
            if (id != tareaDTO.Id && tarea == null)
                return BadRequest($"Usuario no registrado");

            //tarea.Nombre = tareaDTO.Nombre;
            tarea.Descripcion = tareaDTO.Descripcion;
            tarea.Completado = tareaDTO.Completado;
            
            context.Entry(tarea).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Tarea> Delete(int id)
        {
            var tarea = context.Tarea.FirstOrDefault(x => x.Id == id);

            if (tarea == null)
            {
                return NotFound();
            }
            //tambien puede usarse context.Autor.Remove(autor);
            context.Entry(tarea).State = EntityState.Deleted;
            context.SaveChanges();
            return tarea;
        }
    }
}
