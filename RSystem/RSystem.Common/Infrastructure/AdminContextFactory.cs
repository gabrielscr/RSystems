using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSystem.Common.Infrastructure
{
    public class AdminContextFactory : IDesignTimeDbContextFactory<AdminContext>
    {
        public AdminContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdminContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RSystem;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AdminContext(optionsBuilder.Options);
        }
    }
}
