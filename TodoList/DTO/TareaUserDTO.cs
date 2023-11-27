using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.DTO
{
    public class TareaUserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Completado { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
