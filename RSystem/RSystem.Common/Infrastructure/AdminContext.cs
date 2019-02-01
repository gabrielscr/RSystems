using Microsoft.EntityFrameworkCore;
using RochaSystem.Domain;
using Tempus.Utils.EntityFrameworkCore;

namespace RSystem.Common.Infrastructure
{
    public class AdminContext : TransactionDbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Marca>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Estoque>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Cidade>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Estado>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pais>(opts =>
            {
                opts.Property(p => p.Id).ValueGeneratedNever();
            });
        }
    }
}
