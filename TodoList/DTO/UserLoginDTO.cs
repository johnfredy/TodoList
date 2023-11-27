using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
