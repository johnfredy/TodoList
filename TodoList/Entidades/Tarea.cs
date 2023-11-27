using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Entidades
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres.")]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Completado { get; set; }
        public UserLogin UserLogin { get; set; }
    }
}
