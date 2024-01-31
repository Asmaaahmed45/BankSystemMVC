using BankSystem.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankSystem.EF
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
         public DbSet<User> Users { get; set; }
         public DbSet<Role> Roles { get; set; }
         public DbSet<Employees> Employees { get; set; }
         public DbSet<Governorates> Governorates { get; set;}
         public DbSet<Region> Region { get; set; }
         public DbSet<Village> Village { get; set; }

    }
}
