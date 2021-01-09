using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public class ProiectContext : DbContext
    {
        public ProiectContext (DbContextOptions<ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect.Models.Song> Song { get; set; }

        public DbSet<Proiect.Models.Publisher> Publisher { get; set; }

        public DbSet<Proiect.Models.Category> Category { get; set; }
    }
}
