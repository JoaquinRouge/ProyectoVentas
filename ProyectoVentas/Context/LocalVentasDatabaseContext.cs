using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoVentas.Models;

namespace ProyectoVentas.Context
{
    public class LocalVentasDatabaseContext : DbContext
    {

        public LocalVentasDatabaseContext(DbContextOptions<LocalVentasDatabaseContext>
            options) : base(options)
        {
        }

        public DbSet<Prenda> Prendas { get; set; }
    }

}
