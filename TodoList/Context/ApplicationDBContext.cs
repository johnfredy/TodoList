using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Entidades;

namespace TodoList.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Tarea> Tarea { get; set; } 
        public DbSet<UserLogin> UserLogin { get; set; }

    }
}
