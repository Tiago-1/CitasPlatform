using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CitasPlatform.Models;

namespace CitasPlatform.Data
{
    public class CitasPlatformContext : DbContext
    {
        public CitasPlatformContext (DbContextOptions<CitasPlatformContext> options)
            : base(options)
        {
        }

        public DbSet<CitasPlatform.Models.Usuario> Usuario { get; set; }

    }
}
