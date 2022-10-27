using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dragut_Diana_Lab2.Models;

namespace Dragut_Diana_Lab2.Data
{
    public class Dragut_Diana_Lab2Context : DbContext
    {
        public Dragut_Diana_Lab2Context (DbContextOptions<Dragut_Diana_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Dragut_Diana_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Dragut_Diana_Lab2.Models.Publisher> Publisher { get; set; }
        public DbSet<Dragut_Diana_Lab2.Models.Author> Author { get; set; }
    
    }
}
