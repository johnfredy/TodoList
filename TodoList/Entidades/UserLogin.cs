using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Entidades
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public List<Tarea> Tareas { get; set; }
    }
}
