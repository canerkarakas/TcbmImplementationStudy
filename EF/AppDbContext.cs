using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TcmbImplementationStudy.Models;

namespace TcmbImplementationStudy.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CurrencyDB;Integrated Security=True");
        }
    }
}
