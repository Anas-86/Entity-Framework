using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Experimental
{
    public class Northwind : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=Hitman4719781978;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
