using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Context;

namespace TodoList.Models
{
    public class PrepareDb
    {
        public static void Population(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDBContext>());
            }
        }

        public static void SeedData(ApplicationDBContext context)
        {
            System.Console.WriteLine("Applying initial migration..."); //para informarnos desde la consola
            context.Database.Migrate();
            System.Console.WriteLine("Initial migration (database) done!");
        }
    }
}
